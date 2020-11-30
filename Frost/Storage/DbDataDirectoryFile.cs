using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using MoreLinq;

namespace FrostDB.Storage
{
    public class DbDataDirectoryFileItem
    {
        public int PageNumber { get; set; }
        public int LineNumber { get; set; }

        /// <summary>
        /// Returns PageNumber LineNumber 
        /// </summary>
        /// <returns>The Directory File Item in string format</returns>
        public override string ToString()
        {
            return $"{PageNumber.ToString()} {LineNumber.ToString()}";
        }
    }

    /// <summary>
    /// Contains page id and line number for the data file
    /// </summary>
    public class DbDataDirectoryFile : IStorageFile
    {

        // page number, line number
        #region Private Fields
        private DbDataFile _dataFile;
        private string _extension;
        private string _databaseName;
        private string _folder;
        private readonly object _fileLock = new object();

        #endregion

        #region Public Properties
        public ConcurrentBag<DbDataDirectoryFileItem> Lines { get; set; }
        public int TotalPages => Lines.Count;
        public int VersionNumber { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a file holding the page id and line number in the binary data file.
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

            Lines = new ConcurrentBag<DbDataDirectoryFileItem>();

            // need to check if file is on disk and it not, create it
            if (!DoesFileExist())
            {
                CreateFile();
            }
            else
            {
                LoadFile();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the line number from the data directory for the specified pageId
        /// </summary>
        /// <param name="pageId">The pageId you wish to lookup the line number for</param>
        /// <returns>The line number of the specified pageId.</returns>
        public int GetLineNumberForPage(int pageId)
        {
            return Lines.Where(line => line.PageNumber == pageId).FirstOrDefault().LineNumber;
        }

        public bool WritePages(Page[] pages)
        {
            // start at second line because the first line is the version number
            int lineNumber = 2;
            Lines.Clear();

            foreach (var page in pages)
            {
                Lines.Add(new DbDataDirectoryFileItem { PageNumber = page.Id, LineNumber = lineNumber });
                lineNumber++;
            }

            SaveLines();

            return true;
        }

        /// <summary>
        /// Adds the specified page id and line number to the directory
        /// </summary>
        /// <param name="pageId">The page id being added</param>
        /// <param name="lineNumber">The line number of the page in the data file</param>
        /// <returns>True if successful, otherwise false</returns>
        public bool AddPage(int pageId, int lineNumber)
        {
            Lines.Add(new DbDataDirectoryFileItem { LineNumber = lineNumber, PageNumber = pageId });

            lock (_fileLock)
            {
                // page number line number
                using (StreamWriter sw = File.AppendText(FileName()))
                {
                    sw.WriteLine($"{pageId.ToString()} {lineNumber.ToString()}");
                }
            }

            return true;
        }
        /// <summary>
        /// Returns the max page id stored in the data directory file
        /// </summary>
        /// <returns>The max page id in the file</returns>
        public int GetMaxPageIdInFile()
        {
            return Lines.Max(line => line.PageNumber);
        }

        /// <summary>
        /// Returns the next line number in the file (use when adding a new page)
        /// </summary>
        /// <returns>The next available line number</returns>
        public int GetNextLineNumber()
        {
            return Lines.Max(item => item.PageNumber) + 1;
        }

        /// <summary>
        /// Returns the line number in the data file for the page id specified. If the line number is not found,
        /// it will return 0;
        /// </summary>
        /// <param name="pageId">The page id being requested</param>
        /// <returns>The line number of the page in the data file</returns>
        public int GetLineNumberForPageId(int pageId)
        {
            DbDataDirectoryFileItem item = Lines.Where(line => line.PageNumber == pageId).FirstOrDefault();

            if (item == null)
            {
                return 0;
            }
            else
            {
                return item.LineNumber;
            }
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

        private void LoadFile()
        {
            lock (_fileLock)
            {
                if (DoesFileExist())
                {
                    var lines = File.ReadAllLines(FileName());
                    ParseLines(lines);
                }
            }
        }

        private void SaveLines()
        {
            lock (_fileLock)
            {
                using (var file = new StreamWriter(FileName()))
                {
                    file.WriteLine("version " + VersionNumber.ToString());
                }

                List<string> lines = new List<string>(Lines.Count);

                Lines.ForEach(line => lines.Add(line.ToString())); ;

                foreach (var line in Lines)
                {
                    File.AppendAllLines(FileName(), lines);
                }
            }
        }

        private void ParseLines(string[] lines)
        {
            // page number line number
            foreach (var line in lines)
            {
                if (line.StartsWith("version"))
                {
                    var items = line.Split(" ");
                    VersionNumber = Convert.ToInt32(items[1]);
                }
                else if (line.Equals(string.Empty))
                {
                    continue;
                }
                else 
                {
                    var items = line.Split(" ");
                    Lines.Add(new DbDataDirectoryFileItem
                    {
                        PageNumber = Convert.ToInt32(items[0]),
                        LineNumber = Convert.ToInt32(items[1])
                    });
                }
            }
        }
        #endregion
    }
}
