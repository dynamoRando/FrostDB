using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FrostDB.EventArgs;

namespace FrostDB.Base
{
    public class ContractManager : IContractManager
    {
        #region Private Fields
        private Process _process;
        private List<Contract> _contracts;
        #endregion

        #region Public Properties
        public List<Contract> Contracts => _contracts;
        #endregion

        #region Constructors
        public ContractManager() { }
        public ContractManager(Process process)
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public void AddPendingContract(Contract contract)
        {
            _contracts.Add(contract);

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
