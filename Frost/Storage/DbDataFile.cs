using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Represents the binary data for this database on disk.
    /// </summary>
    public class DbDataFile : IStorageFile
    {
        #region Private Fields
        private string _dataFileExtension;
        private string _dataFileFolder;
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
        public DbDataFile(string extension, string folder, string databaseName)
        {
            _dataFileExtension = extension;
            _dataFileFolder = folder;
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
