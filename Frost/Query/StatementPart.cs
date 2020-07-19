using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StatementPart
{
    #region Public Properties
    public string Text { get; set; }
    public string TextWithWhiteSpace { get; set; }
    // if the text can be broken down into elements
    public List<string> StatementElements { get; set; }
    // the function that the statement came from
    public string StatementOrigin { get; set; }
    // the text above the current statement
    public string StatementParent { get; set; }
    public string StatementParentWithWhiteSpace { get; set; }
    public string StatementGrandParent { get; set; }
    public string StatementGrandParentWithWhiteSpace { get; set; }
    public string StatementTableName { get; set; }
    public string StatementColumnName { get; set; }
    public string StatementOperator { get; set; }
    public string StatementValue { get; set; }
    #endregion

    #region Constructors
    public StatementPart()
    {
        StatementElements = new List<string>();
    }

    public StatementPart(string text, List<string> elements)
    {
        Text = text;
        StatementElements = elements;
    }
    #endregion

    #region Public Methods
    public void ParseStatementPart()
    {
        if (!string.IsNullOrEmpty(TextWithWhiteSpace))
        {
            var items = TextWithWhiteSpace.Split(' ').ToList();
            if (items.Count == 3)
            {
                StatementColumnName = items[0];
                StatementOperator = items[1];
                StatementValue = items[2];
            }
        }
    }
    #endregion

    #region Private Methods
    #endregion
}
