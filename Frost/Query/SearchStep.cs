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
        var rows = new List<Row>();
        var tableName = Part.StatementTableName;
        var columnName = Part.StatementColumnName;
        var operation = Part.StatementOperator;
        var value = Part.StatementValue;

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

                    if (type == Type.GetType("System.Int32"))
                    {
                        rows = CompareInt(operation, value, table);
                    }
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
    private List<Row> CompareInt(string operation, string value, Table table)
    {
        var result = new List<Row>();

        foreach(var row in table.Rows)
        {
            if (operation.Equals(">"))
            {
                int item = Convert.ToInt32(value);
                var rowdata = row.Get(_process);
                rowdata.Values.ForEach(value => 
                { 
                    if (value.ColumnName.Equals(Part.StatementColumnName))
                    {
                        if (Convert.ToInt32(value.Value) > item)
                        {
                            result.Add(rowdata);
                        }
                    }
                });
            }
        }

        return result;
    }
    #endregion
}
