using FrostDB.EventArgs;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Base
{
    [Serializable]
    public class BaseTable :
        IBaseTable, ISerializable, IFrostObjectGet, IDBObject
    {
        #region Private Fields
        private BaseStore _store;
        private List<BaseRowReference> _rows;
        private Guid? _id;
        private string _name;
        private List<Column> _columns;
        #endregion

        #region Public Properties
        public List<BaseRowReference> Rows => _rows;
        public Guid? Id => _id;
        public string Name => _name;
        public List<Column> Columns => _columns;
        public Guid? DatabaseId { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public BaseTable()
        {
            _store = new BaseStore();
            _rows = new List<BaseRowReference>();
        }
        protected BaseTable(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        public Row GetRow(BaseRowReference reference)
        {
            var row = new Row();

            if (reference.Location.IsLocal())
            {
                row = _store.Rows.Where(r => r.Id == reference.RowId).First();
            }
            else
            {
                row = reference.Get();
            }

            return row;
        }
        public List<Row> GetRows(string queryString)
        {
            var rows = new List<Row>();
            _rows.ForEach(row => { rows.Add(row.Get()); });

            var parameters = new List<RowValueQueryParam>();

            if (QueryParser.IsValidQuery(queryString, this))
            {
                parameters = QueryParser.GetParameters(queryString, this);
            }

            var query = new QueryRunner();
            var results = query.Execute(parameters, rows);

            return results;
        }
        public Column GetColumn(Guid? id)
        {
            return Columns.Where(c => c.Id == id).FirstOrDefault();
        }

        public void AddRow(Row row, Location location)
        {
            throw new NotImplementedException();
        }

        public void AddRowLocally(Row row)
        {
            if (RowMatchesTableColumns(row))
            {
                _store.AddRow(row);
                _rows.Add(GetNewRowReference(row));

                EventManager.TriggerEvent(EventName.Row.Added,
                    CreateRowAddedEventArgs(row));
            }
        }
        public Row GetNewRow()
        {
            List<Guid?> ids = new List<Guid?>();
            this.Columns.ForEach(c => ids.Add(c.Id));

            return new Row(ids, this.Id);
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private bool RowMatchesTableColumns(Row row)
        {
            bool isMatch = true;

            row.ColumnIds.ForEach(c =>
            {
                var x = GetColumn(c);

                isMatch = this.Columns.Any(tc => tc.Name == x.Name &&
                tc.DataType == x.DataType);
            });

            if (!(row.ColumnIds.Count == this.Columns.Count))
            {
                isMatch = false;
            }

            return isMatch;
        }

        private RowAddedEventArgs CreateRowAddedEventArgs(Row row)
        {
            return new RowAddedEventArgs
            {
                DatabaseId = this.DatabaseId,
                BaseTable = this,
                Row = row
            };
        }

        private BaseRowReference GetNewRowReference(Row row)
        {
            var colIds = new List<Guid?>();

            Columns.ForEach(c =>
            {
                colIds.Add(c.Id);
            });

            return new BaseRowReference(colIds, Id, (Location)Process.GetLocation());
        }

        private BaseRowReference GetNewRowReference(Row row, Location location)
        {
            var colIds = new List<Guid?>();

            Columns.ForEach(c =>
            {
                colIds.Add(c.Id);
            });

            return new BaseRowReference(colIds, this.Id, location);
        }
        #endregion

    }
}
