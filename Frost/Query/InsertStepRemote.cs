using FrostDB;
using System;
using System.Collections.Generic;
using System.Collections;

public class InsertStepRemote : IPlanStep
{
    #region Private Fields
    #endregion

    #region Public Properties
    public Guid Id { get; set; }
    public int Level { get; set; }
    public Participant Participant { get; set; }
    public List<string> Columns { get; set; }
    public List<string> Values { get; set; }
    #endregion

    #region Constructors
    public InsertStepRemote()
    {
        Id = Guid.NewGuid();
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