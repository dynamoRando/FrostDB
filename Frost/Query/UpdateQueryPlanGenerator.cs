using System;
using FrostDB;
using System.Collections;
using System.Collections.Generic;

public class UpdateQueryPlanGenerator
{
    #region Private Fields
    private Process _process;
    
    #endregion
    
    #region Public Properties
    #endregion

    #region Constructors
    public UpdateQueryPlanGenerator(Process process)
    {
        _process = process;
    }
    #endregion

    #region Public Methods
     public QueryPlan GeneratePlan(UpdateStatement statement)
     {
         var result = new QueryPlan();
         statement.ParseElements();
         result.Steps.AddRange(GetWhereClauseSteps(statement));
         result.Steps.AddRange(GetUpdateSteps(statement));
         return result;
     }
    #endregion

    #region Private Methods
    private List<IPlanStep> GetUpdateSteps(UpdateStatement statement)
    {
        throw new NotImplementedException();
    }
    private List<IPlanStep> GetWhereClauseSteps(UpdateStatement statement)
    {
        if (statement.HasWhereClause)
        {
            throw new NotImplementedException();
        }
        throw new NotImplementedException();
    }
    #endregion
}