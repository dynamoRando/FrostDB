using FrostCommon;
using FrostCommon.ConsoleMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface;
using FrostDB.Extensions;

namespace FrostDB
{
    public class MessageConsoleProcessorProcess : IMessageConsoleProcessorObject
    {
        #region Private Fields
        private MessageBuilder _messageBuilder;
        private Process _process;
		#endregion

		#region Public Properties
		#endregion

		#region Protected Methods
		#endregion

		#region Events
		#endregion

		#region Constructors
		public MessageConsoleProcessorProcess(Process process)
		{
            _process = process;
            _messageBuilder = new MessageBuilder(_process);
		}
		#endregion

		#region Public Methods
        public IMessage Process(Message message)
        {
            IMessage result = null;
            switch (message.Action)
            {
                case MessageConsoleAction.Process.Get_Databases:
                    result = HandleProcessGetDatabases(message);
                    break;
                case MessageConsoleAction.Process.Get_Partial_Databases:
                    result = HandleProcessGetPartialDatabases(message);
                    break;
                case MessageConsoleAction.Process.Get_Id:
                    result = HandleGetProcessId(message);
                    break;
                case MessageConsoleAction.Process.Add_Database:
                    result = HandleAddNewDatabase(message);
                    break;
                case MessageConsoleAction.Process.Remove_Datababase:
                    result = HandleRemoveDatabase(message);
                    break;
                case MessageConsoleAction.Process.Get_Pending_Process_Contracts:
                    result = HandleGetPendingProcessContracts(message);
                    break;
                case MessageConsoleAction.Process.Accept_Pending_Contract:
                    result = HandleAcceptPendingContract(message);
                    break;
                default:
                    throw new NotImplementedException("Unknown message console message");
            }

            return result;
        }
        #endregion

        #region Private Methods
        private IMessage HandleProcessGetPartialDatabases(Message message)
        {
            string messageContent = string.Empty;

            List<string> databases = new List<string>();
            _process.PartialDatabases.ForEach(d => databases.Add(d.Name));
            Type type = databases.GetType();
            messageContent = JsonConvert.SerializeObject(databases);

            return _messageBuilder.BuildMessage(message.Origin, messageContent, MessageConsoleAction.Process.Get_Partial_Databases_Response, type, message.Id, MessageActionType.Process);
        }

        private IMessage HandleProcessGetDatabases(Message message)
        {
            string messageContent = string.Empty;

            List<string> databases = new List<string>();
            _process.Databases.ForEach(d => databases.Add(d.Name));
            Type type = databases.GetType();
            messageContent = JsonConvert.SerializeObject(databases);

            return _messageBuilder.BuildMessage(message.Origin, messageContent, MessageConsoleAction.Process.Get_Databases_Response, type, message.Id, MessageActionType.Process);
        }

        private IMessage HandleGetProcessId(Message message)
        {
            string messageContent = string.Empty;
            Type type = _process.Id.GetType();
            messageContent = JsonConvert.SerializeObject(_process.Id);

            return _messageBuilder.BuildMessage(message.Origin, messageContent, MessageConsoleAction.Process.Get_Id_Response, type, message.Id, MessageActionType.Process);
        }

        private IMessage HandleAddNewDatabase(Message message)
        {
            _process.AddDatabase(message.Content);
            return _messageBuilder.BuildMessage(message.Origin, string.Empty, MessageConsoleAction.Process.Add_Database_Response, message.Content.GetType(), message.Id, MessageActionType.Process);
        }

        private IMessage HandleRemoveDatabase(Message message)
        {
            _process.RemoveDatabase(message.Content);
            return _messageBuilder.BuildMessage(message.Origin, string.Empty, MessageConsoleAction.Process.Remove_Database_Response, message.Content.GetType(), message.Id, MessageActionType.Process);
        }

        private IMessage HandleGetPendingProcessContracts(Message message)
        {
            var list = _process.GetPendingContracts();
            var info = new List<ContractInfo>();

            list.ForEach(c =>
            {
                info.Add(c.Convert());
            });

            Type type = info.GetType();
            string messageContent = string.Empty;

            messageContent = JsonConvert.SerializeObject(info);
            return _messageBuilder.BuildMessage(message.Origin, messageContent, MessageConsoleAction.Process.Get_Pending_Process_Contracts_Response, type, message.Id, MessageActionType.Process);
        }

        private IMessage HandleAcceptPendingContract(Message message)
        {
            /*
             * We need to accept the incoming contract on our side (mark it as accepted)
             * and then create a new partial database on our side
             * and then send to the original host of the database that we accept the contract
             * 
             */

            var contract = message.GetContentAs<ContractInfo>();
            _process.ContractManager.AcceptPendingContract(contract);
            // TODO: we need to make sure the construction of a partial database is done correctly
            // so that there are no null values
            _process.AddPartialDatabase(contract);
            var location = contract.Location.Convert();

            Message acceptContract = new Message(location, _process.GetLocation(), contract.DatabaseName, MessageDataAction.Contract.Accept_Pending_Contract, MessageType.Data);
            _process.Network.SendMessage(acceptContract);
        }

        #endregion


    }
}
