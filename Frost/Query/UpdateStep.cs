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
        // if we have an input step then we need to get the rows from the input step and then 
        // update those rows and save back to the database
        throw new NotImplementedException();
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