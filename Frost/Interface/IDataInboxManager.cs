using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrostDB.Interface
{
    public interface IDataInboxManager
    {
        public void AddToInbox(DataMessage message);
        public bool CheckInbox(Guid id);
        public Task<IDBObject> GetInboxMessageDataAsync(Guid id);
    }
}
