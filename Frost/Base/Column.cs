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
            throw new NotImplementedException();
        }

        protected Column(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion


    }
}
