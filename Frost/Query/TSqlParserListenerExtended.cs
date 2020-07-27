using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using FrostDB;
using FrostDB.Query;
using QueryParserConsole.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.XPath;

public class TSqlParserListenerExtended : TSqlParserBaseListener
{
    #region Private Fields
    IStatement _statement;
    ICharStream _charStream;
    private string _input;
    #endregion

    #region Constructors
    public TSqlParserListenerExtended() { }
    public TSqlParserListenerExtended(IStatement statement, string input)
    {
        _statement = statement;
        _input = input;
    }
    public TSqlParserListenerExtended(SelectStatement statement, string input)
    {
        _statement = statement;
        _input = input;
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

    public InsertStatement GetStatementAsInsert()
    {
        return _statement as InsertStatement;
    }

    public UpdateStatement GetStatementAsUpdate()
    {
        return _statement as UpdateStatement;
    }

    public DeleteStatement GetStatementAsDelete()
    {
        return _statement as DeleteStatement;
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

    // begin insert functions
    public override void EnterInsert_statement([NotNull] TSqlParser.Insert_statementContext context)
    {
        Console.WriteLine(context.GetText());
    }

    public override void EnterFull_table_name(TSqlParser.Full_table_nameContext context)
    {
        Console.WriteLine(context.GetText());
    }

    public override void EnterColumn_name_list(TSqlParser.Column_name_listContext context)
    {
        Console.WriteLine(context.GetText());
    }

    public override void EnterSimple_id(TSqlParser.Simple_idContext context)
    {
        Console.WriteLine(context.GetText());
    }

    public override void EnterInsert_statement_value(TSqlParser.Insert_statement_valueContext context)
    {
        Console.WriteLine(context.GetText());
    }

    public override void EnterExpression_list(TSqlParser.Expression_listContext context)
    {
        Console.WriteLine(context.GetText());
    }

    public override void EnterPrimitive_expression(TSqlParser.Primitive_expressionContext context)
    {
        Console.WriteLine(context.GetText());
    }

    // end insert functions

    // begin update functions
    public override void EnterUpdate_statement(TSqlParser.Update_statementContext context)
    {
        Console.WriteLine(context.GetText());
    }

    public override void EnterUpdate_elem(TSqlParser.Update_elemContext context)
    {
        Console.WriteLine(context.GetText());
    }

    public override void EnterSearch_condition_list(TSqlParser.Search_condition_listContext context)
    {
        Console.WriteLine(context.GetText());
    }
    // end update functions

    // begin delete functions
    public override void EnterDelete_statement(TSqlParser.Delete_statementContext context)
    {
        Console.WriteLine(context.GetText());
    }

    public override void EnterDelete_statement_from(TSqlParser.Delete_statement_fromContext context)
    {
        Console.WriteLine(context.GetText());
    }
    // end delete functions

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
