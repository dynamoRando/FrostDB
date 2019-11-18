using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB
{
    [Serializable]
    public class Store : 
        IBaseStore, ISerializable, IDBObject
    {
        #region Private Fields
        private List<Row> _rows;
        private Guid? _tableId;
        #endregion

        #region Public Properties
        public Guid? TableId => _tableId;
        public List<Row> Rows => _rows;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Store() 
        {
            _rows = new List<Row>();
            _tableId = Guid.NewGuid();
        }

        protected Store(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _tableId = (Guid?)serializationInfo.GetValue
              ("TableId", typeof(Guid?));
            _rows = (List<Row>)serializationInfo.GetValue
                ("TableRows", typeof(List<Row>));
        }

        #endregion

        #region Public Methods
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TableId", _tableId, typeof(Guid?));
            info.AddValue("TableRows", _rows, typeof(List<Row>));
        }

        public void AddRow(Row row)
        {
            _rows.Add(row);
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
