using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class InsertQuery : IQuery
    {
        #region Private Fields
        private Process _process;
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
        }
        #endregion

        #region Public Methods
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public bool TryParse(string statement)
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

            var tableName = lines[1].Trim();
            var items = lines[3].Trim();
            var particpant = lines[5].Trim();
            

            var columns = items[0];
            var values = items[1];

            if (_process.HasDatabase(DatabaseName))
            {
                var db = _process.GetDatabase(DatabaseName);
                hasTable = db.HasTable(tableName);
            }



           
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
