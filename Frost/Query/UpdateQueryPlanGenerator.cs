using System;
using FrostDB;

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
         throw new NotImplementedException();
     }
    #endregion

    #region Private Methods
    #endregion
}