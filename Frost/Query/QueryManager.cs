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

            if (IsDDLStatment(input))
            {
                FrostIDDLStatement statement = GetDDLStatement(commandStatement, databaseName);

                if (string.IsNullOrEmpty(statement.DatabaseName))
                {
                    statement.DatabaseName = databaseName;
                }
                else
                {
                    databaseName = statement.DatabaseName;
                }
                plan = _planGenerator.GeneratePlan(statement, databaseName);
            }
            else
            {
                FrostIDMLStatement statement = GetDMLStatement(commandStatement, databaseName);
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


        private bool IsDDLStatment(string input)
        {
            if (input.Contains(QueryKeywords.CREATE_TABLE) || input.Contains(QueryKeywords.CREATE_DATABASE))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private FrostIDDLStatement GetDDLStatement(string input, string databaseName)
        {
            FrostIDDLStatement result = null;
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
            var parseTree = parser.ddl_clause();
            ParseTreeWalker walker = new ParseTreeWalker();
            loader = new TSqlParserListenerExtended(GetDDLStatementType(sqlStatement), sqlStatement);
            loader.TokenStream = tokens;
            walker.Walk(loader, parseTree);

            if (loader.IsStatementCreateTable())
            {
                result = loader.GetStatementAsCreateTable();
            }

            if (loader.IsStatementCreateDatabase())
            {
                result = loader.GetStatementAsCreateDatabase();
            }

            return result;
        }

        private FrostIDMLStatement GetDMLStatement(string input, string databaseName)
        {
            FrostIDMLStatement result = null;
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
            var parseTree = parser.dml_clause();
            ParseTreeWalker walker = new ParseTreeWalker();
            loader = new TSqlParserListenerExtended(GetDMLStatementType(sqlStatement), sqlStatement);
            loader.TokenStream = tokens;
            walker.Walk(loader, parseTree);

            if (loader.DMLStatement is InsertStatement)
            {
                var item = loader.DMLStatement as InsertStatement;
                item.Participant = GetParticipant(GetParticipantString(input));
                item.ParticipantString = GetParticipantString(input);
                item.DatabaseName = databaseName;
                if (item.Participant is null)
                {
                    item.Participant = new Participant(_process.GetLocation());
                }
                result = item;
            }
            else if (loader.DMLStatement is UpdateStatement)
            {
                var item = loader.DMLStatement as UpdateStatement;
                item.DatabaseName = databaseName;
                item.SetProcess(_process);
                result = item as FrostIDMLStatement;
            }
            else
            {
                result = loader.DMLStatement;
            }

            return result;
        }

        private FrostIDDLStatement GetDDLStatementType(string input)
        {
            FrostIDDLStatement result = null;
            if (input.Contains(QueryKeywords.CREATE_TABLE))
            {
                result = new CreateTableStatement();
            }

            if (input.Contains(QueryKeywords.CREATE_DATABASE))
            {
                result = new CreateDatabaseStatement();
            }

            return result;
        }

        private FrostIDMLStatement GetDMLStatementType(string input)
        {
            FrostIDMLStatement result = null;
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
