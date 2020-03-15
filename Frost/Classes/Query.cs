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

        #endregion

        #region Private Methods
        #endregion


    }
}
