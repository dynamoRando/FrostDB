﻿using FrostDB.Enum;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;

namespace FrostDB.Base
{
    public class Process : IBaseProcess<IBaseDatabase>
    {
        #region Private Fields
        private ICommService _commService;
        #endregion

        #region Public Properties
        
        public BaseDataManager<IBaseDatabase> DatabaseManager { get; }
        public Guid? Id { get => Configuration.Id; }
        public string Name { get => Configuration.Name; }
        public static IProcessConfiguration Configuration { get; private set; }
        public List<IBaseDatabase> Databases => DatabaseManager.Databases;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Process()
        {
            SetConfiguration();
            ProcessReference.Process = this;
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
                new BaseDatabase(databaseName));
        }

        public virtual void AddPartialDatabase(string databaseName)
        {
            DatabaseManager.AddDatabase(
               new BasePartialDatabase(databaseName));
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

        public virtual IBaseDatabase GetDatabase(Guid id)
        {
            return DatabaseManager.GetDatabase(id);
        }
        public virtual IBaseDatabase GetDatabase(string databaseName)
        {
            return DatabaseManager.GetDatabase(databaseName);
        }

        public virtual BasePartialDatabase GetPartialDatabase(string databaseName)
        {
            BasePartialDatabase db = null;

            Databases.ForEach(database =>
            {
                if (database is BasePartialDatabase && database.Name == databaseName)
                {
                    db = database as BasePartialDatabase;
                }
            });

            return db;
        }

        public virtual BaseDatabase GetFullDatabase(string databaseName)
        {
            BaseDatabase db = null;

            Databases.ForEach(database =>
            {
                if (database is BaseDatabase && database.Name == databaseName)
                {
                    db = database as BaseDatabase;
                }
            });

            return db;
        }

        public virtual List<BasePartialDatabase> GetPartialDatabases()
        {
            var dbs = new List<BasePartialDatabase>();

            Databases.ForEach(database =>
            {
                if (database is BasePartialDatabase)
                {
                    dbs.Add(database as BasePartialDatabase);
                }
            });

            return dbs;
        }

        public virtual List<BaseDatabase> GetFullDatabases()
        {
            var dbs = new List<BaseDatabase>();

            Databases.ForEach(database => 
            { 
                if (database is BaseDatabase)
                {
                    dbs.Add(database as BaseDatabase);
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
                if (database is BasePartialDatabase)
                {
                    dbs.Add(database.Name);
                }
            });

            return dbs;
        }

        public static void AddRemoteRow(Row row, Location location)
        {
            throw new NotImplementedException();
        }

        public IBaseDatabase GetDatabase(Guid? databaseId)
        {
            return Databases.Where(d => d.Id == databaseId).First();
        }
        public Row GetRemoteRow(Location location, Guid? rowId)
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
