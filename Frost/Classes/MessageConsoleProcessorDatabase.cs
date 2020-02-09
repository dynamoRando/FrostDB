using FrostCommon;
using FrostCommon.ConsoleMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class MessageConsoleProcessorDatabase
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
        public MessageConsoleProcessorDatabase() { }
        #endregion

        #region Public Methods
        public void Process(Message message)
        {
            switch (message.Action)
            {
                case MessageConsoleAction.Database.Get_Database_Info:
                    HandleGetDatabaseInfo(message);
                    break;
                case MessageConsoleAction.Database.Get_Database_Tables:
                    HandleGetDatabaseTables(message);
                    break;
                case MessageConsoleAction.Database.Add_Table_To_Database:
                    HandleAddNewTable(message);
                    break;
                case MessageConsoleAction.Database.Remove_Table_From_Database:
                    HandleRemoveTable(message);
                    break;
                case MessageConsoleAction.Database.Get_Contract_Information:
                    HandleGetContractInformation(message);
                    break;
                case MessageConsoleAction.Database.Update_Contract_Information:
                    HandleUpdateContractInformation(message);
                    break;
                case MessageConsoleAction.Database.Add_Participant:
                    HandleAddParticipant(message);
                    break;
                case MessageConsoleAction.Database.Get_Pending_Contracts:
                    HandleGetPendingContracts(message);
                    break;
            }
        }
        #endregion

        #region Private Methods
        private void HandleGetDatabaseInfo(Message message)
        {
            string dbName = message.Content;
            var db = ProcessReference.GetDatabase(dbName);

            DatabaseInfo info = new DatabaseInfo();

            info.Name = db.Name;
            info.Id = db.Id;

            db.Tables.ForEach(t => info.AddToTables((t.Id, t.Name)));

            Type type = info.GetType();
            string messageContent = string.Empty;

            messageContent = JsonConvert.SerializeObject(info);
            MessageBuilder.SendResponse(message, messageContent, MessageConsoleAction.Database.Get_Database_Info_Response, type, MessageActionType.Database);
        }

        private void HandleGetDatabaseTables(Message message)
        {
            throw new NotImplementedException();
        }
        private void HandleAddNewTable(Message message)
        {
            var info = JsonConvert.DeserializeObject<TableInfo>(message.Content);
            var db = ProcessReference.GetDatabase(info.DatabaseName);

            var columns = new List<Column>();

            foreach (var c in info.Columns)
            {
                var col = new Column(c.Item1, c.Item2);
                columns.Add(col);
            }

            var table = new Table(info.TableName, columns, db.Id);
            db.AddTable(table);
        }

        private void HandleRemoveTable(Message message)
        {
            var info = JsonConvert.DeserializeObject<TableInfo>(message.Content);
            var db = ProcessReference.GetDatabase(info.DatabaseName);
            db.RemoveTable(info.TableName);
        }
        private void HandleGetContractInformation(Message message)
        {
            var databaseName = message.Content;
            var db = ProcessReference.GetDatabase(databaseName);

            var info = new ContractInfo();
            info.ContractDescription = db.Contract.ContractDescription;
            info.DatabaseName = db.Name;
            db.Tables.ForEach(t => info.TableNames.Add(t.Name));

            foreach (var p in db.Contract.ContractPermissions)
            {
                (string, string, List<string>) item;
                item.Item1 = ProcessReference.GetTableName(databaseName, p.TableId);
                item.Item2 = p.Cooperator.ToString();
                item.Item3 = new List<string>();

                foreach (var k in p.Permissions)
                {
                    item.Item3.Add(k.ToString());
                }

                info.SchemaData.Add(item);
            }

            Type type = info.GetType();
            string messageContent = string.Empty;

            messageContent = JsonConvert.SerializeObject(info);
            MessageBuilder.SendResponse(message, messageContent, MessageConsoleAction.Database.Get_Contract_Information_Response, type, MessageActionType.Database);
        }

        private void HandleUpdateContractInformation(Message message)
        {
            var info = message.GetContentAs<ContractInfo>();
            ProcessReference.UpdateContractInformation(info);
            MessageBuilder.SendResponse(message, string.Empty, MessageConsoleAction.Database.Update_Contract_Information_Response, message.Content.GetType(), MessageActionType.Database);
        }

        private void HandleAddParticipant(Message message)
        {
            ParticipantInfo info = new ParticipantInfo();
            info = message.GetContentAs<ParticipantInfo>();
            var db = ProcessReference.GetDatabase(info.DatabaseName);
            var participant = new Participant(new Location(Guid.NewGuid(), info.IpAddress, Convert.ToInt32(info.PortNumber), string.Empty));
            db.AddPendingParticipant(participant);
            MessageBuilder.SendResponse(message, string.Empty, MessageConsoleAction.Database.Add_Participant_Response, message.Content.GetType(), MessageActionType.Database);
        }

        private void HandleGetPendingContracts(Message message)
        {
            var info = new PendingContractInfo();

            string dbName = message.Content;
            var db = ProcessReference.GetDatabase(dbName);
            db.PendingParticipants.ForEach(p =>
            {
                info.PendingContracts.Add(p.Location.IpAddress + ":" + p.Location.PortNumber.ToString());
            });

            info.DatabaseId = db.Id;
            info.DatabaseName = db.Name;

            Type type = info.GetType();
            string messageContent = string.Empty;

            messageContent = JsonConvert.SerializeObject(info);
            MessageBuilder.SendResponse(message, messageContent, MessageConsoleAction.Database.Get_Pending_Contracts_Response, type, MessageActionType.Database);
        }

        #endregion

    }
}
