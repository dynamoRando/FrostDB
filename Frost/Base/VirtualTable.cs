using FrostDB.Base;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Base.Extensions;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Runtime.Serialization;

namespace FrostDB.Base
{
    /*
    [Serializable]
    public class VirtualTable : ITable<Column, Row>, IVirtualTable,
        ISerializable
    {
        #region Private Fields
        private BaseDatabase _database;
        private ConcurrentBag<RowReference> _rows;
        private Guid? _id;
        private string _name;
        private List<Column> _columns;
        #endregion

        #region Public Properties
        public List<Column> Columns => _columns;
        public Guid? Id => _id;
        public string Name => _name;
        public List<Row> Rows => GetRows();

        public Guid? DatabaseId { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public VirtualTable(string tableName, List<Column> columns, Database database)
        {
            _database = database;
            _rows = new ConcurrentBag<RowReference>();
        }

        protected VirtualTable(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _id = (Guid?)serializationInfo.GetValue("Id", typeof(Guid));
            _name = (string)serializationInfo.GetValue("Name", typeof(string));
            _columns = (List<Column>)serializationInfo.GetValue
                ("Columns", typeof(List<Column>));
            _rows = (ConcurrentBag<RowReference>)serializationInfo.GetValue
                ("Rows", typeof(ConcurrentBag<RowReference>));
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

        public void UpdateRow(Row oldRow, Row newRow)
        {
            throw new NotImplementedException();
        }

        public List<Row> GetRows(string queryString)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private List<Row> GetRows()
        {
            var rowData = new ConcurrentBag<Row>();

            Parallel.ForEach(_rows, (reference) =>
            {
                Task.Run(() => rowData.Add(reference.GetData()));
            });

            return rowData.ToList();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id.Value, typeof(Guid));
            info.AddValue("Columns", Columns, typeof(List<Column>));
            info.AddValue("Name", Name, typeof(string));
            info.AddValue("Rows", Name, typeof(ConcurrentBag<RowReference>));
        }

        public Column GetColumn(Guid? id)
        {
            throw new NotImplementedException();
        }


        #endregion


    }
*/
}
