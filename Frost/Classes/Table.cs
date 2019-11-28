using FrostDB.EventArgs;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using FrostDB.Enum;

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
        private ContractValidator _contractValidator;
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
            _contractValidator = new ContractValidator(ProcessReference.GetDatabase(DatabaseId).Contract, DatabaseId);
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

        public bool HasCooperativeData()
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

        public void AddRow(RowForm form)
        {
            // we do a schema check
            if (RowMatchesTableColumns(form.Row))
            {
                // we make sure this action is okay with the defined contract
                if (_contractValidator.ActionIsValidForParticipant(TableAction.AddRow, form.Participant))
                {
                    // we make sure the participant accepts this action if they're remote
                    if (form.Participant.AcceptsAction(TableAction.AddRow))
                    {
                        if (form.Participant.Location.IsLocal() || form.Participant.IsDatabase(DatabaseId))
                        {
                            AddRowLocally(form.Row);
                        }
                        else
                        {
                            AddRowRemotely(form);
                        }
                    }
                }
            }
        }

        public RowForm GetNewRow(Guid? participantId)
        {
            RowForm form = null;

            var db = ProcessReference.GetDatabase(DatabaseId);
            if (db.HasParticipant(participantId))
            {
                form = new RowForm(GetNewRow(), db.GetParticipant(participantId));
            }

            return form;
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
        private Row GetNewRow()
        {
            List<Guid?> ids = new List<Guid?>();
            this.Columns.ForEach(c => ids.Add(c.Id));

            return new Row(ids, this.Id);
        }

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

        private void AddRowRemotely(RowForm form)
        {
            //Client.SaveRow(participant.Location, DatabaseId, row.TableId, row);
            _rows.Add(GetNewRowReference(form.Row, form.Participant.Location));

            EventManager.TriggerEvent(EventName.Row.Added_Remotely,
                   CreateRowAddedEventArgs(form.Row));
        }

        private void AddRowLocally(Row row)
        {
            _store.AddRow(row);
            _rows.Add(GetNewRowReference(row));

            EventManager.TriggerEvent(EventName.Row.Added,
                CreateRowAddedEventArgs(row));
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
