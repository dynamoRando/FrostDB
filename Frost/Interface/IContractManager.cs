using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IContractManager
    {
        bool HasContract(Guid? contractId);
        List<Contract> Contracts { get; }
        void AddPendingContract(Contract contract);
    }
}
