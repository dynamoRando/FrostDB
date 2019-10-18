using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Base
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
        public Column(string name, Type type)
        {
            _id = Guid.NewGuid();
            _name = name;
            _type = type;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id.Value, typeof(Guid));
            info.AddValue("Name", Name, typeof(string));
            info.AddValue("DataType", DataType, typeof(Type));
        }

        protected Column(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _id = (Guid)serializationInfo.GetValue("Id", typeof(Guid));
            _name = (string)serializationInfo.GetValue("Name", typeof(string));
            _type = (Type)serializationInfo.GetValue
                ("DataType", typeof(Type));
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion


    }
}
