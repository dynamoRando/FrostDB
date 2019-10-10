﻿using FrostDB.Base;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Base.Extensions;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace FrostDB.Base
{
    public class VirtualTable : ITable<Column, Row>, IVirtualTable
    {
        #region Private Fields
        private Database _database;
        #endregion

        #region Public Properties
        public List<Column> Columns => throw new NotImplementedException();
        public Guid? Id => throw new NotImplementedException();
        public string Name => throw new NotImplementedException();
        public ConcurrentBag<RowReference> Rows { get; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public VirtualTable(string tableName, List<Column> columns, Database database)
        {
            _database = database;
            Rows = new ConcurrentBag<RowReference>();
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

        /// <summary>
        /// Gets the row from remote partial database table
        /// </summary>
        /// <param name="row">A row parameter with expected values</param>
        /// <returns>A row filled with data from remote database table</returns>
        public Row GetRow(Row row)
        {
            // this is just an example, should use the actual row id of the data
            // this is probably wrong
            //var data = _database.Manager.Inbox.GetInboxMessageDataAsync(row.Id);

            //return (Row)data.Result;

            /*
             * var results = _rows.ForEach(r => r.Fetch());
             *  return results.Where(x => x.Values == row.Values).All();
             */

            // example
            return GetRows().Where(r => r.Values == row.Values).First();
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

        public List<Row> GetRows(string condition)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private List<Row> GetRows()
        {
            var rowData = new ConcurrentBag<Row>();

            Parallel.ForEach(Rows, (reference) =>
            {
                Task.Run(() => rowData.Add(reference.GetData()));
            });

            return rowData.ToList();
        }
        #endregion




    }
}