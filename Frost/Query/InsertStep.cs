using FrostDB;
using System;
using System.Collections;
using System.Collections.Generic;

public class InsertStep : IPlanStep
{
    #region Private Fields
    Process _process;
    #endregion

    #region Public Properties
    public Guid Id { get; set; }
    public int Level { get; set; }
    public List<string> Columns { get; set; }
    public List<string> Values { get; set; }
    public string TableName { get; set; }
    public string DatabaseName { get; set; }
    public bool IsValid { get; set; }
    #endregion

    #region Constructors
    public InsertStep()
    {
        Id = Guid.NewGuid();
        Columns = new List<string>();
        Values = new List<string>();
        IsValid = true;
    }
    #endregion

    #region Public Methods
    public StepResult GetResult(Process process, string databaseName)
    {
        _process = process;
        var result = new StepResult();
        Table table = null;
        if (_process.HasDatabase(DatabaseName))
        {
            var db = _process.GetDatabase(DatabaseName);
            if (db.HasTable(TableName))
            {
                table = db.GetTable(TableName);
                bool hasAllColumns = true;

                foreach (var columnName in Columns)
                {
                    if (!table.HasColumn(columnName))
                    {
                        result.IsValid = false;
                        result.ErrorMessage = $"Column: {columnName} not found";
                        hasAllColumns = false;
                    }

                    if (!hasAllColumns)
                    {
                        break;
                    }
                }

                if (hasAllColumns)
                {
                    if (Columns.Count == Values.Count)
                    {
                        var row = table.GetNewRowForLocal();
                        foreach (var value in Values)
                        {
                            int valueIndex = Values.IndexOf(value);
                            var col = table.GetColumn(Columns[valueIndex]);


                            row.Row.AddValue(col.Id, value, col.Name, col.DataType);
                        }
                        table.AddRow(row);
                        result.RowsAffected++;
                        result.IsValid = true;
                    }
                    else
                    {
                        result.IsValid = false;
                        result.ErrorMessage = "Column Value Count Mismatch";
                    }
                }
            }
            else
            {
                result.IsValid = false;
                result.ErrorMessage = $"Table: {TableName} not found";
            }
        }
        else
        {
            result.IsValid = false;
            result.ErrorMessage = $"Database: {DatabaseName} not found";
        }

        return result;
    }

    public string GetResultText()
    {
        string result = string.Empty;
        result += $"Insert into Database:Table - {DatabaseName} : {TableName}" + Environment.NewLine;

        foreach (var value in Values)
        {
            var intPos = Values.IndexOf(value);
            var columnName = Columns[intPos];
            result += $"Inserting into column {columnName} value: {value}" + Environment.NewLine;
        }

        return result;
    }
    #endregion

    #region Private Methods
    #endregion
}