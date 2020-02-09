using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FrostDB.EventArgs;
using FrostCommon.ConsoleMessages;
using FrostDB.Enum;
using FrostCommon;

namespace FrostDB
{
    public class ContractManager : IContractManager
    {
        #region Private Fields
        private Process _process;
        private List<Contract> _contracts;
        private ContractFileManager _fileManager;
        #endregion

        #region Public Properties
        public List<Contract> Contracts => _contracts;
        #endregion

        #region Constructors
        public ContractManager()
        {
            _fileManager = new ContractFileManager();
            _contracts = new List<Contract>();
        }
        public ContractManager(Process process) : this()
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public void UpdateContractPermissions(ContractInfo info)
        {
            var db = ProcessReference.GetDatabase(info.DatabaseName);
            db.Contract.ContractPermissions.Clear();

            foreach (var item in info.SchemaData)
            {
                var tableName = item.Item1;

                Cooperator cooperator;

                if (item.Item2 == "Process")
                {
                    cooperator = Cooperator.Process;
                }
                else
                {
                    cooperator = Cooperator.Participant;
                }

                List<TablePermission> permissions = new List<TablePermission>();

                foreach (var k in item.Item3)
                {
                    switch (k)
                    {
                        case "None":
                            permissions.Add(TablePermission.None);
                            break;
                        case "All":
                            permissions.Add(TablePermission.All);
                            break;
                        case "Read":
                            permissions.Add(TablePermission.Read);
                            break;
                        case "Insert":
                            permissions.Add(TablePermission.Insert);
                            break;
                        case "Update":
                            permissions.Add(TablePermission.Update);
                            break;
                        case "Delete":
                            permissions.Add(TablePermission.Delete);
                            break;
                        default:
                            throw new InvalidOperationException("Unknown permission");
                    }
                }

                db.Contract.ContractPermissions.Add(new TableContractPermission(ProcessReference.GetTableId(db.Name, tableName), cooperator, permissions));
                db.Contract.ContractDescription = info.ContractDescription;

            }

            EventManager.TriggerEvent(EventName.Contract.Contract_Updated, CreatewNewContractUpdatedEventArgs((Database)db));
        }

        public List<Contract> GetContractsFromDisk()
        {
            _contracts = _fileManager.GetContracts(Process.Configuration.ContractFolder);
            return _contracts;
        }

        public void AcceptPendingContract(ContractInfo contract)
        {
            var localContract = _contracts.Where(c => c.DatabaseName == contract.DatabaseName).First();
            localContract.IsAccepted = true;
            SaveContract(localContract);
        }

        public void AddPendingContract(Contract contract)
        {
            _contracts.Add(contract);
            SaveContract(contract);
          
            EventManager.TriggerEvent(EventName.Contract.Pending_Added,
                CreateNewPendingContractEventArgs(contract));
        }
        public bool HasContract(Guid? contractId)
        {
            return _contracts.Any(c => c.ContractId == contractId);
        }
        #endregion

        #region Private Methods
        private void SaveContract(Contract contract)
        {
            _fileManager
              .SaveContract(contract,
              Process.Configuration.ContractFolder,
              Process.Configuration.ContractExtension);
        }
        private PendingContractAddedEventArgs CreateNewPendingContractEventArgs(Contract contract)
        {
            return new PendingContractAddedEventArgs { Contract = contract };
        }

        private ContractUpdatedEventArgs CreatewNewContractUpdatedEventArgs(Database database)
        {
            return new ContractUpdatedEventArgs { Database = database };
        }
        #endregion
    }
}
