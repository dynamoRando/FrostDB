using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FrostDB.Storage
{

    class DbDataDirectoryFileItem
    {
        public int PageNumber { get; set; }
        public int LineNumber { get; set; }
    }

    /// <summary>
    /// Contains page id and line number for the data file
    /// </summary>
    class DbDataDirectoryFile : IStorageFile
    {

        // page number, line number
        #region Private Fields
        private DbDataFile _dataFile;
        private string _extension;
        private string _databaseName;
        private string _folder;
        #endregion

        #region Public Properties
        public List<DbDataDirectoryFileItem> Lines { get; set; }
        public int TotalPages => Lines.Count;
        public int VersionNumber { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a file holding the page id and line number in the binrary data file.
        /// </summary>
        /// <param name="dataFile">The actual binary data file of the db</param>
        /// <param name="folder">The folder in which dbs are held</param>
        /// <param name="databaseName">The name of the database</param>
        /// <param name="extension">The file extension for data directory files</param>
        public DbDataDirectoryFile(DbDataFile dataFile, string folder, string databaseName, string extension)
        {
            _dataFile = dataFile;
            _folder = folder;
            _databaseName = databaseName;
            _extension = extension;

            Lines = new List<DbDataDirectoryFileItem>();

            // need to check if file is on disk and it not, create it
            if (!DoesFileExist())
            {
                CreateFile();
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Returns the max page id stored in the data directory file
        /// </summary>
        /// <returns>The max page id in the file</returns>
        public int GetMaxPageIdInFile()
        {
            return Lines.Max(line => line.PageNumber);
        }

        /// <summary>
        /// Returns the line number in the data file for the page id specified
        /// </summary>
        /// <param name="pageId">The page id being requested</param>
        /// <returns>The line number of the page in the data file</returns>
        public int GetLineNumberForPageId(int pageId)
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private void CreateFile()
        {
            SetVersionNumberIfBlank();

            using (var file = new StreamWriter(FileName()))
            {
                file.WriteLine("version " + VersionNumber.ToString());
            }
        }

        private bool DoesFileExist()
        {
            return File.Exists(FileName());
        }

        private string FileName()
        {
            return Path.Combine(_folder, _databaseName + _extension);
        }

        private void SetVersionNumberIfBlank()
        {
            if (VersionNumber == 0)
            {
                VersionNumber = StorageFileVersions.DATA_DIRECTORY_FILE_VERSION_1;
            }
        }
        #endregion
    }
}
