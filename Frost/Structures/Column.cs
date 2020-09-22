using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB
{
    [Serializable]
    public class Column : IColumn, ISerializable
    {
        #region Private Fields
        private Guid? _id;
        private string _name;
        private Type _type;
        #endregion

        #region Public Properties
        public Guid? Id => _id;
        public string Name => _name;
        public Type DataType => _type;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Column() { }
        public Column(string name, Type type)
        {
            _id = Guid.NewGuid();
            _name = name;
            _type = type;
        }

        public Column(string name, Type type, Guid? id)
        {
            _id = id;
            _type = type;
            _name = name;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ColumnId", Id.Value, typeof(Guid));
            info.AddValue("ColumnName", Name, typeof(string));
            info.AddValue("ColumnDataType", DataType, typeof(Type));
        }

        protected Column(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _id = (Guid)serializationInfo.GetValue("ColumnId", typeof(Guid));
            _name = (string)serializationInfo.GetValue("ColumnName", typeof(string));
            _type = (Type)serializationInfo.GetValue
                ("ColumnDataType", typeof(Type));
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion


    }
}
