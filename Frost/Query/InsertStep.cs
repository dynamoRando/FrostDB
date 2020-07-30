using FrostDB;
using System;
using System.Collections;
using System.Collections.Generic;

public class InsertStep : IPlanStep
{
    #region Private Fields
    Process _process;
    string _databaseName;
    string _tableName;
    #endregion

    #region Public Properties
    public Guid Id { get; set; }
    public int Level { get; set; }
    public List<string> Columns { get; set; }
    public List<string> Values { get; set;}
    #endregion

    #region Constructors
    public InsertStep()
    {
        Id = Guid.NewGuid();
        Columns = new List<string>();
        Values = new List<string>();
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
    #endregion
}