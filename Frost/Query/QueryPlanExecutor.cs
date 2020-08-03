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
        var rowList = new List<Row>();

        resultString += " ------------ " + Environment.NewLine;
        int totalRows = 0;
        int rows = 0;
        int buildRows = 0;
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
            else
            {
                if (stepResult.RowsAffected > 0)
                {
                    rows = stepResult.RowsAffected;
                }
            }

            totalRows += rows;
            rowList.AddRange(stepResult.Rows);
        }

        if (!planFailed)
        {
            resultString += BuildResponse(GetFinalColumns(rowList, plan.Columns, out buildRows));
            resultString += " ------------ " + Environment.NewLine;
            result.Message = "Succeeded";
            result.IsSuccessful = true;
            result.JsonData = resultString;
            if (buildRows > 0)
            {
                result.NumberOfRowsAffected = buildRows;
            }
            else
            {
                result.NumberOfRowsAffected = totalRows;
            }
        }
        else
        {
            result.IsSuccessful = false;
            result.Message = resultString;
            result.NumberOfRowsAffected = 0;
        }

        return result;
    }
    #endregion

    #region Private Methods
    private List<Row> GetFinalColumns(List<Row> input, List<string> columns, out int rowCount)
    {
        var result = new List<Row>();
        Row x = null;
        RowQueryComparer comparer = new RowQueryComparer();

        foreach (var row in input)
        {
            x = new Row(Guid.Empty);
            foreach (var value in row.Values)
            {
                foreach (var column in columns)
                {
                    if (value.ColumnName.ToUpper() == column.ToUpper())
                    {
                        x.AddValue(null, value.Value, value.ColumnName, value.ColumnType);
                    }
                }
            }
            result.Add(x);
        }
        var z = new List<Row>();
        z = result.Distinct(comparer).ToList();
        rowCount = z.Count;
        return z;
    }
    private StepResult ExecuteStep(IPlanStep step, string databaseName, out int rowCount)
    {
        var result = step.GetResult(_process, databaseName);
        rowCount = result.Rows.Count;
        return result;
    }

    private string BuildResponse(List<Row> input)
    {
        string results = string.Empty;

        var rows = input;
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

