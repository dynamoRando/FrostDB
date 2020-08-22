using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FrostCommon.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using FrostCommon;

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
            _planGenerator = new QueryPlanGenerator(_process);
        }
        #endregion

        #region Public Methods
        public QueryPlan GetPlan(string input)
        {
            var plan = new QueryPlan();
            var items = input.Split(';');
            var databaseStatement = string.Empty;
            var commandStatement = string.Empty;
            if (items.Count() > 0)
            {
                databaseStatement = items[0];
                commandStatement = items[1];
            }

            var databaseName = GetDatabaseName(databaseStatement);
            var statement = GetStatement(commandStatement, databaseName);
            statement.DatabaseName = databaseName;

            if (!statement.IsValid)
            {
                var step = new SearchStep();
                step.IsValid = false;
                plan.Steps.Add(step);
            }
            else
            {
                plan = _planGenerator.GeneratePlan(statement, databaseName);
            }

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

        private IParseTree GetStatementType(string input, TSqlParser parser)
        {
            if (input.Contains(QueryKeywords.CREATE_TABLE))
            {
                return parser.ddl_clause();
            }
            else
            {
                return parser.dml_clause();
            }
        }

        private IDMLStatement GetStatement(string input, string databaseName)
        {
            IDMLStatement result = null;
            TSqlParserListenerExtended loader;
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
            var parseTree = GetStatementType(input, parser);
            ParseTreeWalker walker = new ParseTreeWalker();

            if (parseTree is TSqlParser.Ddl_clauseContext)
            {
                loader = new TSqlParserListenerExtended(GetDDLStatementType(sqlStatement), sqlStatement);
            }
            else
            {
                loader = new TSqlParserListenerExtended(GetDMLStatementType(sqlStatement), sqlStatement);
            }

            loader.TokenStream = tokens;
            walker.Walk(loader, parseTree);

            if (loader.Statement is InsertStatement)
            {
                var item = loader.Statement as InsertStatement;
                item.Participant = GetParticipant(GetParticipantString(input));
                item.ParticipantString = GetParticipantString(input);
                item.DatabaseName = databaseName;
                if (item.Participant is null)
                {
                    item.Participant = new Participant(_process.GetLocation());
                }
                result = item;
            }
            else if (loader.Statement is UpdateStatement)
            {
                var item = loader.Statement as UpdateStatement;
                item.DatabaseName = databaseName;
                item.SetProcess(_process);
                result = item as IDMLStatement;
            }
            else
            {
                result = loader.Statement;
            }

            return result;
        }

        private IDMLStatement GetDDLStatementType(string input)
        {
            throw new NotImplementedException();
        }

        private IDMLStatement GetDMLStatementType(string input)
        {
            IDMLStatement result = null;
            if (input.Contains(QueryKeywords.SELECT))
            {
                result = new SelectStatement();
            }

            if (input.Contains(QueryKeywords.UPDATE))
            {
                result = new UpdateStatement();
            }

            if (input.Contains(QueryKeywords.INSERT))
            {
                result = new InsertStatement();
            }

            if (input.Contains(QueryKeywords.DELETE))
            {
                result = new DeleteStatement();
            }

            return result;
        }

        private string GetParticipantString(string input)
        {
            string result = string.Empty;
            if (input.Contains(QueryKeywords.FOR_PARTICIPANT))
            {
                int keywordIndex = input.IndexOf(QueryKeywords.FOR_PARTICIPANT);
                var participantString = input.Substring(keywordIndex + QueryKeywords.FOR_PARTICIPANT.Length).Trim();
                result = participantString;
            }

            return result;
        }

        private string RemoveParticipantKeyword(string input)
        {
            var result = string.Empty;
            if (HasParticipant(input))
            {
                var indexOfParticipant = input.IndexOf(QueryKeywords.FOR_PARTICIPANT);
                result = input.Substring(0, indexOfParticipant).Trim();
            }

            return result;
        }

        private bool HasParticipant(string input)
        {
            if (input.Contains(QueryKeywords.FOR_PARTICIPANT))
            {
                return true;
            }
            return false;
        }

        private Participant GetParticipant(string input)
        {
            Participant participant = null;
            var items = input.Split(":");
            if (items.Length == 2)
            {
                var ipAddress = items[0];
                var portNumber = items[1];

                IPAddress address;
                IPAddress.TryParse(ipAddress, out address);

                if (address != null)
                {
                    participant = new Participant(new Location(Guid.NewGuid(), ipAddress, Convert.ToInt32(portNumber), string.Empty));
                }
            }

            return participant;
        }
        #endregion

    }
}
