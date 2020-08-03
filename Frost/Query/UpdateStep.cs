using System;
using FrostDB;

public class UpdateStep : IPlanStep
{
    #region Private Fields
    private Process _process;
    #endregion
    
    #region Public Properties
    public Guid Id { get; set; }
    public int Level { get; set; }
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