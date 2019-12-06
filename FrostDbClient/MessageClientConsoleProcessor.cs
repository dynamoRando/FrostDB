using System;
using System.Linq;
using System.Collections.Generic;
using FrostCommon;
using Newtonsoft.Json;
using FrostCommon.ConsoleMessages;

namespace FrostDbClient
{
    internal class MessageClientConsoleProcessor : IMessageProcessor
    {

        #region Private Fields
        FrostClientInfo _info;
        EventManager _eventManager;
        #endregion

        #region Public Properties

        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public MessageClientConsoleProcessor(ref FrostClientInfo info, ref EventManager eventManager)
        {
            _info = info;
            _eventManager = eventManager;
        }
        #endregion

        #region Public Methods
        public void Process(IMessage message)
        {
            var m = (message as Message);
            if (m.Action.Contains("Process"))
            {
                HandleProcessMessage(m);
            }

            if (m.Action.Contains("Database"))
            {
                HandleDatabaseMessage(m);
            }

            HandleInfoQueue(m.ReferenceMessageId);

            //throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private void HandleInfoQueue(Guid? id)
        {
            if (_info.HasMessageId(id))
            {
                _info.RemoveFromQueue(id);
            }
        }
        private void HandleDatabaseMessage(Message message)
        {
            switch (message.Action)
            {
                case MessageConsoleAction.Database.Get_Database_Info_Response:
                    HandleDbInfo(message);
                    break;
            }
        }
        private void HandleProcessMessage(Message message)
        {
            switch (message.Action) 
            {
                case MessageConsoleAction.Process.Get_Databases_Response:
                    HandleDatabaseList(message);
                    break;
                case MessageConsoleAction.Process.Get_Id_Response:
                    HandleProcessId(message);
                    break;
            }
        }

        private void HandleDbInfo(Message message)
        {
            DatabaseInfo dbInformation = JsonConvert.DeserializeObject<DatabaseInfo>(message.Content);
            
            if (_info.DatabaseInfos.ContainsKey(dbInformation.Name))
            {
                DatabaseInfo removed = null;
                _info.DatabaseInfos.TryRemove(dbInformation.Name, out removed);
            }

            if(_info.DatabaseInfos.TryAdd(dbInformation.Name, dbInformation))
            {
                _eventManager.TriggerEvent(ClientEvents.GotDatabaseInfo, null);
            }
        }

        private void HandleProcessId(Message message)
        {
            _info.ProcessId = JsonConvert.DeserializeObject<Guid?>(message.Content);
            _eventManager.TriggerEvent(ClientEvents.GotProcessId, null);
        }

        private void HandleDatabaseList(Message message)
        {
            _info.DatabaseNames = JsonConvert.DeserializeObject<List<string>>(message.Content);
            _eventManager.TriggerEvent(ClientEvents.GotDatabaseNames, null);
        }
        #endregion


    }
}
