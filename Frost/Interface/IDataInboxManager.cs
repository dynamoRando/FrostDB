using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IDataInboxManager
    {
        public void AddToInbox(DataMessage message);
        public bool CheckInbox(Guid id);
        public IDBObject GetInboxMessageData(Guid id);
    }
}
