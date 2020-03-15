using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FrostDB
{
    public class InsertQuery : IQuery
    {
        #region Private Fields
        private Process _process;
        private Database _database;
        private Table _table;
        private List<InsertQueryParam> _params;
        private string _participant;
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
        public InsertQuery(Process process)
        {
            _process = process;
            _params = new List<InsertQueryParam>();
        }
        #endregion

        #region Public Methods
        public void Execute()
        {
            var form = _table.GetNewRowForLocal();
            
            foreach(var p in _params)
            {
                form.Row.AddValue(p.Column.Id, p.Value, p.ColumnName, p.Column.DataType);
            }

            _table.AddRow(form);
        }

        public bool IsValid(string statement)
        { /*
             * INSERT INTO { table } 
             * ( col1, col2, col3 ... )
             * VALUES { val1, val2, val3... } 
             * FOR PARTICIPANT { participant }
             */

            bool hasTable = false;
            bool syntaxCorrect = false;
            bool hasParticipant = false;

            var lines = statement.Split('{', '}');

            if (lines.Count() >= 5)
            {
                var tableName = lines[1].Trim();
                var items = lines[3].Trim();
                var particpant = lines[5].Trim();


                var columns = items[0];
                var values = items[1];

                if (_process.HasDatabase(DatabaseName))
                {
                    _database = (Database)_process.GetDatabase(DatabaseName);
                    hasTable = _database.HasTable(tableName);
                }

                if (hasTable)
                {
                    TableName = tableName;
                    _table = _database.GetTable(tableName);
                    syntaxCorrect = ColumnInsertCorrect(items);
                }

                if (syntaxCorrect)
                {
                    hasParticipant = HasPartcipant(particpant);
                }

            }
            else
            {
                return false;
            }

            return hasParticipant && syntaxCorrect && hasTable;
        }
        #endregion

        #region Private Methods
        private bool HasPartcipant(string participantString)
        {
            var value = participantString.Trim();

            if (string.Equals(participantString, "local", StringComparison.OrdinalIgnoreCase))
            {
                _participant = value;
                return true;
            }

            return false;
        }
        private bool HasColumns(string columnList)
        {
            var columns = columnList.Split(',').ToList();

            foreach(var c in columns)
            {
                if (!_table.HasColumn(c.Trim()))
                {
                    return false;
                }
            }

            return true;
        }
        private bool ColumnInsertCorrect(string statement)
        {
            bool hasColumns = false;
            bool valuesCorrect = false;

            string columns = string.Empty;
            string values = string.Empty;

            var items = statement.Split('(', ')');

            if (items.Count() >= 5)
            {
                columns = items[1].Trim();
                hasColumns = HasColumns(columns);
                if (hasColumns)
                {
                    ParseColumns(columns);
                }

                values = items[3].Trim();
                valuesCorrect = ParseValues(values);
            }
            else
            {
                return false;
            }

            return hasColumns && valuesCorrect;
        }

        private bool ParseValues(string valueList)
        {
            int index = 0;
            var values = valueList.Split(',').ToList();

            if (values.Count() != _params.Count())
            {
                return false;
            }
            else
            {
                foreach(var v in values)
                {
                    string value = v.Trim().Replace("'", "");

                    index += 1;
                    var param = _params.Where(p => p.Index == index).First();
                    var type = param.Column.DataType;

                    switch (true)
                    {
                        case bool _ when type == typeof(int):
                            param.Value = Int32.Parse(value);
                            break;
                        case bool _ when type == typeof(float):
                            param.Value = float.Parse(value);
                            break;
                        case bool _ when type == typeof(DateTime):
                            DateTime dt;
                            if (DateTime.TryParse(value, out dt))
                            {
                                param.Value = dt;
                            }
                            else
                            {
                                return false;
                            }
                            break;
                        case bool _ when type == typeof(string):
                            param.Value = value;
                            break;
                    }
                }
            }

            return true;
        }

        private void ParseColumns(string columnList)
        {
            int index = 0;
            var columns = columnList.Split(',').ToList();

            foreach(var c in columns)
            {
                var param = new InsertQueryParam();
                param.Column = _table.GetColumn(c.Trim());
                index += 1;
                param.Index = index;
                _params.Add(param);
            }

        }
        #endregion
    }
}
