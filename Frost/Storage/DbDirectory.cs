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
        public List<string> OnlineDatabases => Databases.Where(d => d.IsOnline).Select(k => k.DatabaseName).ToList();
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
