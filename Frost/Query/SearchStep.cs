using System;
using System.Collections.Generic;
using System.Text;


public class SearchStep : IPlanStep
{
    #region Private Fields
    StatementPart _part;
    #endregion

    #region Public Properties
    public Guid Id { get; set; }
    public int Level { get; set; }
    public StatementPart Part => _part;
    #endregion

    #region Constructors
    public SearchStep() 
    {
        Id = Guid.NewGuid();
    }
    public SearchStep(StatementPart part) : this()
    {
        _part = part;
    }
    #endregion

    #region Public Methods
    public PlanResult GetResult()
    {
        throw new NotImplementedException();
    }

    public void GetResultText()
    {
        Console.WriteLine($"Executing SearchStep:");
        Console.WriteLine($"SearchStep Id: {Id.ToString()}");
        Console.WriteLine($"Executing Search: {Part.TextWithWhiteSpace}");
    }
    #endregion
}
