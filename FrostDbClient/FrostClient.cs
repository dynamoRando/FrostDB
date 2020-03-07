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
        double _queueTimeout = 30.0;
        string _localIpAddress;
        string _remoteIpAddress;
        int _remotePortNumber;
        int _localPortNumber;
        Server _localServer;
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

            SetupServer();
        }
        #endregion

        #region Public Methods

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

        public void GetPartialDatabases()
        {
            SendMessage(BuildMessage(string.Empty, MessageConsoleAction.Process.Get_Partial_Databases, MessageActionType.Process));
        }
        
        // i can either call a method and then try and wait for an event to be recieved
        public void GetDatabases()
        {
            SendMessage(BuildMessage(string.Empty, MessageConsoleAction.Process.Get_Databases, MessageActionType.Process));
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
            var id = SendMessage(BuildMessage(string.Empty, MessageConsoleAction.Process.Get_Pending_Process_Contracts, MessageActionType.Process));
            bool gotData = await WaitForMessageAsync(id);

            if (gotData)
            {
                if (_info.ProcessPendingContracts.ContainsKey(string.Empty))
                {
                    List<ContractInfo> removed = null;
                    _info.ProcessPendingContracts.TryRemove(string.Empty, out removed);
                    list = removed;
                }
            }

            return list;
        }

        public void GetContractInformation(string databaseName)
        {
            SendMessage(BuildMessage(databaseName, MessageConsoleAction.Database.Get_Contract_Information, MessageActionType.Database));  
        }

        public async Task<ContractInfo> GetContractInformationAsync(string databaseName)
        {
            var result = new ContractInfo();
            var id = SendMessage(BuildMessage(databaseName, MessageConsoleAction.Database.Get_Contract_Information, MessageActionType.Database));
            bool gotData = await WaitForMessageAsync(id);

            if (gotData)
            {
                if (_info.ContractInfos.ContainsKey(databaseName))
                {
                    ContractInfo removed = null;
                    _info.ContractInfos.TryRemove(databaseName, out removed);
                    result = removed;
                }
            }

            return result;
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

        public void GetTableInfo(string databaseName, string tableName)
        {
            DatabaseInfo item;
            if (_info.DatabaseInfos.TryGetValue(databaseName, out item))
            {
                var table = item.Tables.Where(t => t.Item2 == tableName).First();
                Guid? tableId = table.Item1;
                GetTableInfo(item.Id, tableId);
            }
        }

        public async Task<List<string>> GetTablesAsync(Guid? databaseId)
        {
            throw new NotImplementedException();
        }

        // or, i can send a message and then check for when the data has come back and return to the caller
        public async Task<List<string>> GetDatabasesAsync()
        {
            var result = new List<string>();
            var id = SendMessage(BuildMessage(string.Empty, MessageConsoleAction.Process.Get_Databases, MessageActionType.Process));
            bool gotData = await WaitForMessageAsync(id);

            if (gotData)
            {
                result = _info.DatabaseNames;
            }

            return result;
        }

        public async Task<DatabaseInfo> GetDatabaseInfoAsync(string databaseName)
        {
            var result = new DatabaseInfo();
            var id = SendMessage(BuildMessage(databaseName, MessageConsoleAction.Database.Get_Database_Info, MessageActionType.Database));

            bool gotData = await WaitForMessageAsync(id);

            if (gotData)
            {
                result = _info.DatabaseInfos.Where(d => d.Key == databaseName).First().Value;
            }

            return result;
        }

        public async Task<List<string>> GetPartialDatabasesAsync()
        {
            var result = new List<string>();
            var id = SendMessage(BuildMessage(string.Empty, MessageConsoleAction.Process.Get_Partial_Databases, MessageActionType.Process));
            bool gotData = await WaitForMessageAsync(id);

            if (gotData)
            {
                result = _info.PartialDatabaseNames;
            }

            return result;
        }

        public async Task<TableInfo> GetTableInfoAsync(Guid? databaseId, Guid? tableId, string tableName)
        {
            var requestInfo = (database: databaseId, table: tableId);
            var id = SendMessage(BuildMessage(requestInfo, MessageConsoleAction.Table.Get_Table_Info, MessageActionType.Table));

            var result = new TableInfo();
            bool gotData = await WaitForMessageAsync(id);

            if (gotData)
            {
                if (_info.TableInfos.ContainsKey(tableName))
                {
                    _info.TableInfos.TryRemove(tableName, out result);
                }
            }

            return result;
        }

        public void GetDatabaseInfo(string databaseName)
        {
            SendMessage(BuildMessage(databaseName, MessageConsoleAction.Database.Get_Database_Info, MessageActionType.Database));
        }

        public void GetAcceptedContractsForDb(string databaseName)
        {
            SendMessage(BuildMessage(databaseName, MessageConsoleAction.Database.Get_Accepted_Contracts, MessageActionType.Database));
        }

        public void GetPendingContractsForDb(string databaseName)
        {
            SendMessage(BuildMessage(databaseName, MessageConsoleAction.Database.Get_Pending_Contracts, MessageActionType.Database));
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private void GetTableInfo(Guid? databaseId, Guid? tableId)
        {
            var requestInfo = (database: databaseId, table: tableId);
            SendMessage(BuildMessage(requestInfo, MessageConsoleAction.Table.Get_Table_Info, MessageActionType.Table));
        }

        private async Task<bool> WaitForMessageAsync(Guid? id)
        {
            return await Task.Run(() => WaitForMessage(id));
        }

        private bool WaitForMessage(Guid? id)
        {
            Stopwatch watch = new Stopwatch();
            bool responseRecieved = false;

            watch.Start();

            while (watch.Elapsed.TotalSeconds < _queueTimeout)
            {
                if (!_info.HasMessageId(id))
                {
                    responseRecieved = true;

                    Debug.WriteLine(watch.Elapsed.TotalSeconds.ToString());
                    Console.WriteLine(watch.Elapsed.TotalSeconds.ToString());

                    break;

                }
                else
                {
                    continue;
                }
            }

            watch.Stop();

            return responseRecieved;
        }

        private Guid? SendMessage(Message message)
        {
            Guid? id = message.Id;
            // this timeout should be part of a configuration or a param passed in
            _info.AddToQueue(id);
            _client.Send(message, ClientConstants.TimeOut);
            return id;
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
        private void SetupServer()
        {
            _localServer = new Server();
            _localServer.Start(_localPortNumber, _localIpAddress, _processor);
        }
        #endregion


    }
}
