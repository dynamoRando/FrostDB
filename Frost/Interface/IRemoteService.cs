using FrostDB.Interface.IServiceResults;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrostDB.Interface
{
    public interface IRemoteService
    {
        void AddPendingContract(Contract contract);
        void AcceptPendingContract(Participant participant);
        Row GetRow(Guid? databaseId, Guid? tableId, Guid? rowId);
        void SaveRow(Guid? databaseId, Guid? tableId, Row row);
        void StartService();
        void StopService();
    }
}
