﻿using FrostDB.EventArgs;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FrostDB.Base
{
    public class DataManager<TDatabase> where TDatabase : IDatabase
    {
        #region Private Fields
        private string _databaseFolder;
        private string _databaseExtension;
        private IDataFileManager<DataFile> _dataFileManager;
        private IDatabaseFileMapper<TDatabase, DataFile, DataManager<TDatabase>> _databaseFileMapper;
        #endregion

        #region Public Properties
        public List<TDatabase> Databases { get; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DataManager()
        {
            RegisterEvents();
        }
        public DataManager(string databaseFolder,
            string databaseExtension,
            IDatabaseFileMapper<TDatabase, DataFile, DataManager<TDatabase>> mapper) : this()
        {
            Databases = new List<TDatabase>();

            _dataFileManager = new DataFileManager();
            _databaseFileMapper = mapper;

            _databaseFolder = databaseFolder;
            _databaseExtension = databaseExtension;
        }
        #endregion  

        #region Public Methods
        public void AddDatabase(TDatabase database)
        {
            if (!HasDatabase(database.Name))
            {
                Databases.Add(database);
                SaveToDisk(database);
            }
        }

        public TDatabase GetDatabase(string databaseName)
        {
            return Databases.Where(d => d.Name == databaseName).First();
        }

        public TDatabase GetDatabase(Guid guid)
        {
            return Databases.Where(d => d.Id == guid).First();
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
            throw new NotImplementedException();
        }

        public int LoadDatabases(string databaseFolderLocation)
        {
            int count = 0;

            foreach (var file in Directory.GetFiles(databaseFolderLocation))
            {
                var database = GetDatabaseFromDisk(file);
                Databases.Add(database);
                count = Databases.Count;
            }

            return count;
        }

        public void AddToInbox(IMessage message)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private TDatabase GetDatabaseFromDisk(string file)
        {
            var dataFile = _dataFileManager.GetDataFile(file);
            return _databaseFileMapper.Map(dataFile, this);
        }

        private void SaveToDisk(TDatabase database)
        {
            var fileName = _databaseFolder + database.Name + _databaseExtension;
            Save(database, fileName);

            //if (!File.Exists(fileName))
            //{
            //    Save(database, fileName);
            //}
            //else
            //{
            //    Console.WriteLine("Database already exists");
            //}
        }

        private void Save(TDatabase database, string fileName)
        {
            var file = _databaseFileMapper.Map(database);
            _dataFileManager.SaveDataFile(fileName, file);
        }

        private void RegisterEvents()
        {
            EventManager.StartListening("Table_Created", new Action<IEventArgs>(HandleCreatedTableEvent));
        }

        private void HandleCreatedTableEvent(IEventArgs e)
        {
            if (e is TableCreatedEventArgs)
            {
                var args = (TableCreatedEventArgs)e;

                if (args.Database is TDatabase)
                {
                    SaveToDisk((TDatabase)args.Database);
                }
            }
        }
        #endregion
    }
}
