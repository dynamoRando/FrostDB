using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Represents the binary data for this database on disk.
    /// </summary>
    public class DbDataFile : IStorageFile
    {
        /*
         * version int
         * <binary page data>
         */

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
        /// <summary>
        /// Reads the Db Data File for the next available page that is not already in the specified list of addresses
        /// </summary>
        /// <param name="excludeAddresses">The list of page addreses to exclude</param>
        /// <returns>A page from disk</returns>
        public Page GetNextPage(List<PageAddress> excludeAddresses)
        {
            throw new NotImplementedException(); 
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates the data file for this database on disk.
        /// </summary>
        private void CreateFile()
        {
            using (var file = File.Create(FileName()))
            {

            }
        }
        /// <summary>
        /// Checks if the database file exists, otherwise false.
        /// </summary>
        /// <returns>True if file exists, otherwise false.</returns>
        private bool DoesFileExist()
        {
            return File.Exists(FileName());
        }

        /// <summary>
        /// Returns the data file name for this database.
        /// </summary>
        /// <returns>Returns the data file name for this database.</returns>
        private string FileName()
        {
            return Path.Combine(_dataFileFolder, _databaseName + "." + _dataFileExtension);
        }
        #endregion


    }
}
