using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Stores the users and their permissions for the database on disk
    /// </summary>
    public class DbSecurityFile : IStorageFile
    {
        #region Private Fields
        private string _securityFileExtension;
        private string _securityFileFolder;
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
        public DbSecurityFile(string extension, string folder, string databaseName)
        {
            _securityFileExtension = extension;
            _securityFileFolder = folder;
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
