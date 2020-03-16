using FrostCommon.Net;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FrostDB
{
    public class SelectQuery : IQuery
    {
        /*
         * SELECT { ( col1, col2, col3 .. ) }
         * FROM { tableName }
         * WHERE { ( condition 1 ) ... }
         */
        #region Private Fields
        private Process _process;
        private Database _database;
        private Table _table;
        private List<Column> _columns;
        private bool _hasWhereClause;
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
        public SelectQuery(Process process)
        {
            _process = process;
            _columns = new List<Column>();
        }
        #endregion

        #region Public Methods
        public FrostPromptResponse Execute()
        {
            int rowCount = 0;
            string resultString = string.Empty;
            FrostPromptResponse response = new FrostPromptResponse();

            resultString += " ------------ " + Environment.NewLine;

            if (!_hasWhereClause)
            {
                var rows = _table.GetAllRows();
                rows.ForEach(r => 
                {
                    r.Values.ForEach(v => 
                    {
                        resultString += " { " + _table.Columns.Where(c => c.Id == v.ColumnId).First().Name + " : " + v.Value.ToString() + " } ";
                    });

                    rowCount += 1;
                    resultString += Environment.NewLine;
                });
            }

            resultString += " ------------ " + Environment.NewLine;

            response.NumberOfRowsAffected = rowCount;
            response.JsonData = resultString;
            response.Message = "Succeeded";
            response.IsSuccessful = true;

            return response;
        }

        public bool IsValid(string statement)
        {
            _hasWhereClause = CheckHasWhereClause(statement);

            var lines = statement.Split('{', '}');

            bool hasTable = false;
            bool hasColumns = false;

            string columns = string.Empty;
            string tableName = string.Empty;

            if (lines.Count() >= 4)
            {
                ParseLines(lines, out columns, out tableName);

                hasTable = CheckHasTable(tableName);

                if (hasTable)
                {
                    SetTable(tableName);
                    hasColumns = CheckHasColumns(columns);
                }

            }
            else
            {
                return false;   
            }

            return hasTable && hasColumns;
        }
        #endregion

        #region Private Methods
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
        private void ParseLines(string[] lines, out string columns, out string tableName)
        {
            columns = lines[1].Trim();
            tableName = lines[3].Trim(); 
        }

        private bool CheckHasTable(string tableName)
        {
            var result = false;
            if (_process.HasDatabase(DatabaseName))
            {
                SetDatabase();
                result = _database.HasTable(tableName);
            }

            return result;
        }

        private bool CheckHasColumns(string columns)
        {
            bool result = true;

            if (string.Equals(columns , "*"))
            {
                _table.Columns.ForEach(c => _columns.Add(c));
            }
            else
            {
                var columnNames = columns.Split(',');
                foreach(var c in columnNames)
                {
                    if (_table.Columns.Any(x => x.Name == c.Trim()))
                    {
                        _columns.Add(_table.Columns.Where(y => y.Name == c.Trim()).First());
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return result;
        }

        private void SetTable(string tableName)
        {
            _table = _database.GetTable(tableName);
        }

        private void SetDatabase()
        {
            _database = (Database)_process.GetDatabase(DatabaseName);
        }
        #endregion
    }
}
