using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using FrostDB.Interface;

namespace FrostDB
{
    [Serializable]
    public class PartialDatabase : Database
    {
        #region Private Fields
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public PartialDatabase()
        {
        }
        public PartialDatabase(string name) : base(name)
        {
        }

        public PartialDatabase(string name, Guid id,
           List<Table> tables) : base(name, id, tables)
        {
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        protected PartialDatabase(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
