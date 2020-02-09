using FrostCommon;
using FrostCommon.ConsoleMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class MessageConsoleProcessorProcess
    {
		#region Private Fields
		#endregion

		#region Public Properties
		#endregion

		#region Protected Methods
		#endregion

		#region Events
		#endregion

		#region Constructors
		public MessageConsoleProcessorProcess()
		{

		}
		#endregion

		#region Public Methods
        public void Process(Message message)
        {
            switch (message.Action)
            {
                case MessageConsoleAction.Process.Get_Databases:
                    HandleProcessGetDatabases(message);
                    break;
                case MessageConsoleAction.Process.Get_Id:
                    HandleGetProcessId(message);
                    break;
                case MessageConsoleAction.Process.Add_Database:
                    HandleAddNewDatabase(message);
                    break;
                case MessageConsoleAction.Process.Remove_Datababase:
                    HandleRemoveDatabase(message);
                    break;
                case MessageConsoleAction.Process.Get_Pending_Process_Contracts:
                    HandleGetPendingProcessContracts(message);
                    break;
                case MessageConsoleAction.Process.Accept_Pending_Contract:
                    HandleAcceptPendingContract(message);
                    break;
                default:
                    throw new NotImplementedException("Unknown message console message");
            }
        }
        #endregion

        #region Private Methods
        private void HandleProcessGetDatabases(Message message)
        {
            string messageContent = string.Empty;

            List<string> databases = new List<string>();
            ProcessReference.Process.Databases.ForEach(d => databases.Add(d.Name));
            Type type = databases.GetType();
            messageContent = JsonConvert.SerializeObject(databases);

            MessageBuilder.Send(message, messageContent, MessageConsoleAction.Process.Get_Databases_Response, type, MessageActionType.Process);
        }

        private void HandleGetProcessId(Message message)
        {
            string messageContent = string.Empty;
            Type type = ProcessReference.Process.Id.GetType();
            messageContent = JsonConvert.SerializeObject(ProcessReference.Process.Id);

            MessageBuilder.Send(message, messageContent, MessageConsoleAction.Process.Get_Id_Response, type, MessageActionType.Process);
        }

        private void HandleAddNewDatabase(Message message)
        {
            ProcessReference.AddDatabase(message.Content);
            MessageBuilder.Send(message, string.Empty, MessageConsoleAction.Process.Add_Database_Response, message.Content.GetType(), MessageActionType.Process);
        }

        private void HandleRemoveDatabase(Message message)
        {
            ProcessReference.RemoveDatabase(message.Content);
            MessageBuilder.Send(message, string.Empty, MessageConsoleAction.Process.Remove_Database_Response, message.Content.GetType(), MessageActionType.Process);
        }

        private void HandleGetPendingProcessContracts(Message message)
        {
            var list = ProcessReference.GetPendingProcessContracts();
            var info = new List<ContractInfo>();

            list.ForEach(c =>
            {
                var t = new ContractInfo();

                t.ContractDescription = c.ContractDescription;
                t.DatabaseName = c.DatabaseName;
                t.ContractVersion = c.ContractVersion;
                t.ContractId = c.ContractId;
                t.Location.IpAddress = c.DatabaseLocation.IpAddress;
                t.Location.PortNumber = c.DatabaseLocation.PortNumber;

                // TODO: Need to fix this mapping up.

                info.Add(t);
            });

            Type type = info.GetType();
            string messageContent = string.Empty;

            messageContent = JsonConvert.SerializeObject(info);
            MessageBuilder.Send(message, messageContent, MessageConsoleAction.Process.Get_Pending_Process_Contracts_Respoonse, type, MessageActionType.Process);
        }

        private void HandleAcceptPendingContract(Message message)
        {
            /*
             * We need to accept the incoming contract on our side (mark it as accepted)
             * and then create a new partial database on our side
             * and then send to the original host of the database that we accept the contract
             * 
             */

            var contract = message.GetContentAs<ContractInfo>();
            ProcessReference.AcceptPendingContract(contract);
            ProcessReference.AddPartialDatabase(contract.DatabaseName);

            throw new NotImplementedException();
        }

        #endregion


    }
}
