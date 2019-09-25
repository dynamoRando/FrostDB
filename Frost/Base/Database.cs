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
        private List<ITable> _tables;
        #endregion

        #region Public Properties
        public Guid Id { get; }
        public string Name { get { return _name; } }
        public List<ITable> Tables { get { return _tables; } }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Database(string name)
        {
            Id = Guid.NewGuid();
            _name = name;
            _tables = new List<ITable>();
        }
        #endregion

        #region Public Methods
        public void AddTable(ITable table)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
