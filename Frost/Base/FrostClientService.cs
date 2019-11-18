using FrostDB.Interface;
using FrostDB.Interface.IResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class FrostClientService : IFrostClientService
    {
        #region Private Fields
        #endregion

        #region Public Methods
        public IAddRowToPartialDatabase AddRowToPartialDatabase(Participant sourceParticipant, Guid? DatabaseId, Row row)
        {
            throw new NotImplementedException();
        }

        public IRegisterNewPartialDatabase RegisterNewPartialDatabase(Contract contract)
        {
            var process = GetProcess();

            if (!process.HasDatabase(contract.DatabaseId))
            {
                if (!process.HasContract(contract))
                {
                    process.AddPendingContract(contract);
                }
                // add the new partial database to the process
                // if the user accepts the contract
            }

            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private Process GetProcess()
        {
            return ProcessReference.Process;
        }
        #endregion
    }
}
