using FrostDB.Interface.IServiceResults;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrostDB.Interface
{
    public interface IRemoteService
    {
        IRegisterNewPartialDatabaseResult RegisterNewPartialDatabase(Contract contract);
        IAddRowToPartialDatabaseResult AddRowToPartialDatabase
            (Participant sourceParticipant, Guid? DatabaseId, Row row);
        IPendingContractResult AcceptContract(Contract contract);
        Row GetRow(Location location, Guid? databaseId, Guid? tableId, Guid? rowId);

        void StartService();
    }
}
