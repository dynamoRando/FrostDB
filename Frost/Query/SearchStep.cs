using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;


public class SearchStep : IPlanStep
{
    #region Private Fields
    StatementPart _part;
    Process _process;
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
    public PlanResult GetResult(Process process, string databaseName)
    {
        _process = process;

        var result = new PlanResult();
        var tableName = Part.StatementTableName;
        var columnName = Part.StatementColumnName;
        var operation = Part.StatementOperator;

        if (_process.HasDatabase(databaseName))
        {
            var db = _process.GetDatabase(databaseName);
            if (db.HasTable(tableName))
            {
                var table = db.GetTable(tableName);
                if (table.HasColumn(columnName))
                {
                    var column = table.GetColumn(columnName);
                    var type = column.DataType;
                }
            }
        }

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

    #region Private Methods
    #endregion
}
