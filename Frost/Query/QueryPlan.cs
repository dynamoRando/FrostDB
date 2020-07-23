﻿using System;
using System.Collections.Generic;
using System.Text;

public class QueryPlan
{
    #region Private Fields
    #endregion

    #region Public Properties
    public List<IPlanStep> Steps { get; set; }
    public string DatabaseName { get; set; }
    public bool IsValid { get; set; }
    public string ErrorMessage { get; set; }
    public List<string> Columns { get; set; }
    #endregion

    #region Constructors
    public QueryPlan()
    {
        Steps = new List<IPlanStep>();
        Columns = new List<string>();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}
