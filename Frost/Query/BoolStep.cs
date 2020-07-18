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

    public void GetResultText()
    {
        Console.WriteLine("Executing BoolStep:");
        Console.WriteLine($"BoolStep Id: {Id.ToString()}");

        Console.WriteLine($"BoolStep Executing Input 1:");
        InputOne.GetResultText();
        Console.WriteLine($"BoolStep Executing Input 2:");
        InputTwo.GetResultText();

        Console.WriteLine($"Combining Results with {Boolean}");

    }
    #endregion

    #region Private Methods
    #endregion
}
