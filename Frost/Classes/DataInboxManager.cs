using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FrostDB
{
    public class DataInboxManager : IManager, IDataInboxManager
    {
        #region Private Fields
        private ConcurrentBag<DataMessage> _messages;
        private ConcurrentBag<Guid> _messageIds;
        private int _timeoutInSeconds = 180;
        #endregion

        #region Public Properties
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DataInboxManager()
        {
            _messages = new ConcurrentBag<DataMessage>();
        }
        #endregion

        #region Public Methods
        public bool CheckInbox(Guid id)
        {
            return _messages.Any(m => m.Id == id);
        }
        public void AddToInbox(DataMessage message)
        {
            _messages.Add(message);
            _messageIds.Add(message.Id);
        }

        public async Task<IDBObject> GetInboxMessageDataAsync(Guid id)
        {
            return await Task.Run(() => GetData(id));
        }

        #endregion

        #region Private Methods
        private IDBObject GetData(Guid id)
        {
            DataMessage message = new DataMessage();
            Stopwatch watch = new Stopwatch();
            DbObject data = new DbObject();

            message = WaitForMessage(id, message, watch);

            if (message != null)
            {
                data = (DbObject)message.Data;
                Task.Run(() => _messages.TryTake(out message));
            }

            return data;
        }

        private DataMessage WaitForMessage(Guid id, DataMessage message, Stopwatch watch)
        {
            watch.Start();

            while (watch.Elapsed.TotalSeconds < _timeoutInSeconds)
            {
                if (_messageIds.Contains(id))
                {
                    message = _messages.Where(m => m.Id == id).First();
                }
                else
                {
                    continue;
                }
            }

            watch.Stop();

            return message;
        }
        #endregion


    }
}
