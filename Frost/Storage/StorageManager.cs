using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using FrostDB.Storage;

namespace FrostDB
{
    public class StorageManager : IStorageManager
    {
        #region Private Fields
        private Process _process;
        private string _databaseFolder;
        private DbDirectory _directory;
        private Pager _pager;
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
                _pager = new Pager(_process, _databaseFolder);
            }
            else
            {
                throw new ArgumentNullException(nameof(process));
            }
        }
        #endregion

        #region Public Methods
        public Page GetPage(string databaseName, string tableName, int pageId)
        {
            return _pager.GetPage(databaseName, tableName, pageId);
        }

        public Page GetPage(int databaseId, int tableId, int pageId)
        {
            return _pager.GetPage(databaseId, tableId, pageId);
        }

        public List<Database> GetDatabases()
        {
            var result = new List<Database>();
            var databases = GetListOfDatabases();
            foreach(var db in databases)
            {
                var storage = new DbStorage(_process);
                var dbItem = storage.GetDatabase(db);
                result.Add(dbItem);
            }

            return result;
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
            else
            {
                File.Create(file);
            }

            result.AddRange(_directory.OnlineDatabases);

            return result;
        }

        private SchemaFile GetSchemaFile(string databaseName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
