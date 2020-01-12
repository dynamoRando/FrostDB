﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using FrostCommon;
using FrostCommon.Net;

namespace FrostDB
{
    public class Process : IBaseProcess<IDatabase>
    {
        #region Private Fields
        private IContractManager _contractManager;
        private Network _networkManager;
        #endregion

        #region Public Properties

        public DataManager<IDatabase> DatabaseManager { get; }
        public Guid? Id { get => Configuration.Id; }
        public string Name { get => Configuration.Name; }
        public static IProcessConfiguration Configuration { get; private set; }
        public List<IDatabase> Databases => DatabaseManager.Databases;
        public List<Contract> Contracts => _contractManager.Contracts;
        public ContractManager ContractManager => (ContractManager)_contractManager;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Process()
        {
            SetConfiguration();

            DatabaseManager = new DatabaseManager(
               new DatabaseFileMapper(),
               Configuration.DatabaseFolder,
               Configuration.DatabaseExtension);

            _contractManager = new ContractManager(this);
            _networkManager = new Network();

            ProcessReference.Process = this;
            NetworkReference.Network = _networkManager;
        }
        public Process(string instanceIpAddress, int dataPortNumber, int consolePortNumber) 
        {
            var info = new ProcessInfo(OperatingSystem.GetOSPlatform());
            var configurator = new ProcessConfigurator(info);
            var config = configurator.GetConfiguration();

            config.Address = instanceIpAddress;
            config.DataServerPort = dataPortNumber;
            config.ConsoleServerPort = consolePortNumber;
            configurator.SaveConfiguration(config);
            Configuration = config;

            DatabaseManager = new DatabaseManager(
              new DatabaseFileMapper(),
              Configuration.DatabaseFolder,
              Configuration.DatabaseExtension);

            _contractManager = new ContractManager(this);
            _networkManager = new Network();

            ProcessReference.Process = this;
            NetworkReference.Network = _networkManager;
        }
        #endregion

        #region Public Methods
        public static Location GetLocation()
        {
            return Configuration.GetLocation();
        }
        public virtual void AddDatabase(string databaseName)
        {
            DatabaseManager.AddDatabase(
                new Database(databaseName));
        }

        public virtual void AddPartialDatabase(string databaseName)
        {
            DatabaseManager.AddDatabase(
               new PartialDatabase(databaseName));
        }

        public virtual void RemoveDatabase(Guid guid)
        {
            DatabaseManager.RemoveDatabase(guid);
        }

        public virtual void RemoveDatabase(string databaseName)
        {
            DatabaseManager.RemoveDatabase(databaseName);
        }

        public virtual int LoadDatabases()
        {
            return DatabaseManager.LoadDatabases(Configuration.DatabaseFolder);
        }

        public virtual IDatabase GetDatabase(Guid id)
        {
            return DatabaseManager.GetDatabase(id);
        }
        public virtual IDatabase GetDatabase(string databaseName)
        {
            return DatabaseManager.GetDatabase(databaseName);
        }

        public bool HasDatabase(string databaseName)
        {
            return Databases.Any(d => d.Name == databaseName);
        }

        public bool HasDatabase(Guid? databaseId)
        {
            return Databases.Any(d => d.Id == databaseId);
        }

        public virtual PartialDatabase GetPartialDatabase(string databaseName)
        {
            PartialDatabase db = null;

            Databases.ForEach(database =>
            {
                if (database is PartialDatabase && database.Name == databaseName)
                {
                    db = database as PartialDatabase;
                }
            });

            return db;
        }

        public void AddPendingContract(Contract contract)
        {
            _contractManager.AddPendingContract(contract);
        }

        public bool HasContract(Contract contract)
        {
            return _contractManager.HasContract(contract.ContractId);
        }

        public virtual Database GetFullDatabase(string databaseName)
        {
            Database db = null;

            Databases.ForEach(database =>
            {
                if (database is Database && database.Name == databaseName)
                {
                    db = database as Database;
                }
            });

            return db;
        }

        public virtual List<PartialDatabase> GetPartialDatabases()
        {
            var dbs = new List<PartialDatabase>();

            Databases.ForEach(database =>
            {
                if (database is PartialDatabase)
                {
                    dbs.Add(database as PartialDatabase);
                }
            });

            return dbs;
        }

        public virtual List<Database> GetFullDatabases()
        {
            var dbs = new List<Database>();

            Databases.ForEach(database => 
            { 
                if (database is Database)
                {
                    dbs.Add(database as Database);
                }
            });

            return dbs;
        }

        public List<string> GetDatabases()
        {
            var dbs = new List<string>();

            Databases.ForEach(d => {
                dbs.Add(d.Name);
            });

            return dbs;
        }

        public List<string> GetPartialDatabasesString()
        {
            var dbs = new List<string>();

            Databases.ForEach(database => 
            { 
                if (database is PartialDatabase)
                {
                    dbs.Add(database.Name);
                }
            });

            return dbs;
        }

        public IDatabase GetDatabase(Guid? databaseId)
        {
            return Databases.Where(d => d.Id == databaseId).First();
        }

        public void StartRemoteServer()
        {
            _networkManager.StartDataServer();
        }
        
        public void StopRemoteServer()
        {
            _networkManager.StopDataServer();
        }

        public void StartConsoleServer()
        {
            _networkManager.StartConsoleServer();
        }

        public void StopConsoleServer()
        {
            _networkManager.StopConsoleServer();
        }
        #endregion

        #region Private Methods
        private void SetConfiguration()
        {
            var info = new ProcessInfo(OperatingSystem.GetOSPlatform());
            var configurator = new ProcessConfigurator(info);

            Configuration = configurator.GetConfiguration();
        }
        #endregion
    }
}
