using FrostDB.Base;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Instance.Database
{
    public class HostDatabase : Base.Database
    {
        #region Private Fields
        private string _name;
        private List<ITable<Column>> _tables;
        private IContract _contract;
        private DatabaseManager _manager;
        #endregion

        #region Public Properties
        #endregion

        #region Events
        #endregion

        #region Constructors
        public HostDatabase(string databaseName, DatabaseManager manager) : base(databaseName, manager)
        {
            _tables = new List<ITable<Column>>();
            _name = databaseName;
            _manager = manager;
        }
        #endregion

        #region Public Methods
        public void AddTable(CooperativeTable table)
        {
            _tables.Add((ITable<Column>)table);
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
