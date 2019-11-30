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
        #endregion

        #region Public Properties

        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public MessageClientConsoleProcessor(ref FrostClientInfo info)
        {
            _info = info;
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
                case MessageConsoleAction.Process.Get_Id:
                    HandleProcessId(message);
                    break;
            }
        }

        private void HandleProcessId(Message message)
        {
            Guid? id;
            var content = message.Content;
            id = JsonConvert.DeserializeObject<Guid?>(content);

            _info.ProcessId = id;

            EventManager.TriggerEvent(ClientEvents.GotProcessId, null);
        }

        private void HandleDatabaseList(Message message)
        {
            List<string> databases = new List<string>();
            var content = message.Content;
            databases = JsonConvert.DeserializeObject<List<string>>(content);

            _info.DatabaseNames = databases;

            EventManager.TriggerEvent(ClientEvents.GotDatabaseNames, null);
        }
        #endregion


    }
}
