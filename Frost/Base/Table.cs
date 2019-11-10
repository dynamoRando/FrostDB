using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using FrostDB.EventArgs;

namespace FrostDB.Base
{
    /*
    [Serializable]
    public class Table : ITable<Column, Row>, ISerializable
    {
        #region Private Fields
        private List<Column> _columns;
        private List<Row> _rows;
        private Guid? _id;
        private string _name;
        #endregion

        #region Public Properties
        public List<Column> Columns => _columns;
        public Guid? Id => _id;
        public string Name => _name;
        public List<Row> Rows => _rows;
        public Guid? DatabaseId { get; set ; }

        #endregion

        #region Events
        #endregion

        #region Constructors
        public Table()
        {
            _id = Guid.NewGuid();
            _rows = new List<Row>();
        }
        protected Table(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _rows = (List<Row>)serializationInfo.GetValue
               ("TableRows", typeof(List<Row>));
            DatabaseId = (Guid?)serializationInfo.GetValue
                ("TableDatabaseId", typeof(Guid?));
            _id = (Guid)serializationInfo.GetValue("TableId", typeof(Guid));
            _name = (string)serializationInfo.GetValue("TableName", typeof(string));
            _columns = (List<Column>)serializationInfo.GetValue
                ("TableColumns", typeof(List<Column>));
        }

        public Table(string name, List<Column> columns, Guid? database) : this()
        {
            _name = name;
            _columns = columns;
            DatabaseId = database;
        }
        #endregion

        #region Public Methods
        public void AddRow(Row row)
        {
            if (RowMatchesTableColumns(row))
            {
                this._rows.Add(row);
                EventManager.TriggerEvent(EventName.Row.Added,
                    CreateRowAddedEventArgs(row));
            }
        }

        public void DeleteRow(Row row)
        {
            if (HasRow(row))
            {
                this._rows.Remove(row);
                EventManager.TriggerEvent(EventName.Row.Deleted,
                    CreateRowDeletedsEventArgs(row));
            }
        }

        public Row GetNewRow()
        {
            List<Guid?> ids = new List<Guid?>();
            this.Columns.ForEach(c => ids.Add(c.Id));

            return new Row(ids, this.Id);
        }

        public bool HasRow(Row row)
        {
            bool hasRow = false;

            Parallel.ForEach(_rows, (r, rowState) =>
            {
                Parallel.ForEach(r.Values, (c, valueState) =>
                {
                    hasRow = row.Values.All(p =>
                     (p.ColumnName == c.ColumnName &&
                     p.Value == c.Value));

                    if (hasRow)
                    {
                        valueState.Stop();
                        rowState.Stop();
                    }
                });
            });

            return hasRow;
        }

        public bool HasRow(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void UpdateRow(Row oldRow, Row newRow)
        {
            if (HasRow(oldRow) && RowMatchesTableColumns(newRow))
            {
                var refRow = GetRow(oldRow);
                _rows.Remove(refRow);
                _rows.Add(newRow);
            }
        }

        public List<Row> GetRows(string queryString)
        {
            var parameters = new List<RowValueQueryParam>();

            if (QueryParser.IsValidQuery(queryString, this))
            {
                parameters = QueryParser.GetParameters(queryString, this);
            }

            var query = new QueryRunner();
            var results = query.Execute(parameters, _rows);

            return results;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TableId", Id.Value, typeof(Guid));
            info.AddValue("TableColumns", Columns, typeof(List<Column>));
            info.AddValue("TableName", Name, typeof(string));
            info.AddValue("TableRows", _rows, typeof(List<Row>));
            info.AddValue("TableDatabaseId", DatabaseId,
                typeof(Guid?));
        }

        public Column GetColumn(Guid? id)
        {
            return Columns.Where(c => c.Id == id).FirstOrDefault();
        }
        #endregion

        #region Private Methods
        private RowDeletedEventArgs CreateRowDeletedsEventArgs(Row row)
        {
            return new RowDeletedEventArgs
            {
                DatabaseId = this.DatabaseId,
                Table = this,
                Row = row
            };
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

        private Row GetRow(Row row)
        {
            Row returnRow = null;

            Parallel.ForEach(_rows, (r) =>
            {
                Parallel.ForEach(r.Values, (c) =>
                {
                    if (row.Values.All(p =>
                    (p.ColumnName == c.ColumnName &&
                    p.Value == c.Value)))
                    {
                        returnRow = r;
                    }
                });
            });

            return returnRow;
        }

        private Row GetRow(Guid id)
        {
            return _rows.Where(r => r.Id == id).FirstOrDefault();
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
        #endregion
    }
    */
}
