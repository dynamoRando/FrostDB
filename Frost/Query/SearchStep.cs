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
        result.IsValid = true;
        result.ErrorMessage = string.Empty;

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

                    if (type == Type.GetType("System.String"))
                    {
                        rows = CompareString(operation, value, table);
                    }

                    if (type == Type.GetType("System.DateTime"))
                    {
                        rows = CompareDate(operation, value, table);
                    }
                    if (type == Type.GetType("System.Single"))
                    {
                        rows = CompareSingle(operation, value, table);
                    }
                }
                else
                {
                    result.IsValid = false;
                    result.ErrorMessage = "Column Not Found";
                }
            }
            else
            {
                result.IsValid = false;
                result.ErrorMessage = "Table Not Found";
            }
        }
        else
        {
            result.IsValid = false;
            result.ErrorMessage = "Database Not Found";
        }

        result.Rows = rows;
        return result;
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
    private List<Row> CompareSingle(string operation, string value, Table table)
    {
        var result = new List<Row>();
        double item = Convert.ToSingle(value);

        foreach (var row in table.Rows)
        {
            var rowdata = row.Get(_process);
            rowdata.Values.ForEach(value =>
            {
                if (value.ColumnName.Equals(Part.StatementColumnName))
                {
                    if (operation.Equals(">"))
                    {
                        if (Convert.ToSingle(value.Value) > item)
                        {
                            result.Add(rowdata);
                        }
                    }

                    if (operation.Equals("<"))
                    {
                        if (Convert.ToSingle(value.Value) < item)
                        {
                            result.Add(rowdata);
                        }
                    }

                    if (operation.Equals("="))
                    {
                        if (Convert.ToSingle(value.Value) == item)
                        {
                            result.Add(rowdata);
                        }
                    }

                }
            });
        }

        return result;
    }
    private List<Row> CompareDate(string operation, string value, Table table)
    {
        var result = new List<Row>();
        DateTime item = Convert.ToDateTime(value);

        foreach (var row in table.Rows)
        {
            var rowdata = row.Get(_process);
            rowdata.Values.ForEach(value =>
            {
                if (value.ColumnName.Equals(Part.StatementColumnName))
                {
                    if (operation.Equals(">"))
                    {
                        if (Convert.ToDateTime(value.Value) > item)
                        {
                            result.Add(rowdata);
                        }
                    }

                    if (operation.Equals("<"))
                    {
                        if (Convert.ToDateTime(value.Value) < item)
                        {
                            result.Add(rowdata);
                        }
                    }

                    if (operation.Equals("="))
                    {
                        if (Convert.ToDateTime(value.Value) == item)
                        {
                            result.Add(rowdata);
                        }
                    }

                }
            });
        }

        return result;
    }
    private List<Row> CompareString(string operation, string value, Table table)
    {
        var result = new List<Row>();
        string item = value;

        foreach (var row in table.Rows)
        {
            var rowdata = row.Get(_process);
            rowdata.Values.ForEach(value =>
            {
                if (value.ColumnName.Equals(Part.StatementColumnName))
                {
                    if (operation.Equals("="))
                    {
                        if (Convert.ToString(value.Value) == item)
                        {
                            result.Add(rowdata);
                        }
                    }

                }
            });
        }

        return result;
    }
    private List<Row> CompareInt(string operation, string value, Table table)
    {
        var result = new List<Row>();
        int item = Convert.ToInt32(value);

        foreach (var row in table.Rows)
        {
            var rowdata = row.Get(_process);
            rowdata.Values.ForEach(value =>
            {
                if (value.ColumnName.Equals(Part.StatementColumnName))
                {
                    if (operation.Equals(">"))
                    {
                        if (Convert.ToInt32(value.Value) > item)
                        {
                            result.Add(rowdata);
                        }
                    }

                    if (operation.Equals("<"))
                    {
                        if (Convert.ToInt32(value.Value) < item)
                        {
                            result.Add(rowdata);
                        }
                    }

                    if (operation.Equals("="))
                    {
                        if (Convert.ToInt32(value.Value) == item)
                        {
                            result.Add(rowdata);
                        }
                    }

                }
            });
        }

        return result;
    }
    #endregion
}
