using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using FrostDB;
using FrostDB.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Xml.XPath;

public class TSqlParserListenerExtended : TSqlParserBaseListener
{
    #region Private Fields
    FrostIDMLStatement _dmlStatement;
    FrostIDDLStatement _ddlStatement;
    ICharStream _charStream;
    private string _input;
    #endregion

    #region Constructors
    public TSqlParserListenerExtended() { }
    public TSqlParserListenerExtended(FrostIDMLStatement statement, string input)
    {
        _dmlStatement = statement;
        _input = input;
    }

    public TSqlParserListenerExtended(FrostIDDLStatement statement, string input)
    {
        _ddlStatement = statement;
        _input = input;
    }

    #endregion

    #region Public Properties
    public CommonTokenStream TokenStream { get; set; }
    public FrostIDMLStatement DMLStatement => _dmlStatement;
    public FrostIDDLStatement DDLStatement => _ddlStatement;
    #endregion

    #region Public Methods
    public override void EnterTable_name([NotNull] TSqlParser.Table_nameContext context)
    {
        base.EnterTable_name(context);
        if (_dmlStatement != null)
        {
            _dmlStatement.Tables.Add(context.GetText());
        }
        else
        {
            if (_ddlStatement is CreateTableStatement)
            {
                (_ddlStatement as CreateTableStatement).TableName = context.GetText();
            }
        }
    }

    public SelectStatement GetStatementAsSelect()
    {
        return _dmlStatement as SelectStatement;
    }

    public InsertStatement GetStatementAsInsert()
    {
        return _dmlStatement as InsertStatement;
    }

    public UpdateStatement GetStatementAsUpdate()
    {
        return _dmlStatement as UpdateStatement;
    }

    public DeleteStatement GetStatementAsDelete()
    {
        return _dmlStatement as DeleteStatement;
    }

    public bool IsStatementInsert()
    {
        return _dmlStatement is InsertStatement;
    }

    public bool IsStatementSelect()
    {
        return _dmlStatement is SelectStatement;
    }

    public bool IsStatementUpdate()
    {
        return _dmlStatement is UpdateStatement;
    }

    public bool IsStatementDelete()
    {
        return _dmlStatement is DeleteStatement;
    }

    public override void ExitSearch_condition([NotNull] TSqlParser.Search_conditionContext context)
    {
        base.ExitSearch_condition(context);
        // this will set the full statement on the final exit

        _dmlStatement.WhereClause.WhereClauseText = context.GetText();

        int a = context.Start.StartIndex;
        int b = context.Stop.StopIndex;
        Interval interval = new Interval(a, b);
        _charStream = context.Start.InputStream;
        _dmlStatement.WhereClause.WhereClauseWithWhiteSpace = _charStream.GetText(interval);
    }

    public override void EnterSelect_statement([NotNull] TSqlParser.Select_statementContext context)
    {
        base.EnterSelect_statement(context);
        _dmlStatement.RawStatement = context.GetText();
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

        var part = new StatementPart();
        part.StatementTableName = _dmlStatement.Tables.FirstOrDefault();
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

        if (!part.ParseStatementPart())
        {
            _dmlStatement.IsValid = false;
        }

        _dmlStatement.WhereClause.Conditions.Add(part);
    }

    // begin insert functions
    public override void EnterInsert_statement([NotNull] TSqlParser.Insert_statementContext context)
    {
        base.EnterInsert_statement(context);

        var statement = GetStatementAsInsert();
        statement.RawStatement = context.GetText();
    }

    public override void EnterFull_table_name(TSqlParser.Full_table_nameContext context)
    {
        base.EnterFull_table_name(context);

        if (IsStatementInsert())
        {
            var statement = GetStatementAsInsert();
            statement.Tables.Add(context.GetText());
        }

        if (IsStatementUpdate())
        {
            var statement = GetStatementAsUpdate();
            statement.Tables.Add(context.GetText());
        }
    }

    public override void EnterColumn_name_list(TSqlParser.Column_name_listContext context)
    {
        base.EnterColumn_name_list(context);

        if (IsStatementInsert())
        {
            var statement = GetStatementAsInsert();
            statement.ColumnNames.AddRange(context.GetText().Split(",").ToList());
        }
    }

    public override void EnterSimple_id(TSqlParser.Simple_idContext context)
    {
        base.EnterSimple_id(context);
        Debug.WriteLine(context.GetText());
    }

    public override void EnterInsert_statement_value(TSqlParser.Insert_statement_valueContext context)
    {
        base.EnterInsert_statement_value(context);
        Debug.WriteLine(context.GetText());
    }

