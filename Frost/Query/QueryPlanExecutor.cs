using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using FrostDB;
using FrostCommon;
using FrostCommon.Net;

public class QueryPlanExecutor
{
    #region Private Fields
    private Process _process;
    #endregion

    #region Public Properties
    #endregion

    #region Protected Methods
    #endregion

    #region Events
    #endregion

    #region Constructors
    public QueryPlanExecutor() { }
    public QueryPlanExecutor(Process process)
    {
        _process = process;
    }
    #endregion

    #region Public Methods
    public FrostPromptResponse Execute(QueryPlan plan)
    {
        var result = new FrostPromptResponse();
        var resultString = string.Empty;
        bool planFailed = false;

        resultString += " ------------ " + Environment.NewLine;
        int totalRows = 0;
        int rows = 0;
        StepResult stepResult = null;
        plan.Steps.Reverse();
        foreach (var step in plan.Steps)
        {
            stepResult = ExecuteStep(step, plan.DatabaseName, out rows);
            if (stepResult.IsValid == false)
            {
                planFailed = true;
                resultString = stepResult.ErrorMessage;
                break;
            }
            totalRows += rows;
            resultString += BuildResponse(stepResult);
        }

        if (!planFailed)
        {
            resultString += " ------------ " + Environment.NewLine;
            result.Message = "Succeeded";
            result.IsSuccessful = true;
            result.JsonData = resultString;
        }
        else
        {
            result.IsSuccessful = false;
            result.Message = resultString;
        }

        return result;
    }
    #endregion

    #region Private Methods
    private StepResult ExecuteStep(IPlanStep step, string databaseName, out int rowCount)
    {
        var result = step.GetResult(_process, databaseName);
        rowCount = result.Rows.Count();
        return result;
    }

    private string BuildResponse(StepResult input)
    {
        string results = string.Empty;

        var rows = input.Rows;
        rows.ForEach(r =>
        {
            r.Values.ForEach(v =>
            {
                results += " { " + v.ColumnName + " : " + v.Value.ToString() + " } ";
            });

            results += Environment.NewLine;
        });

        return results;
    }
    #endregion

}

