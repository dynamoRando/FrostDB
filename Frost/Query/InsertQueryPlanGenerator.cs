using FrostDB;
using System;
using FrostDB.Extensions;
using System.Collections;
using System.Collections.Generic;

public class InsertQueryPlanGenerator
{
    #region Private Fields
    Process _process;
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
        var result = new QueryPlan();
        if (statement.Participant.IsLocal(_process))
        {
            foreach (var value in statement.InsertValues)
            {
                result.Steps.Add(GetInsertStep(statement.ColumnNames, value));
            }
        }
        else
        {
            foreach (var value in statement.InsertValues)
            {
                result.Steps.Add(GetInsertStepRemote(statement.ColumnNames, value, statement.Participant));
            }
        }

        return result;
    }
    #endregion

    #region  Private Methods
    private InsertStep GetInsertStep(List<string> columns, InsertStatementGroup value)
    {
        var step = new InsertStep();
        step.Columns = columns;
        step.Values = value.Values;

        return step;
    }

    private InsertStepRemote GetInsertStepRemote(List<string> columns, InsertStatementGroup value, Participant participant)
    {
        var step = new InsertStepRemote();
        step.Columns = columns;
        step.Values = value.Values;
        step.Participant = participant;
        return step;
    }
    #endregion
}