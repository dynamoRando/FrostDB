using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FrostCommon.Net;
using QueryParserConsole.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

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

        public FrostPromptResponse GetResult(string input)
        {
            throw new NotImplementedException();
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
            AntlrInputStream inputStream = new AntlrInputStream(input);
            TSqlLexer lexer = new TSqlLexer(inputStream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            TSqlParser parser = new TSqlParser(tokens);
            var parseTree = parser.dml_clause();
            ParseTreeWalker walker = new ParseTreeWalker();
            TSqlParserListenerExtended loader = new TSqlParserListenerExtended(new SelectStatement());
            loader.TokenStream = tokens;
            walker.Walk(loader, parseTree);
            return loader.GetStatementAsSelect();
        }
        #endregion

    }
}
