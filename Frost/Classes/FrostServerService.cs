using FrostDB.Interface;
using FrostDB.Interface.IServiceResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class FrostServerService : IFrostServerService
    {
        public IPendingContractResult AcceptContract(Contract contract)
        {
            throw new NotImplementedException();
        }
    }
}
