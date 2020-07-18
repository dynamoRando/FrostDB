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

    public string GetResultText()
    {
        var item = string.Empty;
        item += $"Executing SearchStep:" + Environment.NewLine;
        item += $"SearchStep Id: {Id.ToString()}" + Environment.NewLine;
        item += $"Executing Search: {Part.TextWithWhiteSpace}" + Environment.NewLine;

        return item;
    }
    #endregion
}
