using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// <summary>
        /// Creates a file that holds the users, roles, permissions for a database
        /// </summary>
        /// <param name="extension">The file extension for the security file</param>
        /// <param name="folder">The folder in which the file is held</param>
        /// <param name="databaseName">The name of the database for this file</param>
        public DbSecurityFile(string extension, string folder, string databaseName)
        {
            _securityFileExtension = extension;
            _securityFileFolder = folder;
            _databaseName = databaseName;

            if (!DoesFileExist())
            {
                CreateFile();
            }
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
        /// <summary>
        /// Checks the version number of this file. If it is not set, it will default to Version 1
        /// </summary>
        private void SetVersionNumberIfBlank()
        {
            if (VersionNumber == 0)
            {
                VersionNumber = StorageFileVersions.DATA_SECURITY_FILE_VERSION_1;
            }
        }

        /// <summary>
        /// Creates a new Schema file for this database
        /// </summary>
        private void CreateFile()
        {
            SetVersionNumberIfBlank();

            using (var file = new StreamWriter(FileName()))
            {
                file.WriteLine("version " + VersionNumber.ToString());
            }
        }

        /// <summary>
        /// Checks to see if the Schema file exists for this database.
        /// </summary>
        /// <returns>True if the schema file exists, otherwise false.</returns>
        private bool DoesFileExist()
        {
            return File.Exists(FileName());
        }

        /// <summary>
        /// Returns the schema filename for this database.
        /// </summary>
        /// <returns>Returns the schema filename for this database.</returns>
        private string FileName()
        {
            return Path.Combine(_securityFileFolder, _databaseName + _securityFileExtension);
        }
        #endregion


    }
}
