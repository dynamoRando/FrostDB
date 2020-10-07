using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using FrostDB;
using FrostCommon;
using FrostCommon.Net;
using System.Data;

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
        int rows = 0;
        int buildRows = 0;
        int totalRows = 0;

        if (!plan.IsValid)
        {
            resultString = HandleInvalidPlan(result, resultString);
        }
        else
        {
            if (IsDDLPlan(plan))
            {
                HandleDDLPlan(plan, ref resultString, ref planFailed);
            }
            else
            {
                HandleDMLPlan(plan, ref resultString, ref planFailed, rowList, ref rows, ref totalRows);
            }

            if (!planFailed)
            {
                buildRows = HandleFailedDMLPlan(plan, result, ref resultString, rowList, totalRows);
            }
            else
            {
                result.IsSuccessful = false;
                result.Message = resultString;
                result.NumberOfRowsAffected = 0;
            }
        }

        return result;
    }
    #endregion

    #region Private Methods
    private int HandleFailedDMLPlan(QueryPlan plan, FrostPromptResponse result, ref string resultString, List<Row> rowList, int totalRows)
    {
        int buildRows;
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

        return buildRows;
    }

    private static string HandleInvalidPlan(FrostPromptResponse result, string resultString)
    {
        resultString += " ------------ " + Environment.NewLine;
        resultString += " Unable to parse statement" + Environment.NewLine;
        resultString += " ------------ " + Environment.NewLine;
        result.IsSuccessful = false;
        result.Message = resultString;
        return resultString;
    }

    private void HandleDDLPlan(QueryPlan plan, ref string resultString, ref bool planFailed)
    {
        if (plan.Steps.Any(step => step is CreateTableStep))
        {
            HandleCreateTable(plan, ref resultString, ref planFailed);
        }

        if (plan.Steps.Any(step => step is CreateDatabaseStep))
        {
            HandleCreateDatabase(plan, ref resultString, ref planFailed);
        }
    }

    private void HandleCreateDatabase(QueryPlan plan, ref string resultString, ref bool planFailed)
    {
        foreach(var step in plan.Steps)
        {
            if (step is CreateDatabaseStep)
            {
                CreateDatabaseStep cd = step as CreateDatabaseStep;
                StepResult result = cd.GetResult(_process, cd.DatabaseName);
                if (result.IsValid)
                {
                    resultString += $"Database {cd.DatabaseName} created";
                    planFailed = false;
                }
            }
        }
    }

    private void HandleCreateTable(QueryPlan plan, ref string resultString, ref bool planFailed)
    {
        foreach (var step in plan.Steps)
        {
            if (step is CreateTableStep)
            {
                CreateTableStep ct = step as CreateTableStep;
                StepResult result = ct.GetResult(_process, plan.DatabaseName);
                if (result.IsValid)
                {
                    resultString += $"Table {ct.TableName} created";
                    planFailed = false;
                }
            }
        }
    }

    private void HandleDMLPlan(QueryPlan plan, ref string resultString, ref bool planFailed, List<Row> rowList, ref int rows, ref int totalRows)
    {
        resultString += " ------------ " + Environment.NewLine;

        StepResult stepResult = null;
        if (!(plan.OriginalStatement is UpdateStatement))
        {
            plan.Steps.Reverse();
        }

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
    }

    private bool IsDDLPlan(QueryPlan plan)
    {
        return plan.Steps.Any(step => step is CreateTableStep) ||
            plan.Steps.Any(step => step is CreateDatabaseStep);
    }

    // TO DO: should this be a "step"?
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

