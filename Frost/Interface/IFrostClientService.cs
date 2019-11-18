using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface.IResults;

namespace FrostDB.Interface
{
    public interface IFrostClientService
    {
        IRegisterNewPartialDatabaseResult RegisterNewPartialDatabase(Contract contract);
        IAddRowToPartialDatabaseResult AddRowToPartialDatabase
            (Participant sourceParticipant, Guid? DatabaseId, Row row);
    }
}
