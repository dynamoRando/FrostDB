using FrostDB;
using System;

public class UpdateStatementElement
{
    #region Private Fields
    #endregion

    #region Public Properties
    public string RawString { get; set; }
    public string RawStringWithWhitespace { get; set; }
    public string ColumnName { get; set; }
    public string TableName { get; set; }
    public string Operator { get; set; }
    public string Value { get; set; }
    public Table Table { get; set; }
    public Column Column { get; set; }
    public Database Database { get; set; }
    public string DatabaseName {get; set;}
    #endregion

    #region Constructors
    public UpdateStatementElement() { }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}