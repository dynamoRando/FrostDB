using FrostCommon.Net;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrostDB
{
    internal class DeleteQuery : IQuery
    {
        #region Private Fields
        private const int MINIMUM_LINE_COUNT = 3;
        private Process _process;
        private bool _hasWhereClause;
        private Database _database;
        private Table _table;
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
        public DeleteQuery(Process process)
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public bool CanWalk(string statement, TSqlWalker walker)
        {
            throw new NotImplementedException();
        }
        public FrostPromptResponse Execute()
        {
            FrostPromptResponse response = new FrostPromptResponse();
            string resultString = string.Empty;
            int totalRows = 0;

            if (!_hasWhereClause)
            {
                totalRows = _table.RemoveAllRows();
            }

            resultString += " ------------ " + Environment.NewLine;

            response.NumberOfRowsAffected = totalRows;
            response.JsonData = resultString;
            response.Message = "Delete Succeeded";
            response.IsSuccessful = true;

            return response;
        }

        public bool IsValid(string statement)
        {
            /*
            * DELETE 
            * FROM { tableName }
            * WHERE { ( condition 1 ) ... }
            */

            _hasWhereClause = CheckHasWhereClause(statement);

            bool hasTable = false;
            bool whereClauseCorrect = false;

            string tableName = string.Empty;
            string whereClause = string.Empty;

            var lines = statement.Split('{', '}');

            if (lines.Count() >= MINIMUM_LINE_COUNT)
            {
                ParseLines(lines, out tableName, out whereClause);
                if (!string.IsNullOrEmpty(tableName))
                {
                    hasTable = CheckHasTable(tableName);

                    if (hasTable)
                    {
                        SetTable(tableName);
                        if (_hasWhereClause)
                        {
                            whereClauseCorrect = ValidateWhereClause(whereClause);
                        }
                    }
                }
            }
            else
            {
                return false;
            }

            // TO DO: fix this
            whereClauseCorrect = true;

            return hasTable && whereClauseCorrect;
        }

        public Task<FrostPromptResponse> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private bool ValidateWhereClause(string whereClause)
        {
            // TO DO: Fix this
            return true;
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
        private void ParseLines(string[] lines, out string tableName, out string whereClause)
        {
            tableName = lines[1].Trim();

            if (_hasWhereClause)
            {
                whereClause = string.Empty;
            }

            // TO DO: Need to fix this
            whereClause = string.Empty;
        }

        private void SetTable(string tableName)
        {
            _table = _database.GetTable(tableName);
        }

        private void SetDatabase()
        {
            _database = (Database)_process.GetDatabase(DatabaseName);
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
        #endregion
    }
}
