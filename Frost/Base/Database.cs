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
        private List<ITable<Column, IRow>> _tables;
        private IContract _contract;
        private IDatabaseManager<Database> _manager;
        #endregion

        #region Public Properties
        public Guid? Id { get; }
        public string Name { get { return _name; } }
        public List<ITable<Column, IRow>> Tables { get { return _tables; } }
        public IContract Contract { get { return _contract; } }
        public IDatabaseManager<Database> Manager { get { return _manager; } }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Database(string name, IDatabaseManager<Database> manager)
        {
            Id = Guid.NewGuid();
            _name = name;
            _tables = new List<ITable<Column, IRow>>();
            _manager = manager;
        }
        public Database(string name, DatabaseManager manager, Guid id) : this(name, manager)
        {
            Id = id;
        }
        #endregion

        #region Public Methods
        public void AddTable(ITable<Column, Row> table)
        {
            throw new NotImplementedException();
        }

        public void AddTable(ITable<Column, RemoteRow> table)
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
