using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace FrostDB
{
    // shows if a database is online or offline
    public class DbDirectoryItem
    {
        public string DatabaseName { get; set; }
        public bool IsOnline { get; set; }
    }

    /// <summary>
    /// Maintains a list of databases that are online or offline
    /// </summary>
    public class DbDirectory
    {
        #region Private Fields
        private DbDirectoryItem[] _databases;
        private string _fileName;
        private readonly object _fileLock = new object();
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DbDirectory(string[] lines, string fileName)
        {
            _databases = new DbDirectoryItem[lines.Length];
            _fileName = fileName;
            ParseLines(lines);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds the database to the Frost system Db directory file
        /// </summary>
        /// <param name="database">The database object</param>
        /// <param name="isOnline">True if the database is online, otherwise false</param>
        public void AddDatabaseToDirectory(Database2 database, bool isOnline)
        {
            // databaseName <bool> isOnline
            lock (_fileLock)
            {
                using (var sw = File.AppendText(_fileName))
                {
                    sw.WriteLine($"{database.Name},{isOnline.ToString()}");
                }
            }
        }

        public string[] OnlineDatabases => GetOnlineDatabases();

        /// <summary>
        /// Attempts to take a database offline and updates the Frost system Db directory file
        /// </summary>
        /// <param name="databaseName">The name of the database to take offline</param>
        /// <returns>True if successful, otherwise false</returns>
        public bool TakeDatabaseOffline(string databaseName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Attempts to bring a database online and updates the Frost system Db directory file
        /// </summary>
        /// <param name="databaseName">The name of the database to bring online</param>
        /// <returns>True if successful, otherwise false</returns>
        public bool BringDatabaseOnline(string databaseName)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private void ParseLines(string[] lines)
        {
            _databases = new DbDirectoryItem[lines.Length];
            int i = 0;

            // dbname, true/false (online, offline)
            foreach (var item in lines)
            {
                var x = item.Split(",");
                var databaseName = x[0].Trim();
                var isOnline = Convert.ToBoolean(x[1].Trim());
                var d = new DbDirectoryItem();
                d.DatabaseName = databaseName;
                d.IsOnline = isOnline;
                _databases[i] = d;
                i++;
            }
        }

        private string[] GetOnlineDatabases()
        {
            return _databases.Where(d => d.IsOnline).Select(k => k.DatabaseName).ToArray();
        }
        #endregion

    }
}
