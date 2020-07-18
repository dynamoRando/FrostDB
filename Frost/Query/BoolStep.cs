using System;
using System.Collections.Generic;
using System.Text;


public class BoolStep : IPlanStep
{
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
    public PlanResult GetResult()
    {
        throw new NotImplementedException();
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
