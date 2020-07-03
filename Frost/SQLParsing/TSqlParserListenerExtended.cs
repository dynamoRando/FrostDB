using Antlr4.Runtime.Misc;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FrostDB
{
    public class TSqlParserListenerExtended : TSqlParserBaseListener
    {
        #region Private Fields
        private IQuery _query;
        #endregion

        #region Constructors
        internal TSqlParserListenerExtended() { }
        internal TSqlParserListenerExtended(ref IQuery query)
        {
            _query = query;
        }
        #endregion

        #region Public Methods
        public override void EnterSelect_statement([NotNull] TSqlParser.Select_statementContext context)
        {
            base.EnterSelect_statement(context);
            Debug.WriteLine("EnterSelect_statement");
            Debug.WriteLine(context.GetText());

        }

        public override void ExitSelect_statement([NotNull] TSqlParser.Select_statementContext context)
        {
            base.ExitSelect_statement(context);
            Debug.WriteLine("ExitSelect_statement");
            Debug.WriteLine(context.GetText());
        }

        public override void EnterSelect_list([NotNull] TSqlParser.Select_listContext context)
        {
            base.EnterSelect_list(context);
            Debug.WriteLine("EnterSelect_list");
            Debug.WriteLine(context.GetText());

            if (_query is SelectQuery)
            {
                var query = (_query as SelectQuery);
                query.SelectListText = context.GetText().Split(',').ToList();
            }
        }

        public override void ExitSelect_list([NotNull] TSqlParser.Select_listContext context)
        {
            base.ExitSelect_list(context);
            Debug.WriteLine("ExitSelect_list");
            Debug.WriteLine(context.GetText());

        }

        public override void EnterSelect_list_elem([NotNull] TSqlParser.Select_list_elemContext context)
        {
            base.EnterSelect_list_elem(context);
            Debug.WriteLine("EnterSelect_list_elem");
            Debug.WriteLine(context.GetText());
        }

        public override void EnterSql_clause([NotNull] TSqlParser.Sql_clauseContext context)
        {
            base.EnterSql_clause(context);
            Debug.WriteLine("EnterSql_clause");
            Debug.WriteLine(context.GetText());
        }

        public override void EnterSql_clauses([NotNull] TSqlParser.Sql_clausesContext context)
        {
            base.EnterSql_clauses(context);
            Debug.WriteLine("EnterSql_clauses");
            Debug.WriteLine(context.GetText());
        }

        public override void ExitSql_clause([NotNull] TSqlParser.Sql_clauseContext context)
        {
            base.ExitSql_clause(context);
            Debug.WriteLine("ExitSql_clause");
            Debug.WriteLine(context.GetText());
        }

        public override void ExitSql_clauses([NotNull] TSqlParser.Sql_clausesContext context)
        {
            base.ExitSql_clauses(context);
            Debug.WriteLine("ExitSql_clause");
            Debug.WriteLine(context.GetText());
        }

        public override void EnterSearch_condition_list([NotNull] TSqlParser.Search_condition_listContext context)
        {
            base.EnterSearch_condition_list(context);
            Debug.WriteLine("EnterSearch_condition_list");
            Debug.WriteLine(context.GetText());
        }

        public override void ExitSearch_condition_list([NotNull] TSqlParser.Search_condition_listContext context)
        {
            base.ExitSearch_condition_list(context);
            Debug.WriteLine("ExitSearch_condition_list");
            Debug.WriteLine(context.GetText());
        }

        public override void EnterSearch_condition([NotNull] TSqlParser.Search_conditionContext context)
        {
            base.EnterSearch_condition(context);
            Debug.WriteLine("EnterSearch_condition");
            Debug.WriteLine(context.GetText());

            if (_query is SelectQuery)
            {
                var query = (_query as SelectQuery);
                query.SearchConditionCount++;
                query.SearchConditionText = context.GetText();
                if (query.SearchConditionText.Contains(QueryKeywords.And)
                    || query.SearchConditionText.Contains(QueryKeywords.Or))
                {
                    query.HasBooleans = true;
                }
            }
        }

        public override void ExitSearch_condition([NotNull] TSqlParser.Search_conditionContext context)
        {
            base.ExitSearch_condition(context);
            Debug.WriteLine("ExitSearch_condition");
            Debug.WriteLine(context.GetText());
        }

        public override void EnterSearch_condition_and([NotNull] TSqlParser.Search_condition_andContext context)
        {
            base.EnterSearch_condition_and(context);
            Debug.WriteLine("EnterSearch_condition_and");
            Debug.WriteLine(context.GetText());
        }

        public override void EnterSearch_condition_not([NotNull] TSqlParser.Search_condition_notContext context)
        {
            base.EnterSearch_condition_not(context);
            Debug.WriteLine("EnterSearch_condition_not");
            Debug.WriteLine(context.GetText());
        }
        public override void EnterFull_table_name([NotNull] TSqlParser.Full_table_nameContext context)
        {
            base.EnterFull_table_name(context);
            Debug.WriteLine("EnterFull_table_name");
            Debug.WriteLine(context.GetText());
        }
        public override void EnterTable_name([NotNull] TSqlParser.Table_nameContext context)
        {
            base.EnterTable_name(context);
            Debug.WriteLine("EnterTable_name");
            Debug.WriteLine(context.GetText());

            if (_query is SelectQuery)
            {
                var query = (_query as SelectQuery);
                query.TableListText.Add(context.GetText());
            }

        }
        public override void EnterSimple_name([NotNull] TSqlParser.Simple_nameContext context)
        {
            base.EnterSimple_name(context);
            Debug.WriteLine("EnterSimple_name");
            Debug.WriteLine(context.GetText());
        }
        #endregion
    }
}
