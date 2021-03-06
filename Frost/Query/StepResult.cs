﻿using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;

public class StepResult : IPlanResult
{
    #region Public Properties
    public List<Row> Rows { get; set; }
    public bool IsValid { get; set; }
    public string ErrorMessage { get; set; }
    public int RowsAffected { get; set; }
    #endregion

    #region Constructors
    public StepResult()
    {
        Rows = new List<Row>();
        IsValid = true;
        RowsAffected = 0;
    }

    public StepResult(List<Row> rows)
    {
        Rows = rows;
    }
    #endregion
}
