using System;
using System.Diagnostics;
using System.Net;
using FrostCommon;
using FrostCommon.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using FrostCommon.ConsoleMessages;
using System.Linq;

namespace FrostDbClient
{
    public class FrostClient
    {
        #region Private Fields
        double _queueTimeout = 10.0;
        string _localIpAddress;
        string _remoteIpAddress;
        int _remotePortNumber;
        int _localPortNumber;
        MessageClientConsoleProcessor _processor;
        Location _local;
        Location _remote;
        FrostClientInfo _info;
        EventManager _eventManager;
        Client _client;
        #endregion

        #region Public Properties
        public FrostClientInfo Info => _info;
        public EventManager EventManager => _eventManager;
        public string RemoteIpAddress => _remoteIpAddress;
        public string LocalIpAddress => _localIpAddress;
        public int RemotePortNumber => _remotePortNumber;
        public int LocalPortNumber => _localPortNumber;
        public Client Client => _client;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public FrostClient(string remoteIpAddress, string localIpAddress, int remotePortNumber, int localPortNumber)
        {
            _client = new Client();
            _remoteIpAddress = remoteIpAddress;
            _remotePortNumber = remotePortNumber;
            _localPortNumber = localPortNumber;
            _localIpAddress = localIpAddress;

            _eventManager = new EventManager();

            _local = new Location(Guid.NewGuid(), _localIpAddress, _localPortNumber, "FrostDbClient");
            _remote = new Location(Guid.NewGuid(), _remoteIpAddress, _remotePortNumber, string.Empty);
            _info = new FrostClientInfo();
            _processor = new MessageClientConsoleProcessor(ref _info, ref _eventManager);

        }
        #endregion

        #region Public Methods
        public void DisconnectClient()
        {
            try
            {
                //_client.DisconnectSocket();
                Client.Shutdown();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }

        }

        public FrostPromptResponse ExecuteCommand(string command)
        {
            var result = new FrostPromptResponse();
            var data = SendMessage(BuildMessage(command, MessageConsoleAction.Prompt.Execute_Command, MessageActionType.Prompt));
            return data.GetContentAs<FrostPromptResponse>();
        }
        public void AcceptContract(ContractInfo contract)
        {
            SendMessage(BuildMessage(Json.SeralizeObject(contract), MessageConsoleAction.Process.Accept_Pending_Contract, MessageActionType.Process));
        }

        public void RejectContract(ContractInfo contract)
        {
            SendMessage(BuildMessage(Json.SeralizeObject(contract), MessageConsoleAction.Process.Reject_Pending_Contract, MessageActionType.Process));
        }

        public void GetProcessId()
        {
            SendMessage(BuildMessage(string.Empty, MessageConsoleAction.Process.Get_Id, MessageActionType.Process));
        }

        public void GetTables(Guid? databaseId)
        {
            SendMessage(BuildMessage(databaseId.ToString(), MessageConsoleAction.Database.Get_Database_Tables, MessageActionType.Database));
        }

        public void GetColumnInfo(string databaseName, string tableName, string columnName)
        {
            TableInfo item;
            if (_info.TableInfos.TryGetValue(columnName, out item))
            {
                var column = item.Columns.Where(c => c.Item1 == columnName).First();

            }
            throw new NotImplementedException();
        }

        public void AddParticipantToDb(string ipAddress, string portNumber, string databaseName)
        {
            // TO DO: update the UI to confirm that the message was sent
            ParticipantInfo info = new ParticipantInfo();
            info.IpAddress = ipAddress;
            info.PortNumber = Convert.ToInt32(portNumber);
            info.DatabaseName = databaseName;

            SendMessage(BuildMessage(Json.SeralizeObject(info), MessageConsoleAction.Database.Add_Participant, MessageActionType.Database));
        }

