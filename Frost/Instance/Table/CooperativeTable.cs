using FrostDB.Base;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Instance
{
    public class CooperativeTable : Table
    {
        #region Private Fields
        private Base.Database _database;
        #endregion

        #region Public Properties
        #endregion

        #region Events
        #endregion

        #region Constructors
        public CooperativeTable(string tableName, List<Column> columns, Base.Database database) : base(tableName, columns, database)
        {
            _database = database;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        public Row GetRow()
        {
            // this is just an example, should use the actual row id of the data
            Guid id = Guid.NewGuid(); 
            var data = _database.Manager.Inbox.GetInboxMessageDataAsync(id);

            return (Row)data.Result;
        }
        #endregion




    }
}
