using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using FrostCommon;
using FrostCommon.Net;
using FrostCommon.ConsoleMessages;
using System.Xml;
using System.Reflection;
using System.IO;
using FrostDB.Processing;

/*
 * May you do good and not evil.
 * May you find forgiveness for yourself and forgive others.
 * May you share freely, never taking more than you give.
 */

namespace FrostDB
{
    /// <summary>
    /// The root FrostDB process.
    /// </summary>
    public class Process : IBaseProcess<IDatabase>
    {
        #region Private Fields
        private IContractManager _contractManager;
        private Network _networkManager;
        private DatabaseManager _dbManager;
        private DatabaseManager2 _dbManager2;
        private PartialDatabaseManager _pdbManager;
        private EventManager _eventManager;
        private ProcessLogger _log;
        private QueryParser _parser;
        private QueryManager _queryManager;
        private Cache _cache;
        #endregion

        #region Public Properties
        public EventManager EventManager => _eventManager;
        public DatabaseManager DatabaseManager => _dbManager;
        internal DatabaseManager2 DatabaseManager2 => _dbManager2;
        public PartialDatabaseManager PartialDatabaseManager => _pdbManager;
        public Guid? Id { get => Configuration.Id; }
        public string Name { get => Configuration.Name; }
        public IProcessConfiguration Configuration { get; private set; }
        public List<Database> Databases => DatabaseManager.Databases;
        public List<string> Databases2 => DatabaseManager2.DatabaseNames;
        public List<PartialDatabase> PartialDatabases => PartialDatabaseManager.Databases;
        public List<Contract> Contracts => _contractManager.Contracts;
        public ContractManager ContractManager => (ContractManager)_contractManager;
        public Network Network => _networkManager;
        public ProcessLogger Log => _log;
        public Cache Cache => _cache;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Process()
        {
            SetupLogging();
            SetConfiguration();
            SetupManagers();

            _contractManager = new ContractManager(this);
            _networkManager = new Network(this);
        }
        public Process(string instanceIpAddress, int dataPortNumber, int consolePortNumber)
        {
            SetupLogging();

            var info = new ProcessInfo(OperatingSystem.GetOSPlatform());
            var configurator = new ProcessConfigurator(info);
            var config = configurator.GetConfiguration();

            config.Address = instanceIpAddress;
            config.DataServerPort = dataPortNumber;
            config.ConsoleServerPort = consolePortNumber;
            configurator.SaveConfiguration(config);
            Configuration = config;

            SetupManagers();

            _contractManager = new ContractManager(this);
            _networkManager = new Network(this);
        }

        public Process(string instanceIpAddress, int dataPortNumber, int consolePortNumber, string rootDirectory)
        {
            SetupLogging();
            var info = new ProcessInfo(OperatingSystem.GetOSPlatform());
            var configurator = new ProcessConfigurator(info);
            var config = configurator.GetConfiguration(rootDirectory);

            config.Address = instanceIpAddress;
            config.DataServerPort = dataPortNumber;
            config.ConsoleServerPort = consolePortNumber;
            config.ContractFolder = Path.Combine(rootDirectory, "contracts");
            config.DatabaseFolder = Path.Combine(rootDirectory, "dbs");
            configurator.SaveConfiguration(config);
            Configuration = config;

            _log.Debug(" --- Process started --- ");
            _log.Debug($"" +
                $"Instance IP: {instanceIpAddress.ToString()} " +
                $"Data Port: {dataPortNumber.ToString()} " +
                $"Console Port: {consolePortNumber.ToString()} " +
                $"Root Dir: {rootDirectory} " +
                $"");

            SetupManagers();

            _contractManager = new ContractManager(this);
            _networkManager = new Network(this);
        }
        #endregion

        #region Public Methods
        public Location GetLocation()
        {
            return Configuration.GetLocation();
        }

        public virtual void AddDatabase(string databaseName)
        {
            DatabaseManager.AddDatabase(
                new Database(databaseName, this));
        }

        public virtual void AddDatabase2(string databaseName)
        {
            _dbManager2.AddDatabase(new Database2(this, databaseName));
        }

        public virtual void AddPartialDatabase(string databaseName)
        {
            PartialDatabaseManager.AddDatabase(
               new PartialDatabase(databaseName, this));
        }

        public virtual void AddPartialDatabase(ContractInfo contractInfo)
        {
            PartialDatabaseManager.CreateDatabaseFromContractInfo(contractInfo, this);
        }

        public virtual void RemoveDatabase(Guid guid)
        {
            DatabaseManager.RemoveDatabase(guid);
        }

        public virtual void RemoveDatabase(string databaseName)
        {
            DatabaseManager.RemoveDatabase(databaseName);
        }

        public virtual Table GetTable(Guid? databaseId, Guid? tableId)
        {
            return GetDatabase(databaseId).GetTable(tableId);
        }

        public virtual Table GetTable(string databaseName, string tableName)
        {
            return GetDatabase(databaseName).GetTable(tableName);
        }

        public virtual int LoadPartialDatabases()
        {
            return PartialDatabaseManager.LoadDatabases(Configuration.DatabaseFolder, Configuration.PartialDatabaseExtension);
        }

        public virtual int LoadDatabases()
        {
            return DatabaseManager.LoadDatabases(Configuration.DatabaseFolder, Configuration.DatabaseExtension);
        }

        //public virtual int LoadDatabases2()
        //{
        //    return DatabaseManager.LoadDatabases2();
        //}

        public virtual IDatabase GetDatabase(string databaseName)
        {
            return DatabaseManager.GetDatabase(databaseName);
        }

        public bool HasDatabase2(string databaseName)
        {
            return _dbManager2.HasDatabase(databaseName.ToUpper());
        }

