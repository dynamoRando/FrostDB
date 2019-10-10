using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class PartialDatabase : IDatabase
    {
        #region Private Fields
        private DataManager<PartialDatabase> _manager;
        private string _name;
        #endregion

        #region Public Properties
        public IContract Contract => throw new NotImplementedException();
        public Guid? Id => throw new NotImplementedException();
        public string Name => throw new NotImplementedException();
        List<ITable<Column, IRow>> IDatabase.Tables => throw new NotImplementedException();
        public DataManager<PartialDatabase> Manager { get { return _manager; } }
        #endregion

        #region Events
        #endregion

        #region 
        public PartialDatabase(string databaseName, DataManager<PartialDatabase> manager)
        {
            _name = databaseName;
            _manager = manager;
        }
        #endregion

        #region Public Methods
        public void AddTable(ITable<Column, Row> table)
        {
            throw new NotImplementedException();
        }

        public void AddTable(ITable<Column, IRow> table)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion



    }
}
