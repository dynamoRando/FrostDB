using System;
using FrostDB;
using System.Collections.Generic;

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
    public List<Row> InputRows { get; set; }
    #endregion

    #region Constructors
    public UpdateStep()
    {
        Id = Guid.NewGuid();
        Level = 0;
        InputRows = new List<Row>();
    }
    public UpdateStep(Process process) : this()
    {
        _process = process;
    }
    #endregion

    #region Public Methods
    public StepResult GetResult(Process process, string databaseName)
    {
        throw new NotImplementedException();
    }

    public string GetResultText()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Private Methods
    #endregion
}