using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class Table : ITable
    {
        #region Private Fields
        private List<IColumn> _columns;
        private List<IRow> _rows;
        private Guid _id;
        private string _name;
        #endregion

        #region Public Properties
        public List<IColumn> Columns => _columns;
        public List<IRow> Rows => _rows;
        public Guid Id => _id;
        public string Name => _name;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Table(string name, List<IColumn> columns)
        {
            _name = name;
            _id = Guid.NewGuid();
            _rows = new List<IRow>();
            _columns = columns;
        }
        #endregion

        #region Public Methods
        public void AddRow(IRow row)
        {
            throw new NotImplementedException();
        }

        public void DeleteRow(IRow row)
        {
            throw new NotImplementedException();
        }

        public IRow GetNewRow()
        {
            throw new NotImplementedException();
        }

        public bool HasRow(IRow row)
        {
            throw new NotImplementedException();
        }

        public bool HasRow(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void UpdateRow(IRow row)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
