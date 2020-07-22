using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using FrostDB;
using FrostCommon;
using FrostCommon.Net;

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
        foreach (var step in plan.Steps)
        {
            ExecuteStep(step, plan.DatabaseName);
        }
        throw new NotImplementedException();
    }
    #endregion

    #region Private Methods
    private void ExecuteStep(IPlanStep step, string databaseName)
    {
        var result = step.GetResult(_process, databaseName);
        
        throw new NotImplementedException();
    }
    #endregion

}

