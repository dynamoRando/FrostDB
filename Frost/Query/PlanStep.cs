using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;

public class PlanStep : IPlanStep
{
    #region Private Fields
    string _operator;
    string _value;
    string _columnName;
    string _tableName;
    Table _table;
    Column _column;
    Process _process;
    #endregion

    #region Public Properties
    public Guid Id { get; set; }
    public int Level { get; set; }
    #endregion

    #region Protected Methods
    #endregion

    #region Events
    #endregion

    #region Constructors
    public PlanStep() { }
    public PlanStep(string tableName, string columnName, string operation, string value)
    {
        _tableName = tableName;
        _columnName = columnName;
        _operator = operation;
        _value = value;
        AssignDbObjects();
    }
    #endregion

    #region Public Methods
    public StepResult GetResult(Process process, string databaseName)
    {
        _process = process;
        throw new NotImplementedException();
    }

    public string GetResultText()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Private Methods
    private void AssignDbObjects()
    {
        Console.WriteLine($"Set table to : {_tableName}");
        Console.WriteLine($"Set column to : {_columnName}");
    }
    #endregion
}
