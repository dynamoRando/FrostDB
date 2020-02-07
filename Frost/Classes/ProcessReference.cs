using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface;
using System.Linq;
using FrostCommon.ConsoleMessages;

namespace FrostDB
{
    public static class ProcessReference
    {

        #region Private Fields
        #endregion

        #region Public Properties
        public static Process Process { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public static List<ContractInfo> GetPendingProcessContracts()
        {
            var contracts =  ProcessReference.Process.GetPendingContracts();
            var list = new List<ContractInfo>();

            contracts.ForEach(c => 
            {
                var info = new ContractInfo();

                info.ContractDescription = c.ContractDescription;
                info.DatabaseName = c.DatabaseName;
                info.ContractVersion = c.ContractVersion;
                info.ContractId = c.ContractId;

                // TODO: Need to fix this mapping up.

                list.Add(info);
            });

            return list;
        }
        public static void UpdateContractInformation(ContractInfo info)
        {
            ProcessReference.Process.ContractManager.UpdateContractPermissions(info);
        }
        public static bool HasDatabase(Guid? databaseId)
        {
            return ProcessReference.Process.Databases.Any(d => d.Id == databaseId);
        }
        public static string GetDatabaseName(Guid? databaseId)
        {
            return ProcessReference.Process.Databases.Where(d => d.Id == databaseId).First().Name;
        }
        public static Guid? GetDatabaseId(string databaseName)
        {
            return ProcessReference.Process.Databases.Where(d => d.Name == databaseName).First().Id;
        }
        public static string GetTableName(string databaseName, Guid? tableId)
        {
            return ProcessReference.GetDatabase(databaseName).GetTableName(tableId);
        }

        public static Guid? GetTableId(string databaseName, string tableName)
        {
            return ProcessReference.GetDatabase(databaseName).GetTableId(tableName);
        }

        public static void RemoveDatabase(string databaseName)
        {
            ProcessReference.Process.RemoveDatabase(databaseName);
        }
        public static void AddDatabase(string databaseName)
        {
            ProcessReference.Process.AddDatabase(databaseName);
        }
        public static Contract GetContract(Guid? databaseId)
        {
            return ProcessReference.Process.GetDatabase(databaseId).Contract;
        }

        public static IDatabase GetDatabase(string databaseName)
        {
            return ProcessReference.Process.GetDatabase(databaseName);
        }

        public static IDatabase GetDatabase(Guid? databaseId) 
        {
            return ProcessReference.Process.GetDatabase(databaseId);
        }

        public static Table GetTable(Guid? databaseId, Guid? tableId)
        {
            return ProcessReference.Process.GetDatabase(databaseId).GetTable(tableId);
        }

        public static Row GetRow(Guid? databaseId, Guid? tableId, Guid? rowId)
        {
            return ProcessReference.Process.GetDatabase(databaseId).GetTable(tableId).GetRow(rowId);
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