        public void UpdateContractInformation(string databaseName, string contractDescription, List<(string, string, List<string>)> schemaData)
        {
            ContractInfo info = new ContractInfo();

            info.ContractDescription = contractDescription;
            info.SchemaData = schemaData;
            info.DatabaseName = databaseName;

            SendMessage(BuildMessage(Json.SeralizeObject(info), MessageConsoleAction.Database.Update_Contract_Information, MessageActionType.Database));
        }

        public void GetProcessPendingContractInformation()
        {
            SendMessage(BuildMessage(string.Empty, MessageConsoleAction.Process.Get_Pending_Process_Contracts, MessageActionType.Process));
        }

        public async Task<List<ContractInfo>> GetProcessPendingContractInformationAsync()
        {
            var list = new List<ContractInfo>();
            var data = SendMessage(BuildMessage(string.Empty, MessageConsoleAction.Process.Get_Pending_Process_Contracts, MessageActionType.Process));
            _processor.Process(data);

            List<ContractInfo> removed = null;
            _info.ProcessPendingContracts.TryRemove(string.Empty, out removed);
            list = removed;

            return list;
        }

        public ContractInfo GetContractInformation(string databaseName)
        {
            var data = SendMessage(BuildMessage(databaseName, MessageConsoleAction.Database.Get_Contract_Information, MessageActionType.Database));
            return data.GetContentAs<ContractInfo>();
        }

        public void RemoveColumnFromTable(string databaseName, string tableName, string columnName)
        {
            ColumnInfo info = new ColumnInfo();
            info.DatabaseName = databaseName;
            info.TableName = tableName;
            info.ColumnName = columnName;

            SendMessage(BuildMessage(Json.SeralizeObject(info), MessageConsoleAction.Table.Remove_Column, MessageActionType.Table));

        }

        public void AddColumnToTable(string databaseName, string tableName, string columnName, string dataType)
        {
            ColumnInfo info = new ColumnInfo();
            info.DatabaseName = databaseName;
            info.TableName = tableName;
            info.ColumnName = columnName;
            info.Type = Type.GetType(dataType);

            SendMessage(BuildMessage(Json.SeralizeObject(info), MessageConsoleAction.Table.Add_Column, MessageActionType.Table));
        }

        public void AddTableToDb(string databaseName, string tableName, List<(string, Type)> columns)
        {
            var info = new TableInfo();
            info.Columns.AddRange(columns);
            info.TableName = tableName;
            info.DatabaseName = databaseName;
            SendMessage(BuildMessage(Json.SeralizeObject(info), MessageConsoleAction.Database.Add_Table_To_Database, MessageActionType.Database));
        }

        public void RemoveTableFromDb(string databaseName, string tableName)
        {
            var info = new TableInfo();
            info.TableName = tableName;
            info.DatabaseName = databaseName;
            SendMessage(BuildMessage(Json.SeralizeObject(info), MessageConsoleAction.Database.Remove_Table_From_Database, MessageActionType.Database));
        }

        public void AddNewDatabase(string databaseName)
        {
            SendMessage(BuildMessage(databaseName, MessageConsoleAction.Process.Add_Database, MessageActionType.Process));
        }

        public void RemoveDatabase(string databaseName)
        {
            SendMessage(BuildMessage(databaseName, MessageConsoleAction.Process.Remove_Datababase, MessageActionType.Process));
        }

        // or, i can send a message and then check for when the data has come back and return to the caller
        public List<string> GetDatabases()
        {
            var response = SendMessage(BuildMessage(string.Empty, MessageConsoleAction.Process.Get_Databases, MessageActionType.Process));
            return response.GetContentAs<List<string>>();
        }

        public async Task<DatabaseInfo> GetDatabaseInfoAsync(string databaseName)
        {
            var result = new DatabaseInfo();
            var data = SendMessage(BuildMessage(databaseName, MessageConsoleAction.Database.Get_Database_Info, MessageActionType.Database));
            _processor.Process(data);
            result = _info.DatabaseInfos.Where(d => d.Key == databaseName).First().Value;

            return result;
        }

