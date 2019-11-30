using System;
using System.Collections.Generic;
using System.Text;
using FrostCommon;
using Newtonsoft.Json;

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
            if (m.ReferenceMessageId.Value == Guid.Empty)
            {
                // do something with data that can be rendered to the UI
                if (m.Action.Contains("Process"))
                {
                    HandleProcessMessage(m);
                }

                if (m.Action.Contains("Database"))
                {
                    // call database processor, or whatever
                }

                //m.SendResponse();
            }
            else
            {
                // do nothing
            }

            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
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
