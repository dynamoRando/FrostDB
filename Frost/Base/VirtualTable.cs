using FrostDB.Base;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class VirtualTable : ITable<Column, RemoteRow>, IVirtualTable
    {
        #region Private Fields
        private Database _database;
        #endregion

        #region Public Properties
        public List<Column> Columns => throw new NotImplementedException();
        public List<RemoteRow> Rows => throw new NotImplementedException();
        public Guid? Id => throw new NotImplementedException();
        public string Name => throw new NotImplementedException();
        #endregion

        #region Events
        #endregion

        #region Constructors
        public VirtualTable(string tableName, List<Column> columns, Database database)
        {
            _database = database;
        }
        #endregion

        #region Public Methods

        public void AddRow(RemoteRow row)
        {
            throw new NotImplementedException();
        }

        public void DeleteRow(RemoteRow row)
        {
            throw new NotImplementedException();
        }

        public RemoteRow GetNewRow()
        {
            throw new NotImplementedException();
        }

        public Row GetRow()
        {
            // this is just an example, should use the actual row id of the data
            Guid id = Guid.NewGuid();
            var data = _database.Manager.Inbox.GetInboxMessageDataAsync(id);

            return (Row)data.Result;
        }

        public bool HasRow(RemoteRow row)
        {
            throw new NotImplementedException();
        }

        public bool HasRow(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void UpdateRow(RemoteRow row)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion




    }
}
