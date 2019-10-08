﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class Table : ITable<Column, Row>
    {
        #region Private Fields
        private List<Column> _columns;
        private List<Row> _rows;
        private Guid? _id;
        private string _name;
        private Database _database;
        #endregion

        #region Public Properties
        public List<Column> Columns => _columns;
        public List<Row> Rows => _rows;
        public Guid? Id => _id;
        public string Name => _name;

        #endregion

        #region Events
        #endregion

        #region Constructors
        public Table(string name, List<Column> columns, Database database)
        {
            _name = name;
            _id = Guid.NewGuid();
            _rows = new List<Row>();
            _columns = columns;
            _database = database;
        }
        #endregion

        #region Public Methods
        public void AddRow(Row row)
        {
            throw new NotImplementedException();
        }

        public void DeleteRow(Row row)
        {
            throw new NotImplementedException();
        }

        public Row GetNewRow()
        {
            throw new NotImplementedException();
        }

        public bool HasRow(Row row)
        {
            throw new NotImplementedException();
        }

        public bool HasRow(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void UpdateRow(Row row)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
