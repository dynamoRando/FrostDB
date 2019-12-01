using System;
using System.Collections.Generic;
using FrostCommon.ConsoleMessages;

namespace FrostDbClient
{
    public class FrostClientInfo
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public Guid? ProcessId { get; set; }
        public List<string> DatabaseNames { get; set; }
        public List<DatabaseInfo> DatabaseInfos { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public FrostClientInfo()
        {
            ProcessId = Guid.NewGuid();
            DatabaseNames = new List<string>();
            DatabaseInfos = new List<DatabaseInfo>();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion


    }
}
