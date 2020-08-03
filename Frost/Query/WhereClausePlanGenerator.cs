using FrostDB;
using System;
using System.Collections.Generic;

public class WhereClausePlanGenerator
{
    #region Private Fields
    private Process _process;
    #endregion
    
    #region Public Properties
    #endregion

    #region Constructors
    public WhereClausePlanGenerator()
    {

    }

    public WhereClausePlanGenerator(Process process) : this()
    {
        _process = process;
    }
    #endregion

    #region Public Methods
    public List<IPlanStep> GetPlanSteps(WhereClause clause)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Private Methods
    #endregion
}