    public override void EnterExpression_list(TSqlParser.Expression_listContext context)
    {
        base.EnterExpression_list(context);

        if (IsStatementInsert())
        {
            var statement = GetStatementAsInsert();
            var group = new InsertStatementGroup();
            group.Values.AddRange(context.GetText().Split(",").ToList());
            statement.InsertValues.Add(group);
        }

        Debug.WriteLine(context.GetText());
    }

    public override void EnterPrimitive_expression(TSqlParser.Primitive_expressionContext context)
    {
        Console.WriteLine(context.GetText());
    }

    // end insert functions

    // begin update functions
    public override void EnterUpdate_statement(TSqlParser.Update_statementContext context)
    {
        if (IsStatementUpdate())
        {
            var statement = GetStatementAsUpdate();
            statement.RawStatement = context.GetText();
        }

        Debug.WriteLine(context.GetText());
    }
    public override void EnterUpdate_elem(TSqlParser.Update_elemContext context)
    {
        if (IsStatementUpdate())
        {
            var statement = GetStatementAsUpdate();
            var element = new UpdateStatementElement();
            element.RawString = context.GetText();
            
            int a = context.Start.StartIndex;
            int b = context.Stop.StopIndex;
            Interval interval = new Interval(a, b);
            _charStream = context.Start.InputStream;

            element.RawStringWithWhitespace = _charStream.GetText(interval);
            statement.Elements.Add(element);
        }

        Debug.WriteLine(context.GetText());
    }

    public override void EnterSearch_condition_list(TSqlParser.Search_condition_listContext context)
    {
        if (IsStatementUpdate())
        {
            var statement = GetStatementAsUpdate();
            if (statement.WhereClause is null)
            {
                statement.WhereClause = new WhereClause();
            }

            statement.WhereClause.WhereClauseText = context.GetText();

        }
        Debug.WriteLine(context.GetText());
    }
    // end update functions

    // begin delete functions
    public override void EnterDelete_statement(TSqlParser.Delete_statementContext context)
    {
        Console.WriteLine(context.GetText());
    }

    public override void EnterDelete_statement_from(TSqlParser.Delete_statement_fromContext context)
    {
        var item = context.GetText();
        Debug.WriteLine(item);
        if (IsStatementDelete())
        {
            var statement = GetStatementAsDelete();
            statement.Tables.Add(item);
        }
    }

    // end delete functions

    // create table functions
    public override void EnterCreate_table([NotNull] TSqlParser.Create_tableContext context)
    {
        base.EnterCreate_table(context);
        var createTable = new CreateTableStatement();
        createTable.RawStatement = context.GetText();
        _ddlStatement = createTable;
        Debug.WriteLine(context.GetText());
    }

    public override void EnterData_type([NotNull] TSqlParser.Data_typeContext context)
    {
        base.EnterData_type(context);
        Debug.WriteLine(context.GetText());
    }

    public override void EnterColumn_definition([NotNull] TSqlParser.Column_definitionContext context)
    {
        base.EnterColumn_definition(context);
        if (_ddlStatement != null)
        {
            if (_ddlStatement is CreateTableStatement)
            {
                (_ddlStatement as CreateTableStatement).ColumnNamesAndTypes.Add(GetWhiteSpaceFormat(context));
            }
        }
        Debug.WriteLine(context.GetText());
    }

    public override void EnterNull_notnull([NotNull] TSqlParser.Null_notnullContext context)
    {
        base.EnterNull_notnull(context);
        Debug.WriteLine(context.GetText());
    }

    public override void EnterNull_or_default([NotNull] TSqlParser.Null_or_defaultContext context)
    {
        base.EnterNull_or_default(context);
        Debug.WriteLine(context.GetText());
    }

    // end create table functions
    #endregion

    #region Private Properties
    private string GetWhiteSpaceFormat(ParserRuleContext context)
    {
        int a = context.Start.StartIndex;
        int b = context.Stop.StopIndex;
        Interval interval = new Interval(a, b);
        _charStream = context.Start.InputStream;

        return _charStream.GetText(interval);
    }
    private string GetWhitespaceStringFromTokenInterval(Interval interval)
    {
        try
        {
            var start = TokenStream.Get(interval.a).StartIndex;
            var end = TokenStream.Get(interval.b).StopIndex;
            Interval i = new Interval(start, end);
            return _charStream.GetText(i);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return string.Empty;
        }
    }
    #endregion
}
