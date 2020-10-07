using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FrostDB.Interface;

namespace FrostDB
{
    /// <summary>
    /// Contains the indexes that this database has
    /// </summary>
    class DbDataIndexFile : IStorageFile
    {
        #region Private Fields
        private string _indexFileExtension;
        private string _indexFileFolder;
        private string _databaseName;
        #endregion

        #region Public Properties
        public int VersionNumber { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a file that contains the indexes that a database may have
        /// </summary>
        /// <param name="extension">The file extension</param>
        /// <param name="folder">The folder in which the index file is held</param>
        /// <param name="databaseName">The name of the db</param>
        public DbDataIndexFile(string extension, string folder, string databaseName)
        {
            _indexFileExtension = extension;
            _indexFileFolder = folder;
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
        #endregion

        #region Private Methods
        /// <summary>
        /// Checks the version number of this file. If it is not set, it will default to Version 1
        /// </summary>
        private void SetVersionNumberIfBlank()
        {
            if (VersionNumber == 0)
            {
                VersionNumber = StorageFileVersions.DATA_INDEX_FILE_VERSION_1;
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
            return Path.Combine(_indexFileFolder, _databaseName + _indexFileExtension);
        }
        #endregion

    }
}
