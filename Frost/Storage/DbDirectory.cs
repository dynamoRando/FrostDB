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
        #endregion

        #region Public Properties
        public List<DbDirectoryItem> Databases { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DbDirectory(List<string> lines)
        {
            Databases = new List<DbDirectoryItem>();
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
            // need to write to disk the new database and it's online status

            throw new NotImplementedException();
        }

        public List<string> OnlineDatabases => Databases.Where(d => d.IsOnline).Select(k => k.DatabaseName).ToList();

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
        private void ParseLines(List<string> lines)
        {
            // dbname, true/false (online, offline)
            foreach (var item in lines)
            {
                var x = item.Split(",");
                var databaseName = x[0].Trim();
                var isOnline = Convert.ToBoolean(x[1].Trim());
                var d = new DbDirectoryItem();
                d.DatabaseName = databaseName;
                d.IsOnline = isOnline;
                Databases.Add(d);
            }
        }
        #endregion

    }
}