        public List<string> GetDatabaseNames()
        {
            var names = new List<string>();
            names.AddRange(_dbManager2.DatabaseNames);
            names.AddRange(Databases.Select(d => d.Name).ToList());

            return names;
        }

        public Database2 GetDatabase2(string databaseName)
        {
            return _dbManager2.GetDatabase(databaseName.ToUpper());
        }

        public Database2 GetDatabase2(int databaseId)
        {
            return _dbManager2.GetDatabase(databaseId);
        }

        public bool HasDatabase(string databaseName)
        {
            bool hasDatabase = false;
            hasDatabase = Databases.Any(d => d.Name.ToUpper() == databaseName.ToUpper());

            if (!hasDatabase)
            {
                hasDatabase = _dbManager2.HasDatabase(databaseName);
            }

            return hasDatabase;
        }

        public bool HasPartialDatabase(string databaseName)
        {
            return PartialDatabases.Any(d => d.Name == databaseName);
        }

        public bool HasPartialDatabase(Guid? databaseId)
        {
            return PartialDatabases.Any(d => d.Id == databaseId);
        }

        public virtual string GetTableName(string databaseName, Guid? tableId)
        {
            return Databases.Where(d => d.Name == databaseName).FirstOrDefault().Tables.Where(t => t.Id == tableId).First().Name;
        }

        public List<Page> GetPages(int databaseId, int tableId, List<Page> excludePages)
        {
            throw new NotImplementedException();
        }

        public virtual void Startup()
        {
            LoadDatabases();
            LoadPartialDatabases();
            StartRemoteServer();
            StartConsoleServer();
        }

        public virtual void UpdateContractInformation(ContractInfo info)
        {
            ContractManager.UpdateContractPermissions(info);
        }

        public virtual Row GetRow(Guid? databaseId, Guid? tableId, Guid? rowId)
        {
            return GetDatabase(databaseId).GetTable(tableId).GetRow(rowId);
        }

        public bool IsPartialDatabase(Guid? databaseId)
        {
            return PartialDatabases.Any(d => d.Id == databaseId);
        }

        public bool IsPartialDatabase(string databaseName)
        {
            return PartialDatabases.Any(d => d.Name == databaseName);
        }

        public bool HasDatabase(Guid? databaseId)
        {
            return Databases.Any(d => d.Id == databaseId);
        }
        public virtual PartialDatabase GetPartialDatabase(Guid? databaseId)
        {
            return PartialDatabases.Where(d => d.Id == databaseId).FirstOrDefault();
        }
        public virtual PartialDatabase GetPartialDatabase(string databaseName)
        {
            return PartialDatabases.Where(d => d.Name == databaseName).FirstOrDefault();
        }

        public FrostPromptPlan GetPlan(string command)
        {
            return _queryManager.GetPlanExplanation(command.ToUpper());
        }

        public FrostPromptResponse ExecuteQuery(string command)
        {
            FrostPromptResponse response = new FrostPromptResponse();
            var cmd = command.ToUpper();
            var plan = _queryManager.GetPlan(cmd);

            var executor = new QueryPlanExecutor(this);
            return executor.Execute(plan);
        }

        public FrostPromptResponse ExecuteCommand(string command)
        {
            FrostPromptResponse response = new FrostPromptResponse();

            IQuery query;
            if (_parser.IsValidCommand(command, this, out query))
            {
                var runner = new QueryRunner();
                response = runner.Execute(query);
            }
            else
            {
                response.IsSuccessful = false;
                response.Message = "Syntax incorrect";
            }

            return response;
        }

        public void AddPendingContract(Contract contract)
        {
            _contractManager.AddPendingContract(contract);
        }

        public bool HasContract(Contract contract)
        {
            return _contractManager.HasContract(contract.ContractId);
        }

        public virtual List<Contract> GetPendingContracts()
        {
            return _contractManager.GetContractsFromDisk().Where(c => c.IsAccepted == false).ToList();
        }

        public FrostConfiguration GetConfiguration()
        {
            return (FrostConfiguration)this.Configuration;
        }

        public IDatabase GetDatabase(Guid? databaseId)
        {
            return Databases.Where(d => d.Id == databaseId).FirstOrDefault();
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

        public void Setup()
        {
            SetupVersion2();
        }
        #endregion

        #region Private Methods
        private void SetupManagers()
        {
            _eventManager = new EventManager();

            var dbManager = new DataManagerEventManagerDatabase(this);

            _dbManager = new DatabaseManager(
               Configuration.DatabaseFolder,
               Configuration.DatabaseExtension,
               new DatabaseFileMapper(),
               dbManager,
               this
               );

            dbManager.Manager = DatabaseManager;
            dbManager.RegisterEvents();

            var partialDbManager = new DataManagerEventManagerPartialDatabase();

            _pdbManager = new PartialDatabaseManager(
                Configuration.DatabaseFolder,
                Configuration.PartialDatabaseExtension,
                new PartialDatabaseFileMapper(),
                partialDbManager,
                this
                );

            partialDbManager.Manager = PartialDatabaseManager;
            partialDbManager.RegisterEvents();

            _parser = new QueryParser(this);
            _queryManager = new QueryManager(this);
        }
        private void SetConfiguration()
        {
            var info = new ProcessInfo(OperatingSystem.GetOSPlatform());
            var configurator = new ProcessConfigurator(info);

            Configuration = configurator.GetConfiguration();
        }

        private void SetupLogging()
        {
            _log = new ProcessLogger(this);
        }

        private void SetupVersion2()
        {
            _dbManager2 = new DatabaseManager2(Configuration.DatabaseFolder, new StorageManager(this));
            _cache = new Cache(this);
        }

        #endregion
    }
}
