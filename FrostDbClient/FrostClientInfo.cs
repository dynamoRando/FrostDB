using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDbClient
{
    public class FrostClientInfo
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public Guid? ProcessId { get; set; }
        public List<string> DatabaseNames { get; set; }
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
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion


    }
}
