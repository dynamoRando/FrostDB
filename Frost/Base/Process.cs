using FrostDB.Enum;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;

namespace FrostDB.Base
{
    public class Process : IProcess<Database, PartialDatabase>
    {
        #region Private Fields
        private ICommService _commService;
        #endregion

        #region Public Properties
        public List<Database> Databases => DatabaseManager.Databases;
        public List<PartialDatabase> PartialDatabases => PartialDatabaseManager.Databases;
        public DataManager<Database> DatabaseManager { get; }
        public DataManager<PartialDatabase> PartialDatabaseManager { get; }
        public Guid? Id { get => Configuration.Id; }
        public string Name { get => Configuration.Name; }
        public static IProcessConfiguration Configuration { get; private set; }
        public static List<IBaseDatabase> Data { get; set; }
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

            PartialDatabaseManager = new PartialDatabaseManager(
                new PartialDatabaseFileMapper(),
                Configuration.DatabaseFolder,
                Configuration.PartialDatabaseExtension);
        }
        #endregion

        #region Public Methods
        public static ILocation GetLocation()
        {
            return Configuration.GetLocation();
        }
        public virtual void AddDatabase(string databaseName)
        {
            DatabaseManager.AddDatabase(
                new Database(databaseName, DatabaseManager));
        }

        public virtual void AddPartialDatabase(string databaseName)
        {
            PartialDatabaseManager.AddDatabase(
                new PartialDatabase(databaseName, PartialDatabaseManager));
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
        public virtual Database GetDatabase(string databaseName)
        {
            return DatabaseManager.GetDatabase(databaseName);
        }

        public virtual PartialDatabase GetPartialDatabase(string databaseName)
        {
            return PartialDatabaseManager.GetDatabase(databaseName);
        }

        public List<string> GetDatabases()
        {
            var dbs = new List<string>();

            Databases.ForEach(d => {
                dbs.Add(d.Name);
            });

            return dbs;
        }

        public List<string> GetPartialDatabases()
        {
            var dbs = new List<string>();

            PartialDatabases.ForEach(d => {
                dbs.Add(d.Name);
            });

            return dbs;
        }

        public static void AddRemoteRow(Row row, Location location)
        {
            throw new NotImplementedException();
        }

        public static IBaseDatabase GetDatabase(Guid? databaseId)
        {
            return Data.Where(d => d.Id == databaseId).First();
        }
        public static Row GetRemoteRow(Location location, Guid? rowId)
        {
            throw new NotImplementedException();
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
