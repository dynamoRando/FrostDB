using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
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
