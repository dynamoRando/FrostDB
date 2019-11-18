using FrostDB.Base;
using FrostDB.Interface.IResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IFrostServerService
    {
        IPendingContractResult AcceptContract(Contract contract);
    }
}
