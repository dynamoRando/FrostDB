using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Stores the contract information for this database on disk
    /// </summary>
    public class DbContractFile : IStorageFile
    {
        #region Private Fields
        private string _contractFileExtension;
        private string _contractFileFolder;
        private string _databaseName;
        #endregion

        #region Public Properties
        public int VersionNumber { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DbContractFile(string extension, string folder, string databaseName)
        {
            _contractFileExtension = extension;
            _contractFileFolder = folder;
            _databaseName = databaseName;
        }
        #endregion

        #region Public Methods
        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public void Load()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
