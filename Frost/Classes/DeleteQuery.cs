using FrostCommon.Net;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class DeleteQuery : IQuery
    {
        #region Private Fields
        private Process _process;
        #endregion

        #region Public Properties
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public Process Process { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DeleteQuery(Process process)
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public FrostPromptResponse Execute()
        {
            throw new NotImplementedException();
        }

        public bool IsValid(string statement)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
