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
            _id = Guid.NewGuid();
            _store = new BaseStore();
            _rows = new List<BaseRowReference>();
        }

        public BaseTable(string name, List<Column> columns, Guid? database) : this()
        {
            _name = name;
            _columns = columns;
            DatabaseId = database;
        }

        protected BaseTable(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _rows = (List<BaseRowReference>)serializationInfo.GetValue
             ("TableRows", typeof(List<BaseRowReference>));
            DatabaseId = (Guid?)serializationInfo.GetValue
                ("TableDatabaseId", typeof(Guid?));
            _id = (Guid)serializationInfo.GetValue("TableId", typeof(Guid));
            _name = (string)serializationInfo.GetValue("TableName", typeof(string));
            _columns = (List<Column>)serializationInfo.GetValue
                ("TableColumns", typeof(List<Column>));
            _store = (BaseStore)serializationInfo.GetValue
                ("TableStore", typeof(BaseStore));
        }
        #endregion

        #region Public Methods
        public bool IsCooperative()
        {
            return _rows.Any(row => !row.Location.IsLocal());
        }

        public TableSchema GetSchema()
        {
            return new TableSchema(this);
        }

        public Row GetRow(BaseRowReference reference)
        {
            var row = new Row();

            if (reference.Location.IsLocal())
            {
                row = _store.Rows.Where(r => r.Id == reference.RowId).First();
                row.LastAccessed = DateTime.Now;

                EventManager.TriggerEvent(EventName.Row.Read,
                     CreateRowAccessedEventArgs(row));
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

            return new QueryRunner().Execute(parameters, rows);
        }

        public Column GetColumn(Guid? id)
        {
            return Columns.Where(c => c.Id == id).FirstOrDefault();
        }

        public void AddRow(Row row, Location location)
        {
            if (!location.IsLocal())
            {
                Process.AddRemoteRow(row, location);
                _rows.Add(GetNewRowReference(row, location));

                EventManager.TriggerEvent(EventName.Row.Added,
                       CreateRowAddedEventArgs(row));
            }
        }

        public void AddRow(Row row)
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
            info.AddValue("TableId", Id.Value, typeof(Guid?));
            info.AddValue("TableColumns", Columns, typeof(List<Column>));
            info.AddValue("TableName", Name, typeof(string));
            info.AddValue("TableRows", _rows, typeof(List<BaseRowReference>));
            info.AddValue("TableDatabaseId", DatabaseId,
                typeof(Guid?));
            info.AddValue("TableStore", _store,
                typeof(BaseStore));
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
                Table = this,
                Row = row
            };
        }

        private RowAccessedEventArgs CreateRowAccessedEventArgs(Row row)
        {
            return new RowAccessedEventArgs
            {
                DatabaseId = this.DatabaseId,
                Table = this,
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

            return new BaseRowReference(colIds, Id, (Location)Process.GetLocation(), DatabaseId, row.Id);
        }

        private BaseRowReference GetNewRowReference(Row row, Location location)
        {
            var colIds = new List<Guid?>();

            Columns.ForEach(c =>
            {
                colIds.Add(c.Id);
            });

            return new BaseRowReference(colIds, this.Id, location, DatabaseId, row.Id);
        }
        #endregion

    }
}
