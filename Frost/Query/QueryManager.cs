using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FrostCommon.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Net;

namespace FrostDB
{
    internal class QueryManager
    {
        #region Private Fields
        private Process _process;
        private QueryPlanGenerator _planGenerator;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public QueryManager(Process process)
        {
            _process = process;
            _planGenerator = new QueryPlanGenerator();
        }
        #endregion

        #region Public Methods
        public QueryPlan GetPlan(string input)
        {
            var items = input.Split(';');
            var databaseStatement = string.Empty;
            var commandStatement = string.Empty;
            if (items.Count() > 0)
            {
                databaseStatement = items[0];
                commandStatement = items[1];
            }

            var databaseName = GetDatabaseName(databaseStatement);
            var statement = GetStatement(commandStatement);
            
            var plan = _planGenerator.GeneratePlan(statement, databaseName);
            
            return plan;
        }

        public FrostPromptPlan GetPlanExplanation(string input)
        {
            var promptPlan = new FrostPromptPlan();
            var plan = GetPlan(input);

            foreach (var step in plan.Steps)
            {
                promptPlan.PlanText += step.GetResultText();
            }

            return promptPlan;
        }

        #endregion

        #region Private Methods
        private string GetDatabaseName(string input)
        {
            string databaseName = string.Empty;

            if (input.Contains("USE "))
            {
                var items = input.Split(";");
                var words = items[0].Split(" ");
                databaseName = words[1];
            }

            return databaseName;
        }
        private IStatement GetStatement(string input)
        {
            IStatement result;
            var sqlStatement = string.Empty;

            if (HasParticipant(input))
            {
                sqlStatement = RemoveParticipantKeyword(input);
            }
            else
            {
                sqlStatement = input;
            }
                        
            AntlrInputStream inputStream = new AntlrInputStream(sqlStatement);
            TSqlLexer lexer = new TSqlLexer(inputStream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            TSqlParser parser = new TSqlParser(tokens);
            var parseTree = parser.dml_clause();
            ParseTreeWalker walker = new ParseTreeWalker();
            TSqlParserListenerExtended loader = new TSqlParserListenerExtended(GetStatementType(sqlStatement), sqlStatement);
            loader.TokenStream = tokens;
            walker.Walk(loader, parseTree);

            if (loader.Statement is InsertStatement)
            {
                var item = loader.Statement as InsertStatement;
                item.Participant = GetParticipant(input);
                item.ParticipantString = GetParticipantString(input);
                result = item;
            }
            else
            {
                result = loader.Statement;
            }

            return result;
        }

        private IStatement GetStatementType(string input)
        {
            IStatement result = null;
            if (input.Contains(QueryKeywords.Select))
            {
                result = new SelectStatement();
            }

            if (input.Contains(QueryKeywords.Update))      
            {
                result = new UpdateStatement();
            }

            if (input.Contains(QueryKeywords.Insert))
            {
                result = new InsertStatement();
            }

            if (input.Contains(QueryKeywords.Delete))
            {
                result = new DeleteStatement();
            }

            return result;
        }

        private string GetParticipantString(string input)
        {
            string result = string.Empty;
            if (input.Contains(QueryKeywords.For_Participant))
            {
                int keywordIndex = input.IndexOf(QueryKeywords.For_Participant);
                var participantString = input.Substring(keywordIndex + QueryKeywords.For_Participant.Length - input.Length).Trim();
                result = participantString;
            }

            return result;
        }

        private string RemoveParticipantKeyword(string input)
        {
            var result = string.Empty;
            if (HasParticipant(input))
            {
                var indexOfParticipant = input.IndexOf(QueryKeywords.For_Participant);
                result = input.Substring(0, indexOfParticipant);
            }
            
            return result;
        }

        private bool HasParticipant(string input)
        {
            if (input.Contains(QueryKeywords.For_Participant))
            {
                return true;
            }
            return false;
        }

        private Participant GetParticipant(string input)
        {
            var participant = new Participant();
            var items = input.Split(":");
            if (items.Length == 2)
            {
                var ipAddress = items[0];
                var portNumber = items[1];

                IPAddress address;
                IPAddress.TryParse(ipAddress, out address);

                if (address != null)
                {
                    participant.Location.IpAddress = ipAddress;
                    participant.Location.PortNumber = Convert.ToInt32(portNumber);
                }
            }

            return participant;
        }
        #endregion

    }
}
