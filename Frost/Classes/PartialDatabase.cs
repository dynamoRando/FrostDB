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
        Process _process;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public PartialDatabase(Process process) : base(process)
        {
            _process = process;
        }
        public PartialDatabase(string name, Process process) : base(name, process)
        {
        }

        public PartialDatabase(string name, Guid id,
           List<Table> tables, Process process) : base(name, id, tables, process)
        {
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        protected PartialDatabase(SerializationInfo serializationInfo, StreamingContext streamingContext, Process process) : base(process)
        {
            throw new NotImplementedException();
        }
    }
}
