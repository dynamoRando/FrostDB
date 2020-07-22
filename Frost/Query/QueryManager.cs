using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FrostCommon.Net;
using QueryParserConsole.Query;
using System;
using System.Collections.Generic;
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
            var promptPlan = new FrostPromptPlan();
            var statement = GetStatement(input);
            return _planGenerator.GeneratePlan(statement);
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