        public List<string> GetPartialDatabases()
        {
            var data = SendMessage(BuildMessage(string.Empty, MessageConsoleAction.Process.Get_Partial_Databases, MessageActionType.Process));
            return data.GetContentAs<List<string>>();
        }



        public void GetDatabaseInfo(string databaseName)
        {
            SendMessage(BuildMessage(databaseName, MessageConsoleAction.Database.Get_Database_Info, MessageActionType.Database));
        }

        public AcceptedContractInfo GetAcceptedContractsForDb(string databaseName)
        {
            var result = SendMessage(BuildMessage(databaseName, MessageConsoleAction.Database.Get_Accepted_Contracts, MessageActionType.Database));
            AcceptedContractInfo info = null;
            info = result.GetContentAs<AcceptedContractInfo>();
            return info;
        }

        public void GetPendingContractsForDb(string databaseName)
        {
            SendMessage(BuildMessage(databaseName, MessageConsoleAction.Database.Get_Pending_Contracts, MessageActionType.Database));
        }
        #endregion

        #region Private Methods
        private TableInfo GetTableInfo(Guid? databaseId, Guid? tableId)
        {
            var requestInfo = (database: databaseId, table: tableId);
            var result = SendMessage(BuildMessage(requestInfo, MessageConsoleAction.Table.Get_Table_Info, MessageActionType.Table));
            TableInfo info = result.GetContentAs<TableInfo>();
            return info;
        }
        public TableInfo GetTableInfo(string databaseName, string tableName)
        {
            DatabaseInfo item;
            TableInfo tableInfo = null;
            if (_info.DatabaseInfos.TryGetValue(databaseName, out item))
            {
                var table = item.Tables.Where(t => t.Item2 == tableName).FirstOrDefault();
                if (table.Item1.HasValue)
                {
                    Guid? tableId = table.Item1;
                    tableInfo = GetTableInfo(item.Id, tableId);
                }
            }
            return tableInfo;
        }

        public async Task<TableInfo> GetTableInfoAsync(string databaseName, string tableName)
        {
            var requestInfo = (database: databaseName, tableName: tableName);
            var data = SendMessage(BuildMessage(requestInfo, MessageConsoleAction.Table.Get_Table_Info, MessageActionType.Table));
            _processor.Process(data);

            var result = new TableInfo();

            if (_info.TableInfos.ContainsKey(tableName))
            {
                _info.TableInfos.TryRemove(tableName, out result);
            }

            return result;
        }

        public async Task<TableInfo> GetTableInfoAsync(Guid? databaseId, Guid? tableId, string tableName)
        {
            var requestInfo = (database: databaseId, table: tableId);
            var data = SendMessage(BuildMessage(requestInfo, MessageConsoleAction.Table.Get_Table_Info, MessageActionType.Table));
            _processor.Process(data);

            var result = new TableInfo();

            if (_info.TableInfos.ContainsKey(tableName))
            {
                _info.TableInfos.TryRemove(tableName, out result);
            }

            return result;
        }

        private Message SendMessage(Message message)
        {
            return _client.Send(message);
        }
        private Message BuildMessage((string, string) tuple, string action, MessageActionType actionType)
        {
            Message message = new Message(
             destination: _remote,
             origin: _local,
             messageContent: string.Empty,
             messageAction: action,
             messageType: MessageType.Console,
             messageActionType: actionType
             );

            message.TwoStringTuple = tuple;

            return message;
        }
        private Message BuildMessage((Guid?, Guid?) tuple, string action, MessageActionType actionType)
        {
            Message message = new Message(
              destination: _remote,
              origin: _local,
              messageContent: string.Empty,
              messageAction: action,
              messageType: MessageType.Console,
              messageActionType: actionType
              );

            message.TwoGuidTuple = tuple;

            return message;
        }
        private Message BuildMessage(string content, string action, MessageActionType actionType)
        {
            Message message = new Message(
               destination: _remote,
               origin: _local,
               messageContent: content,
               messageAction: action,
               messageType: MessageType.Console,
               messageActionType: actionType
               );

            return message;
        }
        #endregion


    }
}
