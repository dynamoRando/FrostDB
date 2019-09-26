using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class Database : IDatabase
    {
        #region Private Fields
        private string _name;
        private List<ITable<Column>> _tables;
        private IContract _contract;
        private DatabaseManager _manager;
        #endregion

        #region Public Properties
        public Guid Id { get; }
        public string Name { get { return _name; } }
        public List<ITable<Column>> Tables { get { return _tables; } }
        public IContract Contract { get { return _contract; } }
        public DatabaseManager Manager { get { return _manager; } }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Database(string name, DatabaseManager manager)
        {
            Id = Guid.NewGuid();
            _name = name;
            _tables = new List<ITable<Column>>();
            _manager = manager;
        }
        #endregion

        #region Public Methods
        public void AddTable(ITable<Column> table)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
