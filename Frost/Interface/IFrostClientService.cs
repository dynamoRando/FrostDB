using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface.IResults;

namespace FrostDB.Interface
{
    public interface IFrostClientService
    {
        IRegisterDatabaseResult RegisterDatabase(Contract contract);
        IAddRowToDatabaseResult AddRowToDatabase(Participant sourceParticipant, Guid? DatabaseId, Row row);
    }
}
