using FrostDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


public class BoolStep : IPlanStep
{
    #region Private Properties
    private Process _process;
    private string _databaseName;
    #endregion

    #region Public Properties
    public Guid Id { get; set; }
    public int Level { get; set; }
    public IPlanStep InputOne { get; set; }
    public IPlanStep InputTwo { get; set; }
    public string Boolean { get; set; }
    public string BoolStepTextWithWhiteSpace { get; set; }
    #endregion

    #region Constructors
    public BoolStep()
    {
        BoolStepTextWithWhiteSpace = string.Empty;
        Boolean = string.Empty;
        Id = Guid.NewGuid();
    }
    #endregion

    #region Public Methods
    public StepResult GetResult(Process process, string databaseName)
    {
        _process = process;
        _databaseName = databaseName;

        var rows = new List<Row>();
        var result = new StepResult();

        var result1 = InputOne.GetResult(process, databaseName);
        var result2 = InputTwo.GetResult(process, databaseName);

        if (Boolean.Equals("AND"))
        {
            // return rows where the condition is true for both parts
            rows = result1.Rows.Intersect(result2.Rows).ToList();
        }

        if (Boolean.Equals("OR"))
        {
            // union returns both rows, removing duplicates
            rows = result1.Rows.Union(result2.Rows).ToList();
        }

        result.Rows = rows;

        return result;
    }

    public string GetResultText()
    {
        var item = string.Empty;

        item += "Executing BoolStep:" + Environment.NewLine;

        item += $"BoolStep Id: {Id.ToString()}" + Environment.NewLine;

        item += $"BoolStep Executing Input 1:" + Environment.NewLine;
        item += InputOne.GetResultText() + Environment.NewLine;
        item += $"BoolStep Executing Input 2:" + Environment.NewLine;
        item += InputTwo.GetResultText() + Environment.NewLine;

        item += $"Combining Results with {Boolean}" + Environment.NewLine;

        return item;
    }
    #endregion

    #region Private Methods
   

    #endregion
}
