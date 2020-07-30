using FrostDB;
using System;

public class InsertQueryPlanGenerator
{
    #region Private Fields
    Process _process;
    #endregion

    #region Public Properties
    #endregion

    #region Constructors
    public InsertQueryPlanGenerator()
    {

    }

    public InsertQueryPlanGenerator(Process process)
    {
        _process = process;
    }
    #endregion

    #region Public Methods
    public QueryPlan GeneratePlan(InsertStatement statement)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region  Private Methods
    #endregion
}