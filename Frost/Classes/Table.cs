using FrostDB.EventArgs;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB
{
    [Serializable]
    public class Table :
        ITable, ISerializable, IFrostObjectGet, IDBObject
    {
        #region Private Fields
        private Store _store;
        private List<RowReference> _rows;
        private Guid? _id;
        private string _name;
        private List<Column> _columns;
        private TableSchema _schema;
        #endregion

        #region Public Properties
        public List<RowReference> Rows => _rows;
        public Guid? Id => _id;
        public string Name => _name;
        public List<Column> Columns => _columns;
        public Guid? DatabaseId { get; set; }
        public TableSchema Schema => _schema;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Table()
        {
            _id = Guid.NewGuid();
            _store = new Store();
            _rows = new List<RowReference>();
        }

        public Table(string name, List<Column> columns, Guid? databaseId) : this()
        {
            _name = name;
            _columns = columns;
            DatabaseId = databaseId;
            _schema = new TableSchema(this);
        }

        protected Table(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _rows = (List<RowReference>)serializationInfo.GetValue
             ("TableRows", typeof(List<RowReference>));
            DatabaseId = (Guid?)serializationInfo.GetValue
                ("TableDatabaseId", typeof(Guid?));
            _id = (Guid)serializationInfo.GetValue("TableId", typeof(Guid));
            _name = (string)serializationInfo.GetValue("TableName", typeof(string));
            _columns = (List<Column>)serializationInfo.GetValue
                ("TableColumns", typeof(List<Column>));
            _store = (Store)serializationInfo.GetValue
                ("TableStore", typeof(Store));
            _schema = (TableSchema)serializationInfo.GetValue
                ("TableSchema", typeof(TableSchema));
        }
        #endregion

        #region Public Methods
        public bool HasRow(Guid? rowId)
        {
            return _rows.Any(r => r.RowId == rowId);
        }

        public Row GetRow(Guid? rowId)
        {
            var row = _rows.Where(r => r.RowId == rowId).First();
            return row.Get();
        }

        public bool IsCooperative()
        {
            return _rows.Any(row => !(row.Participant.Location.IsLocal() ||
            row.Participant.IsDatabase(DatabaseId)));
        }

        public void UpdateSchema()
        {
            _schema = new TableSchema(this);
        }

        public Row GetRow(RowReference reference)
        {
            var row = new Row();

            if (reference.Participant.Location.IsLocal() 
                || reference.Participant.IsDatabase(DatabaseId))
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

        public void AddRow(Row row, Participant participant)
        {
            if (!participant.Location.IsLocal() || !participant.IsDatabase(DatabaseId))
            {
                if (participant.HasAcceptedContract(DatabaseId))
                {
                    Client.SaveRow(participant.Location, DatabaseId, row.TableId, row);
                    _rows.Add(GetNewRowReference(row, participant.Location));

                    EventManager.TriggerEvent(EventName.Row.Added_Remotely,
                           CreateRowAddedEventArgs(row));
                }
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
            info.AddValue("TableRows", _rows, typeof(List<RowReference>));
            info.AddValue("TableDatabaseId", DatabaseId,
                typeof(Guid?));
            info.AddValue("TableStore", _store,
                typeof(Store));
            info.AddValue("TableSchema", _schema,
                typeof(TableSchema));
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

        private RowReference GetNewRowReference(Row row)
        {
            var colIds = new List<Guid?>();

            Columns.ForEach(c =>
            {
                colIds.Add(c.Id);
            });

            return new RowReference(colIds, Id, new Participant(DatabaseId, (Location)
                Process.GetLocation()), DatabaseId, row.Id);
        }

        private RowReference GetNewRowReference(Row row, Location location)
        {
            var colIds = new List<Guid?>();

            Columns.ForEach(c =>
            {
                colIds.Add(c.Id);
            });

            return new RowReference(colIds, this.Id, new Participant(location), DatabaseId, row.Id);
        }
        #endregion

    }
}
