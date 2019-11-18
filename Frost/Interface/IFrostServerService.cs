using FrostDB;
using FrostDB.Interface.IServiceResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IFrostServerService
    {
        IPendingContractResult AcceptContract(Contract contract);

        void StartService();
    }
}
