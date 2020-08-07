using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace FrostDB
{
    public class DbDirectoryItem
    {
        public string DatabaseName { get; set; }
        public bool IsOnline { get; set; }
    }
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
        public List<string> GetOnlineDatabases => Databases.Where(d => d.IsOnline).Select(k => k.DatabaseName).ToList();
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
