using FrostDB.Interface;
using FrostDB.Storage;
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
        private DbDataDirectoryFile _dataDirectory;
        private string _dataDirectoryExtension;
        private readonly object _fileLock = new object();
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
        /// Creates a data file holding the actual binary data on disk for the db
        /// </summary>
        /// <param name="extension">The file extension for the binary file</param>
        /// <param name="folder">The folder in which the dbs are held</param>
        /// <param name="databaseName">The name of the database</param>
        /// <param name="dataDirectoryExtension">The name of the db directory file extension</param>
        public DbDataFile(string extension, string folder, string databaseName, string dataDirectoryExtension)
        {
            _dataFileExtension = extension;
            _dataFileFolder = folder;
            _databaseName = databaseName;
            _dataDirectoryExtension = dataDirectoryExtension;

            _dataDirectory = new DbDataDirectoryFile(this, folder, databaseName, _dataDirectoryExtension);

            if (!DoesFileExist())
            {
                CreateFile();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets a page with the specified id from the data file on disk
        /// </summary>
        /// <param name="id">The page to get from disk</param>
        /// <returns>A page object</returns>
        public Page GetPage(int id, BTreeAddress address)
        {
            Page page = null;
            int lineNumber = _dataDirectory.GetLineNumberForPageId(id);

            if (lineNumber != 0)
            {
                byte[] data = GetBinaryPageDataFromDisk(lineNumber);
                page = new Page(data, address);
            }
           
            return page;
        }

        /// <summary>
        /// Attempts to add the page to the binary data file and updates the data directory file.
        /// </summary>
        /// <param name="page">The page to add</param>
        /// <returns>True if successful, otherwise false</returns>
        public bool AddPage(Page page)
        {
            int lineNumber = _dataDirectory.GetNextLineNumber();
            _dataDirectory.AddPage(page.Id, lineNumber);
            AddPage(page.ToBinary(), lineNumber);
            return true;
        }

        /// <summary>
        /// Attempts to update the specified page in the binary data file
        /// </summary>
        /// <param name="page">The page to update</param>
        /// <returns>True if successful, otherwise false</returns>
        public bool UpdatePage(Page page)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the Db Data File for the next available page that is not already in the specified list of addresses
        /// </summary>
        /// <param name="excludeAddresses">The list of page addreses to exclude</param>
        /// <returns>A page from disk</returns>
        public Page GetNextPage(List<PageAddress> excludeAddresses)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes the specified pages to disk
        /// </summary>
        /// <param name="pages">The pages to write</param>
        /// <returns>True if successful, otherwise false</returns>
        public bool WritePages(Page[] pages)
        {
            lock (_fileLock)
            {
                int i = 0;
                int currentOffset = 0;
                for (i = 0; i < pages.Length; i++)
                {
                    using (Stream stream = File.Open(FileName(), FileMode.Open))
                    {
                        byte[] data = pages[i].ToBinary();
                        stream.Seek(currentOffset, SeekOrigin.Begin);
                        stream.Write(data, 0, data.Length);
                        currentOffset += DatabaseConstants.PAGE_SIZE;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Returns the first page from the data file from disk
        /// </summary>
        /// <returns>The first page from file</returns>
        public Page GetAPage()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is a stub
        /// </summary>
        /// <returns>All the pages from the data file</returns>
        public Page[] GetAllPages()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Returns the byte array in the data file at the specified linenumber
        /// </summary>
        /// <param name="lineNumber">The line number of the data file</param>
        /// <returns>The binary data at the specified line number</returns>
        private byte[] GetBinaryPageDataFromDisk(int lineNumber)
        {
            string line = string.Empty;
            lock (_fileLock)
            {
                using (Stream stream = File.Open(FileName(), FileMode.Open))
                {
                    stream.Seek(DatabaseConstants.PAGE_SIZE * (lineNumber - 1), SeekOrigin.Begin);
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        line = reader.ReadLine();
                    }
                }
            }
            return DatabaseBinaryConverter.StringToBinary(line);
        }

        /// <summary>
        /// Adds the page binary data to the data file
        /// </summary>
        /// <param name="page">The binary data to save</param>
        /// <param name="lineNumber">The line at which data should be added</param>
        private void AddPage(byte[] page, int lineNumber)
        {
            using (FileStream stream = File.OpenWrite(FileName()))
            {
                stream.Seek(DatabaseConstants.PAGE_SIZE * (lineNumber - 1), SeekOrigin.Begin);
                stream.Write(page, 0, page.Length);
            }
        }

        /// <summary>
        /// Creates the data file for this database on disk.
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
            return Path.Combine(_dataFileFolder, _databaseName + _dataFileExtension);
        }

        private void SetVersionNumberIfBlank()
        {
            if (VersionNumber == 0)
            {
                VersionNumber = StorageFileVersions.DATA_FILE_VERSION_1;
            }
        }
        #endregion

    }
}
