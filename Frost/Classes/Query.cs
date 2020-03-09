using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Enum;

namespace FrostDB
{
    public class Query
    {
        #region Private Fields
        private Process _process;
        #endregion

        #region Public Properties
        public string DatabaseName { get; set; }
        public QueryType QueryType { get; set; }
        public string TableName { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Query(Process process)
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public bool TryParseSelect(string selectStatement, Query query)
        {
            /*
             * SELECT [ cols ] FROM TABLE
             * WHERE condition
             */

            throw new NotImplementedException();
        }

        public bool TryParseInsert(string insertStatement, Query query)
        {
            bool isSuccessful = false;
            bool hasTable = false;

            var tableName = insertStatement.Split('{', '}')[0];
            var particpant = insertStatement.Split('{', '}')[1];
            var items = insertStatement.Split('(', ')');

            var columns = items[0];
            var values = items[1];

            if (_process.HasDatabase(query.DatabaseName))
            {
                var db = _process.GetDatabase(query.DatabaseName);
                hasTable = db.HasTable(tableName);
            }



            /*
             * INSERT INTO { table } 
             * VALUES { vals } 
             * FOR PARTICIPANT { participant }
             */
            throw new NotImplementedException();
        }

        public bool TryParseUpdate(string updateStatement, Query query)
        {
            throw new NotImplementedException();
        }

        public bool TryParseDelete(string deleteStatement, Query query)
        {
            throw new NotImplementedException();
        }

        public bool TryParseDatabase(string useStatement)
        {
            var items = useStatement.Split(" ");
            var dbName = items[1];

            if (_process.HasDatabase(dbName))
            {
                DatabaseName = dbName;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetQueryType(string statement)
        {
            if (statement.Contains(QueryKeywords.Select))
            {
                this.QueryType = QueryType.Select;
            }

            if (statement.Contains(QueryKeywords.Insert))
            {
                this.QueryType = QueryType.Insert;
            }

            if (statement.Contains(QueryKeywords.Update))
            {
                this.QueryType = QueryType.Update;
            }

            if (statement.Contains(QueryKeywords.Delete))
            {
                this.QueryType = QueryType.Delete;
            }
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
