using FrostCommon.Net;
using FrostDB.Classes;
using FrostDB.EventArgs;
using FrostDB.Extensions;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FrostDB
{
    public class UpdateQuery : IQuery
    {
        #region Private Fields
        private Process _process;
        private Database _database;
        private Table _table;
        private List<Column> _columns;
        private bool _hasWhereClause;
        private const int MINIMUM_LINE_COUNT = 4;
        private List<UpdateQueryColumnParameters> _parameters;
        private bool _hasTable = false;
        private bool _columnUpdatesCorrect = false;
        #endregion

        #region Public Properties
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public Process Process { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public UpdateQuery(Process process)
        {
            _process = process;
            _columns = new List<Column>();
            _parameters = new List<UpdateQueryColumnParameters>();
        }
        #endregion

        #region Public Methods
        public FrostPromptResponse Execute()
        {
            if (!_hasWhereClause)
            {
                var values = new List<RowValue>();
                _parameters.ForEach(p => values.Add(p.Convert()));
                
                foreach(var row in _table.Rows)
                {
                    _table.UpdateRow(row, values);
                }
            }

            throw new NotImplementedException();
        }

        /*
         * UPDATE { tableName } SET { col 1 = value1, col 2 = value 2 }
         * WHERE { ( condition 1 ) ... }
         * ;
         */
        public bool IsValid(string statement)
        {
            _hasWhereClause = CheckHasWhereClause(statement);

            var lines = statement.Split('{', '}');

            string columns = string.Empty;
            string tableName = string.Empty;

            if (lines.Count() >= MINIMUM_LINE_COUNT)
            {
                ParseLines(lines, out columns, out tableName);
            }

            return _hasTable && _columnUpdatesCorrect;
        }

        public Task<FrostPromptResponse> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private void ParseLines(string[] lines, out string columns, out string tableName)
        {
            columns = string.Empty;
            tableName = string.Empty;

            tableName = lines[1].Trim();
            _hasTable = CheckHasTable(tableName);

            if (!_hasTable)
            {
                return;
            }

            var setValues = lines[3].Trim();
            ParseSet(setValues, out columns);
        }

        private void ParseSet(string setValues, out string columns)
        {
            //SET 
            // { col 1 = value1, col 2 = value 2 }
            columns = string.Empty;
            var para = new List<UpdateQueryColumnParameters>();

            if (!_hasTable)
            {
                return;
            }

            var columnParms = setValues.Split(',');
            var items = columnParms.ToList();

            bool valuesOk = true;

            foreach (var i in items)
            {
                var k = i.Split('=');
                if (k.Count() == 2)
                {
                    var columnName = k[0]; // left side of equals sign
                    var columnValue = k[1]; // right side of equals sign

                    if (_table.HasColumn(columnName))
                    {
                        valuesOk = CheckValues(ref para, columnName, columnValue);
                        if (!valuesOk)
                        {
                            break;
                        }
                    }
                    else
                    {
                        valuesOk = false;
                        break;
                    }
                }
                else
                {
                    valuesOk = false;
                    break;
                }
            }

            _columnUpdatesCorrect = valuesOk;

            if (_columnUpdatesCorrect)
            {
                _parameters = para;
            }

        }

        private bool CheckValues(ref List<UpdateQueryColumnParameters> para, string columnName, string columnValue)
        {
            bool valuesOk = true;
            var column = _table.GetColumn(columnName);
            var dataType = column.DataType;

            switch (true)
            {
                case bool _ when dataType == typeof(int):
                    int intTry = 0;
                    if (!int.TryParse(columnValue, out intTry))
                    {
                        valuesOk = false;
                        break;
                    }
                    else
                    {
                        SetUpdateQueryParameter(ref para, columnName, columnValue, column, dataType);
                    }
                    break;
                case bool _ when dataType == typeof(float):
                    float floatTry = 0.0F;
                    if (!float.TryParse(columnValue, out floatTry))
                    {
                        valuesOk = false;
                        break;
                    }
                    else
                    {
                        SetUpdateQueryParameter(ref para, columnName, columnValue, column, dataType);
                    }
                    break;
                case bool _ when dataType == typeof(DateTime):
                    DateTime dateTry;
                    if (!DateTime.TryParse(columnValue, out dateTry))
                    {
                        valuesOk = false;
                        break;
                    }
                    else
                    {
                        SetUpdateQueryParameter(ref para, columnName, columnValue, column, dataType);
                    }
                    break;
                case bool _ when dataType == typeof(string):
                    SetUpdateQueryParameter(ref para, columnName, columnValue, column, dataType);
                    break;
            }

            return valuesOk;
        }

        private void SetUpdateQueryParameter(ref List<UpdateQueryColumnParameters> para, string columnName, string columnValue, Column column, Type dataType)
        {
            var j = new UpdateQueryColumnParameters();
            j.TableName = _table.Name;
            j.TableId = _table.Id;
            j.ColumnName = columnName;
            j.ColumnType = dataType;
            j.ColumnId = column.Id;
            j.Value = Convert.ChangeType(columnValue, dataType);
            para.Add(j);
        }

        private bool CheckHasTable(string tableName)
        {
            var result = false;
            if (_process.HasDatabase(DatabaseName))
            {
                SetDatabase();
                result = _database.HasTable(tableName);

                if (result)
                {
                    _table = _database.GetTable(tableName);
                }
            }

            return result;
        }

        private bool CheckHasWhereClause(string statement)
        {
            if (statement.Contains(QueryKeywords.Where))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetDatabase()
        {
            _database = (Database)_process.GetDatabase(DatabaseName);
        }

        #endregion
    }
}
