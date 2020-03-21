using FrostCommon.Net;
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
        private const int MINIMUM_LINE_COUNT = 5;
        private Process _process;
        private Database _database;
        private Table _table;
        private List<InsertQueryParam> _params;
        private string _participant;
        private bool _isLocalQuery = false;
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
        public FrostPromptResponse Execute()
        {
            FrostPromptResponse result = new FrostPromptResponse();

            if (_isLocalQuery)
            {
                InsertRowLocally();

                result = CreateSuccessResponse();
            }
            else
            {
                // TO DO: Need to write this logic
                result = new FrostPromptResponse();
                result.IsSuccessful = false;
                result.Message = "Cannot insert for remote participant at this time";
                result.NumberOfRowsAffected = 0;
                result.JsonData = string.Empty;
            }

            return result;
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

            if (lines.Count() >= MINIMUM_LINE_COUNT)
            {

                string tableName = string.Empty;
                string items = string.Empty;
                string participant = string.Empty;

                ParseStatement(lines, out tableName, out items, out participant);

                hasTable = CheckHasTable(tableName);

                if (hasTable)
                {
                    TableName = tableName;
                    _table = _database.GetTable(tableName);
                    syntaxCorrect = CheckColumnInsertCorrect(items);
                }

                if (syntaxCorrect)
                {
                    hasParticipant = CheckHasParticipant(participant);
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
        private static FrostPromptResponse CreateSuccessResponse()
        {
            FrostPromptResponse result = new FrostPromptResponse();
            result.IsSuccessful = true;
            result.Message = "Insert succeeded";
            result.NumberOfRowsAffected = 1;
            result.JsonData = string.Empty;
            return result;
        }

        private void InsertRowLocally()
        {
            var form = _table.GetNewRowForLocal();

            foreach (var p in _params)
            {
                form.Row.AddValue(p.Column.Id, p.Value, p.ColumnName, p.Column.DataType);
            }

            _table.AddRow(form);
        }

        private void ParseStatement(string[] lines, out string tableName, out string items, out string participant)
        {
            tableName = lines[1].Trim();
            items = lines[3].Trim();
            participant = lines[5].Trim();
        }

        private bool CheckHasTable(string tableName)
        {
            var result = false;
            if (_process.HasDatabase(DatabaseName))
            {
                _database = (Database)_process.GetDatabase(DatabaseName);
                result = _database.HasTable(tableName);
            }

            return result;
        }
        private bool CheckHasParticipant(string participantString)
        {
            var value = participantString.Trim();

            if (string.Equals(value, "local", StringComparison.OrdinalIgnoreCase))
            {
                _participant = value;
                _isLocalQuery = true;
                return true;
            }
            else
            {
                var items = value.Split(":");
                if (items.Count() >= 2)
                {
                    var ipAddress = value[0].ToString();
                    var portNumber = value[1].ToString();
                    if (_database.AcceptedParticipants.Any(p => p.Location.IpAddress == ipAddress && p.Location.PortNumber == Convert.ToInt32(portNumber))) 
                    {
                        _isLocalQuery = false;
                        return true;
                    }
                }
            }

            return false;
        }
        private bool CheckHasColumns(string columnList)
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
        private bool CheckColumnInsertCorrect(string statement)
        {
            bool hasColumns = false;
            bool valuesCorrect = false;

            string columns = string.Empty;
            string values = string.Empty;

            var items = statement.Split('(', ')');

            if (items.Count() >= 5)
            {
                columns = items[1].Trim();
                hasColumns = CheckHasColumns(columns);
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
