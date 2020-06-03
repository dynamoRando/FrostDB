using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using FrostCommon.ConsoleMessages;
using FrostCommon;

namespace FrostDB
{
    public class PartialDatabaseManager 
    {
        #region Private Fields
        private List<PartialDatabase> _databases;
        private string _databaseFolder;
        private string _databaseExtension;
        private IDataFileManager<DataFile> _dataFileManager;
        private IDatabaseFileMapper<PartialDatabase, DataFile> _databaseFileMapper;
        private IDataManagerEventManager _dataEventManager;
        private Process _process;
        #endregion

        #region Public Properties
        public List<PartialDatabase> Databases => _databases;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public PartialDatabaseManager(string databaseFolder,
            string databaseExtension,
            IDatabaseFileMapper<PartialDatabase, DataFile> mapper,
            IDataManagerEventManager dataEventManager, Process process)
        {
            _dataFileManager = new DataFileManager();
            _databaseFileMapper = mapper;

            _databaseFolder = databaseFolder;
            _databaseExtension = databaseExtension;

            if (_databaseFileMapper is null)
            {
                _databaseFileMapper = new PartialDatabaseFileMapper();
            }

            _databases = new List<PartialDatabase>();

            _process = process;

        }
        #endregion

        #region Public Methods
        public void CreateDatabaseFromContractInfo(ContractInfo info, Process process)
        {
            var dbName = info.DatabaseName;

            if (!HasDatabase(dbName))
            {
                var reference = new DatabaseReference();
                reference.DatabaseName = dbName;
                var location = new Location(null, info.Location.IpAddress, info.Location.PortNumber, string.Empty);
                reference.Location = location;
                reference.DatabaseId = info.DatabaseId;

                var db = new PartialDatabase(dbName, process, reference);

                foreach(var table in info.Schema.Tables)
                {
                    db.AddTable(table);
                }

                _databases.Add(db);
                SaveToDisk(db);
            }
        }

        public PartialDatabase GetFullDatabase(string databaseName)
        {
            PartialDatabase db = null;

            Databases.ForEach(database =>
            {
                if ((database is PartialDatabase) && (database.Name == databaseName))
                {
                    db = database as PartialDatabase;
                }
            });

            return db;
        }
        public void AddDatabase(PartialDatabase database)
        {
            if (!HasDatabase(database.Name))
            {
                _databases.Add(database);
                SaveToDisk(database);
            }
        }

        public PartialDatabase GetDatabase(string databaseName)
        {
            return Databases.Where(d => d.Name == databaseName).FirstOrDefault();
        }

        public PartialDatabase GetDatabase(Guid? guid)
        {
            return Databases.Where(d => d.Id == guid).FirstOrDefault();
        }

        public bool HasDatabase(string databaseName)
        {
            return Databases.Any(d => d.Name == databaseName);
        }

        public bool HasDatabase(Guid guid)
        {
            return Databases.Any(d => d.Id == guid);
        }

        public void RemoveDatabase(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void RemoveDatabase(string databaseName)
        {
            File.Delete(_databaseFolder + @"\" + databaseName + _databaseExtension);
            var db = (PartialDatabase)_process.GetDatabase(databaseName);
            _databases.Remove(db);
        }

        public int LoadDatabases(string databaseFolderLocation, string partialDatabaseExtension)
        {
            int count = 0;

            foreach (var file in Directory.GetFiles(databaseFolderLocation, "*" + partialDatabaseExtension))
            {
                var database = GetDatabaseFromDisk(file);
                _databases.Add(database);
                count = Databases.Count;
            }

            return count;
        }

        public void SaveToDisk(PartialDatabase database)
        {
            var fileName = Path.Combine(_databaseFolder, database.Name + _databaseExtension);
            var file = _databaseFileMapper.Map(database);
            _dataFileManager.SaveDataFile(fileName, file);
        }
        #endregion

        #region Private Methods
        private PartialDatabase GetDatabaseFromDisk(string file)
        {
            var dataFile = _dataFileManager.GetDataFile(file);
            return _databaseFileMapper.Map(dataFile, _process);
        }

        private void RegisterEvents()
        {
            _dataEventManager.RegisterEvents();
        }
        #endregion

    }
}
