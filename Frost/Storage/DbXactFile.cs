using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// The database transaction file
    /// </summary>
    class DbXactFile : IStorageFile
    {
        #region Private Fields
        private string _xactFileExtension;
        private string _xactFileFileFolder;
        private string _databaseName;
        #endregion

        #region Public Properties
        public int VersionNumber { get; set; }
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        /// <summary>
        /// This method is a stub.
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public bool WriteTransactionForUpdate(List<RowUpdate> rows)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is a stub
        /// </summary>
        /// <returns></returns>
        public bool WriteTransactionForInsert()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is a stub
        /// </summary>
        /// <returns></returns>
        public bool WriteTransactionForDelete()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion
       
    }
}
