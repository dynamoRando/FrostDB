using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using QueryParserConsole.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.XPath;

namespace QueryParserConsole
{
    public class TSqlParserListenerExtended : TSqlParserBaseListener
    {
        #region Private Fields
        IStatement _statement;
        ICharStream _charStream;
        #endregion

        #region Constructors
        public TSqlParserListenerExtended() { }
        public TSqlParserListenerExtended(IStatement statement)
        {
            _statement = statement;
        }
        public TSqlParserListenerExtended(SelectStatement statement)
        {
            _statement = statement;
        }
        #endregion

        #region Public Properties
        public CommonTokenStream TokenStream { get; set; }
        #endregion

        #region Public Methods
        public override void EnterTable_name([NotNull] TSqlParser.Table_nameContext context)
        {
            base.EnterTable_name(context);
            var select = GetStatementAsSelect();
            select.Tables.Add(context.GetText());
        }
        public SelectStatement GetStatementAsSelect()
        {
            return _statement as SelectStatement;
        }
        public override void ExitSearch_condition([NotNull] TSqlParser.Search_conditionContext context)
        {
            base.ExitSearch_condition(context);
            // this will set the full statement on the final exit
            var select = GetStatementAsSelect();
            select.WhereClause = context.GetText();

            int a = context.Start.StartIndex;
            int b = context.Stop.StopIndex;
            Interval interval = new Interval(a, b);
            _charStream = context.Start.InputStream;
            select.WhereClauseWithWhiteSpace = _charStream.GetText(interval);
        }
        public override void EnterSelect_statement([NotNull] TSqlParser.Select_statementContext context)
        {
            base.EnterSelect_statement(context);
            var select = GetStatementAsSelect();
            select.RawStatement = context.GetText();
        }

        public override void EnterSelect_list([NotNull] TSqlParser.Select_listContext context)
        {
            base.EnterSelect_list(context);
            var select = GetStatementAsSelect();
            select.SelectListRaw = context.GetText();
        }

        public override void EnterSelect_list_elem([NotNull] TSqlParser.Select_list_elemContext context)
        {
            base.EnterSelect_list_elem(context);
            var select = GetStatementAsSelect();
            select.SelectList.Add(context.GetText());
        }

        public override void EnterSearch_condition([NotNull] TSqlParser.Search_conditionContext context)
        {
            base.EnterSearch_condition(context);
        }

        public override void EnterPredicate([NotNull] TSqlParser.PredicateContext context)
        {
            base.EnterPredicate(context);
            Console.WriteLine(context.GetText());
            var select = GetStatementAsSelect();
            var part = new StatementPart();
            part.StatementTableName = select.Tables.FirstOrDefault();
            part.Text = context.GetText();
            part.StatementOrigin = "EnterPredicate";

            int a = context.Start.StartIndex;
            int b = context.Stop.StopIndex;
            Interval interval = new Interval(a, b);
            _charStream = context.Start.InputStream;
            
            part.TextWithWhiteSpace = _charStream.GetText(interval);

            var parent = context.Parent.Parent;
            if (parent != null)
            {
                part.StatementParent = parent.GetText();
                var tokenInterval = parent.SourceInterval;
                part.StatementParentWithWhiteSpace = GetWhitespaceStringFromTokenInterval(tokenInterval);
            }

            var grandparent = context.Parent.Parent.Parent;
            if (grandparent != null)
            {
                part.StatementGrandParent = grandparent.GetText();
                var tokenInterval = grandparent.SourceInterval;
                part.StatementGrandParentWithWhiteSpace = GetWhitespaceStringFromTokenInterval(tokenInterval);
            }

            part.ParseStatementPart();
            select.Statements.Add(part);
        }
        #endregion

        #region Private Properties
        private string GetWhitespaceStringFromTokenInterval(Interval interval)
        {
            try
            {
                var start = TokenStream.Get(interval.a).StartIndex;
                var end = TokenStream.Get(interval.b).StopIndex;
                Interval i = new Interval(start, end);
                return _charStream.GetText(i);
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
