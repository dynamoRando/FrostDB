using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace FrostDB
{
    public class StorageManager : IStorageManager
    {
        #region Private Fields
        private Process _process;
        private string _databaseFolder;
        private DbDirectory _directory;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public StorageManager() { }
        public StorageManager(Process process)
        {
            if (process != null)
            {
                _process = process;
                _databaseFolder = _process.Configuration.DatabaseFolder;
            }
            else
            {
                throw new ArgumentNullException(nameof(process));
            }
        }
        #endregion

        #region Public Methods
        public List<Database> GetDatabases()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private List<string> GetListOfDatabases()
        {
            var result = new List<string>();
            var file = Path.Combine(_databaseFolder, _process.Configuration.DatabaseDirectoryFileName);
            if (File.Exists(file))
            {
                var items = File.ReadAllLines(file).ToList();
                _directory = new DbDirectory(items);
            }

            result.AddRange(_directory.GetOnlineDatabases);

            return result;
        }
        #endregion
    }
}
