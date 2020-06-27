using System;
using System.Linq;
using System.Collections.Generic;
using FrostCommon;
using Newtonsoft.Json;
using FrostCommon.ConsoleMessages;
using FrostCommon.Net;

namespace FrostDbClient
{
    internal class MessageClientConsoleProcessor : IMessageProcessor
    {

        #region Private Fields
        FrostClientInfo _info;
        EventManager _eventManager;
        #endregion

        #region Public Properties
        public int PortNumber { get; set; }
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
        public IMessage ProcessWithResult(IMessage message)
        {
            throw new NotImplementedException();
        }

        public IMessage Process(IMessage message)
        {
            var m = (message as Message);
            IMessage result = null;

            switch (m.ActionType)
            {
                case MessageActionType.Process:
                    result = HandleProcessMessage(m);
                    break;
                case MessageActionType.Database:
                    result = HandleDatabaseMessage(m);
                    break;
                case MessageActionType.Table:
                    result = HandleTableMessage(m);
                    break;
                case MessageActionType.Prompt:
                    result = HandlePromptMessage(m);
                    break;
            }

            HandleInfoQueue(m.ReferenceMessageId);

            return result;
        }
        #endregion

        #region Private Methods
        private IMessage HandlePromptMessage(Message message)
        {
            IMessage result = new Message();
            FrostPromptResponse data = JsonConvert.DeserializeObject<FrostPromptResponse>(message.Content);

            if (_info.Responses.ContainsKey(message.ReferenceMessageId))
            {
                FrostPromptResponse removed = null;
                _info.Responses.TryRemove(message.ReferenceMessageId, out removed);
            }

            _info.Responses.TryAdd(message.ReferenceMessageId, data);
            return result;
        }
        private IMessage HandleTableMessage(Message message)
        {
            IMessage result = new Message();
            switch (message.Action)
            {
                case MessageConsoleAction.Table.Get_Table_Info_Response:
                    HandleGetTableInfo(message);
                    break;
            }

            return result;
        }

        private void HandleGetTableInfo(Message message)
        {
            TableInfo data = JsonConvert.DeserializeObject<TableInfo>(message.Content);

            if (_info.TableInfos.ContainsKey(data.TableName))
            {
                TableInfo removed = null;
                _info.TableInfos.TryRemove(data.TableName, out removed);
            }

            if (_info.TableInfos.TryAdd(data.TableName, data))
            {
                _eventManager.TriggerEvent(ClientEvents.GotTableInfo, null);
            }
        }

        private void HandleInfoQueue(Guid? id)
        {
            if (_info.HasMessageId(id))
            {
                _info.RemoveFromQueue(id);
            }
        }
        private IMessage HandleDatabaseMessage(Message message)
        {
            IMessage result = new Message();
            switch (message.Action)
            {
                case MessageConsoleAction.Database.Get_Database_Info_Response:
                    HandleDbInfo(message);
                    break;
                case MessageConsoleAction.Database.Get_Contract_Information_Response:
                    HandleContractInfo(message);
                    break;
                case MessageConsoleAction.Database.Get_Pending_Contracts_Response:
                    HandlePendingContractInfo(message);
                    break;
                case MessageConsoleAction.Database.Get_Accepted_Contracts_Response:
                    HandleAcceptedContractInfo(message);
                    break;
            }
            return result;
        }

        private void HandleAcceptedContractInfo(Message message)
        {
            AcceptedContractInfo info = JsonConvert.DeserializeObject<AcceptedContractInfo>(message.Content);

            if (_info.AcceptedContractInfos.ContainsKey(info.DatabaseName))
            {
                AcceptedContractInfo removed = null;
                _info.AcceptedContractInfos.TryRemove(info.DatabaseName, out removed);
            }

            if (_info.AcceptedContractInfos.TryAdd(info.DatabaseName, info))
            {
                _eventManager.TriggerEvent(ClientEvents.GotAcceptedContractsInfo, null);
            }
        }

        private void HandlePendingContractInfo(Message message)
        {
            PendingContractInfo info = JsonConvert.DeserializeObject<PendingContractInfo>(message.Content);

            if (_info.PendingContractInfos.ContainsKey(info.DatabaseName))
            {
                PendingContractInfo removed = null;
                _info.PendingContractInfos.TryRemove(info.DatabaseName, out removed);
            }

            if (_info.PendingContractInfos.TryAdd(info.DatabaseName, info))
            {
                _eventManager.TriggerEvent(ClientEvents.GotPendingContractInfo, null);
            }

        }

        private void HandleContractInfo(Message message)
        {
            ContractInfo info = JsonConvert.DeserializeObject<ContractInfo>(message.Content);

            if (_info.ContractInfos.ContainsKey(info.DatabaseName))
            {
                ContractInfo removed = null;
                _info.ContractInfos.TryRemove(info.DatabaseName, out removed);
            }

            if (_info.ContractInfos.TryAdd(info.DatabaseName, info))
            {
                _eventManager.TriggerEvent(ClientEvents.GotDatabaseContractInfo, null);
            }
        }

        private IMessage HandleProcessMessage(Message message)
        {
            IMessage result = new Message();
            switch (message.Action)
            {
                case MessageConsoleAction.Process.Get_Databases_Response:
                    HandleDatabaseList(message);
                    break;
                case MessageConsoleAction.Process.Get_Partial_Databases_Response:
                    HandlePartialDatabaseList(message);
                    break;
                case MessageConsoleAction.Process.Get_Id_Response:
                    HandleProcessId(message);
                    break;
                case MessageConsoleAction.Process.Get_Pending_Process_Contracts_Response:
                    HandleGetPendingProcessContractsResponse(message);
                    break;
            }
            return result;
        }

        private void HandleGetPendingProcessContractsResponse(Message message)
        {
            List<ContractInfo> info = JsonConvert.DeserializeObject<List<ContractInfo>>(message.Content);

            if (_info.ProcessPendingContracts.ContainsKey(string.Empty))
            {
                List<ContractInfo> removed = null;
                _info.ProcessPendingContracts.TryRemove(string.Empty, out removed);
            }

            if (_info.ProcessPendingContracts.TryAdd(string.Empty, info))
            {
                _eventManager.TriggerEvent(ClientEvents.GetProcessPendingContractInfo, null);
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

            if (_info.DatabaseInfos.TryAdd(dbInformation.Name, dbInformation))
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
        private void HandlePartialDatabaseList(Message message)
        {
            _info.PartialDatabaseNames = JsonConvert.DeserializeObject<List<string>>(message.Content);
            _eventManager.TriggerEvent(ClientEvents.GotPartialDatabaseNames, null);
        }
        #endregion


    }
}
