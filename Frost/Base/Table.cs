using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Base
{
    [Serializable]
    public class Table : ITable<Column, Row>, ISerializable
    {
        #region Private Fields
        private List<Column> _columns;
        private List<Row> _rows;
        private Guid? _id;
        private string _name;
        private IDatabase _database;
        #endregion

        #region Public Properties
        public List<Column> Columns => _columns;
        public Guid? Id => _id;
        public string Name => _name;

        #endregion

        #region Events
        #endregion

        #region Constructors
        public Table() { }
        protected Table(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _id = (Guid)serializationInfo.GetValue("Id", typeof(Guid));
            _name = (string)serializationInfo.GetValue("Name", typeof(string));
            _columns = (List<Column>)serializationInfo.GetValue
                ("Columns", typeof(List<Column>));
        }

        public Table(string name, List<Column> columns, Database database)
        {
            _name = name;
            _id = Guid.NewGuid();
            _rows = new List<Row>();
            _columns = columns;
            _database = database;
        }
        public Table(string name, List<Column> columns, PartialDatabase database)
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

        public List<Row> GetRows(string condition)
        {
            throw new NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id, typeof(Guid));
            info.AddValue("Columns", Columns, typeof(List<Column>));
            info.AddValue("Name", Name, typeof(string));
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
