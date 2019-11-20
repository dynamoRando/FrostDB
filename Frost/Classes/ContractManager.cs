using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FrostDB.EventArgs;

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
        }
        public ContractManager(Process process) : this()
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public void AddPendingContract(Contract contract)
        {
            _contracts.Add(contract);
            _fileManager
                .SaveContract(contract,
                Process.Configuration.ContractFolder,
                Process.Configuration.ContractExtension);

            EventManager.TriggerEvent(EventName.Contract.Pending_Added,
                CreateNewPendingContractEventArgs(contract));
        }
        public bool HasContract(Guid? contractId)
        {
            return _contracts.Any(c => c.ContractId == contractId);
        }
        #endregion

        #region Private Methods
        private PendingContractAddedEventArgs CreateNewPendingContractEventArgs(Contract contract)
        {
            return new PendingContractAddedEventArgs { Contract = contract };
        }
        #endregion
    }
}
