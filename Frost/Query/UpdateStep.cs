using System;
using FrostDB;
using System.Collections.Generic;
using System.Linq;
using FrostDB.Extensions;
public class UpdateStep : IPlanStep
{
    #region Private Fields
    private Process _process;
    #endregion

    #region Public Properties
    public Guid Id { get; set; }
    public int Level { get; set; }
    public string ColumnName { get; set; }
    public string Value { get; set; }
    public string TableName { get; set; }
    public string DatabaseName { get; set; }
    public IPlanStep InputStep { get; set; }
    public bool HasInputStep => CheckHasInputStep();
    #endregion

    #region Constructors
    public UpdateStep()
    {
        Id = Guid.NewGuid();
        Level = 0;
    }
    public UpdateStep(Process process) : this()
    {
        _process = process;
    }

    #endregion

    #region Public Methods
    public void SetProcess(Process process)
    {
        _process = process;
    }
    public StepResult GetResult(Process process, string databaseName)
    {
        var result = new StepResult();
        var resultRows = new List<Row>();
        // if we have an input step then we need to get the rows from the input step and then 
        // update those rows and save back to the database
        if (HasInputStep)
        {
            var resultStep = InputStep.GetResult(_process, DatabaseName);
            foreach (var row in resultStep.Rows)
            {
                foreach (var value in row.Values)
                {
                    if (value.ColumnName == ColumnName)
                    {
                        value.Value = Value;
                    }
                }

                _process.GetDatabase(DatabaseName).GetTable(TableName).UpdateRow(row.ToReference(_process, TableName, DatabaseName), row.Values);
            }
            resultRows = resultStep.Rows;
        }
        else
        {
            var table = _process.GetDatabase(DatabaseName).GetTable(TableName);
            var rows = table.GetAllRows();
            foreach (var row in rows)
            {
                foreach (var value in row.Values)
                {
                    if (value.ColumnName == ColumnName)
                    {
                        value.Value = Value;
                    }
                }

                table.UpdateRow(row.ToReference(_process, TableName, DatabaseName), row.Values);
            }

            resultRows = rows;
        }

        result.Rows = resultRows;
        return result;
    }

    public string GetResultText()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Private Methods
    private bool CheckHasInputStep()
    {
        if (InputStep != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}