using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Base
{
    public class RowReference : ISerializable
    {
        #region Private Fields
        private Guid? _id;
        private Location _location;
        #endregion

        #region Public Properties
        public Guid? Id => _id;
        public Location Location => _location;
        #endregion

        #region Constructors
        public RowReference() { }
        protected RowReference(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _id = (Guid)serializationInfo.GetValue("Id", typeof(Guid));
            _location = (Location)serializationInfo.
                GetValue("Values", typeof(Location));
        }
        #endregion

        #region Private Fields
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id.Value, typeof(Guid?));
            info.AddValue("Location", Location, typeof(Location));
        }
        #endregion
    }
}
