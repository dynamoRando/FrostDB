using FrostDB;
using System;
using FrostDB.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InsertQueryPlanGenerator
{
    #region Private Fields
    Process _process;
    InsertStatement _statement;
    #endregion

    #region Public Properties
    #endregion

    #region Constructors
    public InsertQueryPlanGenerator()
    {

    }

    public InsertQueryPlanGenerator(Process process)
    {
        _process = process;
    }
    #endregion

    #region Public Methods
    public QueryPlan GeneratePlan(InsertStatement statement)
    {
        _statement = statement;
        var result = new QueryPlan();
        if (statement.Participant.IsLocal(_process))
        {
            foreach (var value in statement.InsertValues)
            {
                result.Steps.Add(GetInsertStep(value));
            }
        }
        else
        {
            foreach (var value in statement.InsertValues)
            {
                result.Steps.Add(GetInsertStepRemote(value));
            }
        }

        return result;
    }
    #endregion

    #region  Private Methods
    private InsertStep GetInsertStep(InsertStatementGroup value)
    {
        var step = new InsertStep();
        step.Columns = _statement.ColumnNames;
        step.Values = value.Values;
        step.TableName = _statement.Tables.First();
        step.DatabaseName = _statement.DatabaseName;
        return step;
    }

    private InsertStepRemote GetInsertStepRemote(InsertStatementGroup value)
    {
        var foo = new InsertStepRemote();
        foo.Columns = _statement.ColumnNames;
        foo.Values = value.Values;
        foo.Participant = _statement.Participant;
        foo.DatabaseName = _statement.DatabaseName;
        foo.TableName = _statement.Tables.First();
        return foo;
    }
    #endregion
}