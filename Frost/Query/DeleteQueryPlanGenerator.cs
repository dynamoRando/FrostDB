using FrostDB;
using System;

public class DeleteQueryPlanGenerator
{
    #region Private Fields
    private Process _process;
    #endregion

    #region Public Properties
    #endregion

    #region Constructors
    public DeleteQueryPlanGenerator(Process process)
    {
        _process = process;
    }
    #endregion

    #region Public Methods
    public QueryPlan GeneratePlan(DeleteStatement statement)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Private Methods
    #endregion
}