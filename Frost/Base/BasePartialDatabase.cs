using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace FrostDB.Base
{
    [Serializable]
    public class BasePartialDatabase : BaseDatabase
    {
        #region Private Fields
        private List<BaseTable> _tables;
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
        public BasePartialDatabase()
        {
            _id = Guid.NewGuid();
            _tables = new List<BaseTable>();
        }
        public BasePartialDatabase(string name) : this()
        {
            _name = name;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        protected BasePartialDatabase(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
