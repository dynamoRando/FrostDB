﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Stores the contract information for this database on disk. Supersedes the old contract format style. (seralized C# object)
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
        /// <summary>
        /// Creates a contract file for the specified database that holds the data agreement between a db host and its participants
        /// </summary>
        /// <param name="extension">The file extension</param>
        /// <param name="folder">The folder in which the file is held</param>
        /// <param name="databaseName">The name of the database</param>
        public DbContractFile(string extension, string folder, string databaseName)
        {
            _contractFileExtension = extension;
            _contractFileFolder = folder;
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
                VersionNumber = StorageFileVersions.DATA_CONTRACT_FILE_VERSION_1;
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
            return Path.Combine(_contractFileFolder, _databaseName + _contractFileExtension);
        }
        #endregion


    }
}
