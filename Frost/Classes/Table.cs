using FrostCommon;
using FrostDB.EventArgs;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using FrostDB.Enum;
using FrostDB.Extensions;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Diagnostics;

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
        private Process _process;
        private QueryParser _parser;
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
        public Table(Process process)
        {
            _id = Guid.NewGuid();
            _store = new Store();
            _rows = new List<RowReference>();

            _process = process;

            if (DatabaseId != null)
            {
                _contractValidator = new ContractValidator(_process.GetDatabase(DatabaseId).Contract, DatabaseId);
            }

            _parser = new QueryParser(_process);

        }

        public Table(string name, List<Column> columns, Guid? databaseId, Process process) : this(process)
        {
            _name = name;
            _columns = columns;
            DatabaseId = databaseId;
            _schema = new TableSchema(this);
            _parser = new QueryParser(_process);
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
            _parser = new QueryParser(_process);
        }
        #endregion

        #region Public Methods
        public TableSchema GetSchema()
        {
            var schema = new TableSchema();
            schema.TableName = this.Name;
            schema.TableId = this.Id;
            schema.Columns = this.Columns;

            return schema;
        }
        public void SetProcess(Process process)
        {
            _process = process;
        }
        public bool HasRow(Guid? rowId)
        {
            return _rows.Any(r => r.RowId == rowId);
        }

        public Row GetRow(Guid? rowId)
        {
            var result = new Row();
            var row = new RowReference();

            row = _rows.Where(r => r.RowId == rowId).FirstOrDefault();
            result = _store.Rows.Where(r => r.Id == rowId).FirstOrDefault();

            return result;
        }

        public bool HasCooperativeData()
        {
            return _rows.Any(row => !(row.Participant.Location.IsLocal(_process) ||
            row.Participant.IsDatabase(DatabaseId)));
        }

        public void UpdateSchema()
        {
            _schema = new TableSchema(this);
        }

        public Row GetRow(RowReference reference)
        {
            var row = new Row();

            if (reference.Participant.Location.IsLocal(_process)
                || reference.Participant.IsDatabase(DatabaseId))
            {
                row = _store.Rows.Where(r => r.Id == reference.RowId).First();
                row.LastAccessed = DateTime.Now;

                _process.EventManager.TriggerEvent(EventName.Row.Read,
                     CreateRowAccessedEventArgs(row));
            }
            else
            {
                row = reference.Get(_process);
            }

            return row;
        }

        public List<Row> GetAllRows()
        {
            var result = new List<Row>();

            _rows.ForEach(r => result.Add(r.Get(_process)));

            return result;
        }
        public List<Row> GetRows(string queryString)
        {
            var rows = new List<Row>();
            _rows.ForEach(row => { rows.Add(row.Get(_process)); });

            var parameters = new List<RowValueQueryParam>();

            if (_parser.IsValidQuery(queryString, this))
            {
                parameters = _parser.GetParameters(queryString, this);
            }

            return new QueryRunner().Execute(parameters, rows);
        }

        public bool HasColumn(string columnName)
        {
            return this.Columns.Any(c => c.Name == columnName);
        }


        public Column GetColumn(Guid? id)
        {
            return Columns.Where(c => c.Id == id).FirstOrDefault();
        }

        public Column GetColumn(string columnName)
        {
            return Columns.Where(c => c.Name == columnName).FirstOrDefault();
        }

        public void AddColumn(string columnName, Type type)
        {
            Column column;
            if (!HasColumn(columnName))
            {
                column = new Column(columnName, type);
                _columns.Add(column);

                _process.EventManager.TriggerEvent(EventName.Columm.Added,
                       CreateColumnAddedEventArgs(column));
            }
        }

        public void RemoveColumn(string columnName)
        {
            if (HasColumn(columnName))
            {
                var col = GetColumn(columnName);
                _columns.Remove(col);

                _process.EventManager.TriggerEvent(EventName.Columm.Deleted,
                       CreateColumnDeletedEventArgs(col));
            }
        }

        public void AddRow(RowForm form)
        {
            if (CheckInsertRules(form))
            {
                if (form.Participant.Location.IsLocal(_process) || form.Participant.IsDatabase(DatabaseId))
                {
                    AddRowLocally(form);
                }
                else
                {
                    form.IsRemoteInsert = true;
                    AddRowRemotely(form);
                }
            }

            _process.EventManager.TriggerEvent(EventName.Row.Added, CreateRowAddedEventArgs(form.Row));
        }

        public RowForm GetNewRow(Guid? participantId)
        {
            RowForm form = null;

            var db = _process.GetDatabase(DatabaseId);
            if (db.HasParticipant(participantId))
            {
                form = new RowForm(GetNewRow(), db.GetParticipant(participantId));
                form.DatabaseName = db.Name;
                form.TableName = this.Name;
            }

            return form;
        }

        public RowForm GetNewRowForLocal()
        {
            RowForm form = null;

            var db = _process.GetDatabase(DatabaseId);
            if (db.HasParticipant(db.Id))
            {
                form = new RowForm(GetNewRow(), db.GetParticipant(db.Id));
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
        private bool CheckInsertRules(RowForm form)
        {
            return true;

            // TO DO: FIX THIS
            bool insertAllowed = false;

            bool isLocalInsert = form.Participant.Location.IsLocal(_process);

            if (isLocalInsert)
            {
                if (RowMatchesTableColumns(form.Row))
                {
                    if (DatabaseId != null && _contractValidator is null)
                    {
                        if (_process.HasDatabase(form.DatabaseName))
                        {
                            _contractValidator = new ContractValidator(_process.GetDatabase(form.DatabaseName).Contract, DatabaseId);
                        }

                        if (_process.HasPartialDatabase(form.DatabaseName))
                        {
                            _contractValidator = new ContractValidator(_process.GetPartialDatabase(form.DatabaseName).Contract, DatabaseId);
                        }
                    }

                    // we make sure this action is okay with the defined contract
                    if (_contractValidator.ActionIsValidForParticipant(TableAction.AddRow, form.Participant))
                    {
                        // we make sure the participant accepts this action if they're remote
                        if (form.Participant.AcceptsAction(TableAction.AddRow))
                        {
                            insertAllowed = true;
                        }
                    }
                }
            }
            else
            {
                insertAllowed = true;
            }

            return insertAllowed;
        }

        private Row GetNewRow()
        {
            List<Guid?> ids = new List<Guid?>();
            this.Columns.ForEach(c => ids.Add(c.Id));

            return new Row(ids, this.Id);
        }

        private Row GetNewRowWithId(Guid? rowId)
        {
            List<Guid?> ids = new List<Guid?>();
            this.Columns.ForEach(c => ids.Add(c.Id));

            return new Row(ids, this.Id, rowId);
        }

        private bool RowMatchesTableColumns(Row row)
        {
            bool isMatch = true;

            row.ColumnIds.ForEach(c =>
            {
                var x = GetColumn(c);

                if (x != null)
                {
                    isMatch = this.Columns.Any(tc => tc.Name == x.Name && tc.DataType == x.DataType);
                }
            });

            if (!(row.ColumnIds.Count == this.Columns.Count))
            {
                isMatch = false;
            }

            return isMatch;
        }

        private void AddRowRemotely(RowForm form)
        {
            var info = JsonConvert.SerializeObject(form);

            var addNewRowMessage = new Message(form.Participant.Location, _process.GetLocation(), info, MessageDataAction.Row.Save_Row, MessageType.Data);
            _process.Network.SendMessage(addNewRowMessage);

            //Client.SaveRow(participant.Location, DatabaseId, row.TableId, row);
            _rows.Add(GetNewRowReference(form.Row, form.Participant.Location));

            _process.EventManager.TriggerEvent(EventName.Row.Added_Remotely,
                   CreateRowAddedEventArgs(form.Row));
        }

        private void AddRowLocally(RowForm form)
        {
            var row = form.Row;

            string debug = $"Adding row to process {_process.GetLocation().IpAddress} : {_process.GetLocation().PortNumber.ToString()}";

            Debug.WriteLine(debug);
            _process.Log.Debug(debug);

            _store.AddRow(row);
            _rows.Add(GetNewRowReference(row));

            _process.EventManager.TriggerEvent(EventName.Row.Added,
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
                _process.GetLocation()), DatabaseId, row.Id, _process);
        }

        private RowReference GetNewRowReference(Row row, Location location)
        {
            var colIds = new List<Guid?>();

            Columns.ForEach(c =>
            {
                colIds.Add(c.Id);
            });

            return new RowReference(colIds, this.Id, new Participant(location), DatabaseId, row.Id, _process);
        }

        private ColumnAddedEventArgs CreateColumnAddedEventArgs(Column column)
        {
            return new ColumnAddedEventArgs
            {
                DatabaseName = _process.GetDatabase(this.DatabaseId).Name,
                TableName = this.Name,
                ColumnName = column.Name,
                Type = column.DataType
            };
        }

        private ColumnDeletedEventArgs CreateColumnDeletedEventArgs(Column column)
        {
            return new ColumnDeletedEventArgs
            {
                DatabaseName = _process.GetDatabase(this.DatabaseId).Name,
                TableName = this.Name,
                ColumnName = column.Name
            };
        }

        #endregion
    }
}
