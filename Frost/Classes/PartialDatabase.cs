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
        private List<Table> _tables;
        private string _name;
        private Guid? _id;
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
            _id = Guid.NewGuid();
            _tables = new List<Table>();
        }
        public PartialDatabase(string name) : this()
        {
            _name = name;
        }

        public PartialDatabase(string name, Guid id,
           List<Table> tables) : this(name)
        {
            _id = id;
            _tables = tables;
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
