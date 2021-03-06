﻿using FrostCommon;
using FrostCommon.ConsoleMessages;
using FrostDB.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrostDB
{
    public class MessageConsoleProcessorDatabase : IMessageConsoleProcessorObject
    {

        #region Private Fields
        Process _process;
        MessageBuilder _messageBuilder;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public MessageConsoleProcessorDatabase(Process process)
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
                case MessageConsoleAction.Database.Get_Database_Info:
                    result = HandleGetDatabaseInfo(message);
                    break;
                case MessageConsoleAction.Database.Get_Database_Tables:
                    result = HandleGetDatabaseTables(message);
                    break;
                case MessageConsoleAction.Database.Add_Table_To_Database:
                    result = HandleAddNewTable(message);
                    break;
                case MessageConsoleAction.Database.Remove_Table_From_Database:
                    result = HandleRemoveTable(message);
                    break;
                case MessageConsoleAction.Database.Get_Contract_Information:
                    result = HandleGetContractInformation(message);
                    break;
                case MessageConsoleAction.Database.Update_Contract_Information:
                    result = HandleUpdateContractInformation(message);
                    break;
                case MessageConsoleAction.Database.Add_Participant:
                    result = HandleAddParticipant(message);
                    break;
                case MessageConsoleAction.Database.Get_Pending_Contracts:
                    result = HandleGetPendingContracts(message);
                    break;
                case MessageConsoleAction.Database.Get_Accepted_Contracts:
                    result = HandleGetAcceptedContracts(message);
                    break;
            }

            return result;
        }
        #endregion

        #region Private Methods
        private IMessage HandleGetAcceptedContracts(Message message)
        {
            var info = new AcceptedContractInfo();

            string dbName = message.Content;
            var db = _process.GetDatabase(dbName);
            Type type = null;

            if (db != null)
            {
                db.AcceptedParticipants.ForEach(p =>
                {
                    info.AcceptedContracts.Add(p.Location.IpAddress + ":" + p.Location.PortNumber.ToString());
                });

                info.DatabaseId = db.Id;
                info.DatabaseName = db.Name;
            }

            type = info.GetType();
            string messageContent = string.Empty;

            messageContent = JsonConvert.SerializeObject(info);
            return _messageBuilder.BuildMessage(message.Origin, messageContent, MessageConsoleAction.Database.Get_Accepted_Contracts_Response, type, message.Id, MessageActionType.Database);
        }

        private IMessage HandleGetDatabaseInfo(Message message)
        {
            string dbName = message.Content;
            DatabaseInfo info = new DatabaseInfo();
            string messageContent = string.Empty;
            Type type = null;

            var db = _process.GetDatabase(dbName);

            if (db is null)
            {
                var db2 = _process.GetDatabase2(dbName);
                db2.Tables.ForEach(t => info.AddToTables((t.TableId.ToString(), t.Name)));
            }
            else
            {
                info.Name = db.Name;
                info.Id = db.Id.ToString();

                db.Tables.ForEach(t => info.AddToTables((t.Id.ToString(), t.Name)));

                type = info.GetType();

            }

            messageContent = JsonConvert.SerializeObject(info);
            return _messageBuilder.BuildMessage(message.Origin, messageContent, MessageConsoleAction.Database.Get_Database_Info_Response, type, message.Id, MessageActionType.Database);
        }

        private IMessage HandleGetDatabaseTables(Message message)
        {
            throw new NotImplementedException();
        }
        private IMessage HandleAddNewTable(Message message)
        {
            IMessage result = new Message();
            var info = JsonConvert.DeserializeObject<TableInfo>(message.Content);
            var db = _process.GetDatabase(info.DatabaseName);

            var columns = new List<Column>();

            foreach (var c in info.Columns)
            {
                // this is an identity column
                if (c.Item2.ToString() == "System.Int64")
                {
                    continue;
                }
                var col = new Column(c.Item1, Type.GetType(c.Item2));
                columns.Add(col);
            }

            var table = new Table(info.TableName, columns, db.Id, _process);

            // add any identity columns via the method rather than just straight adding column
            if (info.Columns.Any(k => k.Item2.ToString() == "System.Int64"))
            {
                var item = info.Columns.Where(k => k.Item2.ToString() == "System.Int64").First();
                table.AddAutoNumColumn(item.Item1);
            }

            db.AddTable(table);
            return result;
        }

        private IMessage HandleRemoveTable(Message message)
        {
            IMessage result = new Message();
            var info = JsonConvert.DeserializeObject<TableInfo>(message.Content);
            var db = _process.GetDatabase(info.DatabaseName);
            db.RemoveTable(info.TableName);
            return result;
        }
        private IMessage HandleGetContractInformation(Message message)
        {
            var databaseName = message.Content;

            if (string.IsNullOrEmpty(databaseName))
            {
                return new Message();
            }

            var db = _process.GetDatabase(databaseName);

            var info = new ContractInfo();
            info.ContractDescription = db.Contract.ContractDescription;
            info.DatabaseName = db.Name;
            info.DatabaseId = db.Id;
            db.Tables.ForEach(t => info.TableNames.Add(t.Name));
            info.Schema = DbSchemaMapper.Map(db.Schema);

            foreach (var p in db.Contract.ContractPermissions)
            {
                (string, string, List<string>) item;
                item.Item1 = _process.GetTableName(databaseName, p.TableId);
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
            return _messageBuilder.BuildMessage(message.Origin, messageContent, MessageConsoleAction.Database.Get_Contract_Information_Response, type, message.Id, MessageActionType.Database);
        }

        private IMessage HandleUpdateContractInformation(Message message)
        {
            var info = message.GetContentAs<ContractInfo>();
            _process.UpdateContractInformation(info);
            return _messageBuilder.BuildMessage(message.Origin, string.Empty, MessageConsoleAction.Database.Update_Contract_Information_Response, message.Content.GetType(), message.Id, MessageActionType.Database);
        }

        private IMessage HandleAddParticipant(Message message)
        {
            ParticipantInfo info = new ParticipantInfo();
            info = message.GetContentAs<ParticipantInfo>();
            var db = _process.GetDatabase(info.DatabaseName);
            var participant = new Participant(new Location(Guid.NewGuid(), info.IpAddress, Convert.ToInt32(info.PortNumber), string.Empty));
            db.AddPendingParticipant(participant);
            return _messageBuilder.BuildMessage(message.Origin, string.Empty, MessageConsoleAction.Database.Add_Participant_Response, message.Content.GetType(), message.Id, MessageActionType.Database);
        }

        private IMessage HandleGetPendingContracts(Message message)
        {
            var info = new PendingContractInfo();

            string dbName = message.Content;
            var db = _process.GetDatabase(dbName);
            db.PendingParticipants.ForEach(p =>
            {
                info.PendingContracts.Add(p.Location.IpAddress + ":" + p.Location.PortNumber.ToString());
            });

            info.DatabaseId = db.Id;
            info.DatabaseName = db.Name;

            Type type = info.GetType();
            string messageContent = string.Empty;

            messageContent = JsonConvert.SerializeObject(info);
            return _messageBuilder.BuildMessage(message.Origin, messageContent, MessageConsoleAction.Database.Get_Pending_Contracts_Response, type, message.Id, MessageActionType.Database);
        }

        #endregion

    }
}
