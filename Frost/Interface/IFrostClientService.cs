using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface.IResults;

namespace FrostDB.Interface
{
    public interface IFrostClientService
    {
        IRegisterNewPartialDatabase RegisterNewPartialDatabase(Contract contract);
        IAddRowToPartialDatabase AddRowToPartialDatabase
            (Participant sourceParticipant, Guid? DatabaseId, Row row);
    }
}
