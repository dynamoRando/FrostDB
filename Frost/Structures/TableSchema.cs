using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB
{
    [Serializable]
    public class TableSchema : ITableSchema, ISerializable
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public string TableName { get; set; }
        public Guid? TableId { get; set; }
        public List<Column> Columns { get; set; }
        public bool IsCooperative { get; set; }
        public Guid? Version { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        protected TableSchema(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            TableName = (string)serializationInfo.GetValue
            ("SchemaTableName", typeof(string));
            TableId = (Guid?)serializationInfo.GetValue
                ("SchemaTableId", typeof(Guid?));
            Version = (Guid?)serializationInfo.GetValue
                ("SchemaTableVersion", typeof(Guid?));
            Columns = (List<Column>)serializationInfo.GetValue
                ("SchemaTableColumns", typeof(List<Column>));
            IsCooperative = (bool)serializationInfo.GetValue
                ("SchemaTableIsCooperative", typeof(bool));
        }

        public TableSchema()
        {
            Columns = new List<Column>();
            Version = Guid.NewGuid();
        }

        public TableSchema(Table table) : this()
        {
            Map(table);
        }
        #endregion

        #region Public Methods
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SchemaTableName", TableName, typeof(string));
            info.AddValue("SchemaTableId", TableId, typeof(Guid?));
            info.AddValue("SchemaTableVersion", Version, typeof(Guid?));
            info.AddValue("SchemaTableColumns", Columns, typeof(List<Column>));
            info.AddValue("SchemaTableIsCooperative", IsCooperative, typeof(bool));
        }
        #endregion

        #region Private Methods
        private void Map(Table table)
        {
            TableName = table.Name;
            TableId = table.Id;
            Columns = table.Columns;
            IsCooperative = table.HasCooperativeData();
        }
        #endregion
    }
}
