using System;
using System.Collections.Generic;
using System.Text;

public class QueryPlan
{
    #region Private Fields
    #endregion

    #region Public Properties
    public List<IPlanStep> Steps { get; set; }
    #endregion

    #region Constructors
    public QueryPlan()
    {
        Steps = new List<IPlanStep>();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}
