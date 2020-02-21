using FrostCommon.ConsoleMessages;
using FrostDbClient;
using FrostForm.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace FrostForm
{
    public class App
    {
        #region Private Fields
        FrostClient _client;
        formFrost _form;
        formNewDb _formNewDb;

        string _currentSelectedDbName = string.Empty;
        string _currentSelectedTableName = string.Empty;
        string _currentSelectedColumnName = string.Empty;
        Guid? _currentDbId;
        Guid? _currentTableId;
        Guid? _currentColumnId;
        #endregion

        #region Public Properties
        public FrostClient Client => _client;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public App(formFrost form)
        {
            _form = form;
            ListenForFormEvents();
        }
        #endregion

        #region Public Methods
        public void SetupClient(string remoteAddress, int remotePort, int localPort)
        {
            _client = new FrostClient(remoteAddress, "127.0.0.1", remotePort, localPort);
            ListenForAppEvents();
        }

        public void AcceptContract(ContractInfo contract)
        {
            _client.AcceptContract(contract);
        }

        public void RejectContract(ContractInfo contract)
        {
            _client.RejectContract(contract);
        }

        public void AddNewDb(string databaseName)
        {
            _client.AddNewDatabase(databaseName);
        }

        public void RemoveDb(string databaseName)
        {
            _client.RemoveDatabase(databaseName);
        }

        public void AddTableToDb(string databaseName, string tableName, List<(string, Type)> columns)
        {
            _client.AddTableToDb(databaseName, tableName, columns);
        }

        public void RemoveTableFromDb(string databaseName, string tableName)
        {
            _client.RemoveTableFromDb(databaseName, tableName);
        }

        public void AddColumnToTable(string databaseName, string tableName, string columnName, string dataType)
        {
            _client.AddColumnToTable(databaseName, tableName, columnName, dataType);
        }

        public async Task<List<ContractInfo>> GetPendingContractInformationAsync() 
        {
            var result = await _client.GetProcessPendingContractInformationAsync();
            return result;
        }

        public void GetPendingContractInformation()
        {
            _client.GetProcessPendingContractInformation();
        }

        public void RemoveColumnFromTable(string databaseName, string tableName, string columnName)
        {
            _client.RemoveColumnFromTable(databaseName, tableName, columnName);
        }

        public void GetContractInformation(string databaseName)
        {
            _client.GetContractInformation(databaseName);
        }

        public async Task<ContractInfo> GetContractInformationAsync(string databaseName)
        {
            var info = await _client.GetContractInformationAsync(databaseName);
            return info;
        }

        public void AddParticipantToDb(string ipAddress, string portNumber, string databaseName)
        {
            _client.AddParticipantToDb(ipAddress, portNumber, databaseName);
        }

        public void UpdateContractInformation(string databaseName, string contractDescription, List<(string, string, List<string>)> schemaData)
        {
            _client.UpdateContractInformation(databaseName, contractDescription, schemaData);
        }

        #endregion

        #region Private Methods
        private void ListenForAppEvents()
        {
            _client.EventManager.StartListening(ClientEvents.GotDatabaseNames, AddDbNames);
            _client.EventManager.StartListening(ClientEvents.GotDatabaseInfo, ShowDbInfo);
            _client.EventManager.StartListening(ClientEvents.GotTableInfo, ShowTableInfo);
            _client.EventManager.StartListening(ClientEvents.GotPendingContractInfo, ShowPendingContracts);
            _client.EventManager.StartListening(ClientEvents.GotAcceptedContractsInfo, ShowAcceptedContracts);
        }

        private void ShowAcceptedContracts(IEventArgs args)
        {
            AcceptedContractInfo item;
            if (_client.Info.AcceptedContractInfos.TryGetValue(_currentSelectedDbName, out item))
            {
                _form.listAcceptedParticipants.InvokeIfRequired(() =>
                {
                    _form.listAcceptedParticipants.Items.Clear();
                    item.AcceptedContracts.ForEach(i => _form.listAcceptedParticipants.Items.Add(i));
                });
            }
        }

        private void AddDbNames(IEventArgs args)
        {
            List<string> dbs = _client.Info.DatabaseNames;

            _form.listDatabases.InvokeIfRequired(() =>
            {
                _form.listDatabases.Items.Clear();
                dbs.ForEach(d => _form.listDatabases.Items.Add(d));
            });

        }

        private void ShowPendingContracts(IEventArgs args)
        {
            PendingContractInfo item;
            if (_client.Info.PendingContractInfos.TryGetValue(_currentSelectedDbName, out item))
            {
                _form.listPendingParticipants.InvokeIfRequired(() =>
                {
                    _form.listPendingParticipants.Items.Clear();
                    item.PendingContracts.ForEach(i => _form.listPendingParticipants.Items.Add(i));
                });
            }
        }

        private void ShowTableInfo(IEventArgs args)
        {
            TableInfo item;
            if (_client.Info.TableInfos.TryGetValue(_currentSelectedTableName, out item))
            {
                _form.listColumns.InvokeIfRequired(() =>
                {
                    _form.listColumns.Items.Clear();
                    item.Columns.ForEach(i => _form.listColumns.Items.Add(i.Item1));
                });
            }
        }

        private void ShowDbInfo(IEventArgs args)
        {
            string currentDb = string.Empty;

            _form.listDatabases.InvokeIfRequired(() =>
            {
                currentDb = _form.listDatabases.SelectedItem.ToString();
                _currentSelectedDbName = currentDb;
            });

            if (!string.IsNullOrEmpty(currentDb))
            {
                DatabaseInfo item;
                if (_client.Info.DatabaseInfos.TryGetValue(currentDb, out item))
                {
                    _form.labelDatabaseName.InvokeIfRequired(() => { _form.labelDatabaseName.Text = item.Name; });

                    _form.labelDatabaseId.InvokeIfRequired(() =>
                    {
                        _form.labelDatabaseId.Text = item.Id.ToString();
                        _currentDbId = item.Id;
                    });

                    _form.listTables.InvokeIfRequired(() =>
                    {
                        _form.listTables.Items.Clear();
                        item.Tables.ForEach(t => _form.listTables.Items.Add(t.Item2));
                    });

                    _form.listColumns.InvokeIfRequired(() => { _form.listColumns.Items.Clear(); });


                    _form.labelColumnName.InvokeIfRequired(() =>
                    {
                        _form.labelColumnName.Text = string.Empty;
                    });

                    _form.labelColumnDataType.InvokeIfRequired(() =>
                    {
                        _form.labelColumnDataType.Text = string.Empty;
                    });
                }
            }
        }

        private void ListenForFormEvents()
        {
            _form.listDatabases.SelectedIndexChanged += ListDatabases_SelectedIndexChanged;
            _form.listTables.SelectedIndexChanged += ListTables_SelectedIndexChanged;
            _form.listColumns.SelectedIndexChanged += ListColumns_SelectedIndexChanged;
        }

        private void ListColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_form.listColumns.SelectedItem != null)
            {
                string currentColumn = _form.listColumns.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(currentColumn))
                {
                    _currentSelectedColumnName = currentColumn;

                    TableInfo info = null;
                    if (_client.Info.TableInfos.TryGetValue(_currentSelectedTableName, out info))
                    {
                        var column = info.Columns.Where(c => c.Item1 == _currentSelectedColumnName).First();

                        _form.labelColumnName.InvokeIfRequired(() =>
                        {
                            _form.labelColumnName.Text = _currentSelectedColumnName;
                        });

                        _form.labelColumnDataType.InvokeIfRequired(() =>
                        {
                            _form.labelColumnDataType.Text = column.Item2.ToString();
                        });
                    }
                }
            }
        }

        private void ListTables_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (_form.listTables.SelectedItem != null)
            {
                string currentTable = _form.listTables.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(currentTable))
                {
                    _currentSelectedTableName = currentTable;
                    _client.GetTableInfo(_currentSelectedDbName, _currentSelectedTableName);
                }
            }
        }

        private void ListDatabases_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (_form.listDatabases.SelectedItem != null)
            {
                string currentDb = _form.listDatabases.SelectedItem.ToString();

                if (!string.IsNullOrEmpty(currentDb))
                {
                    _currentSelectedDbName = currentDb;
                    _client.GetDatabaseInfo(currentDb);
                    _client.GetPendingContractsForDb(currentDb);
                    _client.GetAcceptedContractsForDb(currentDb);
                }
            }
        }
        #endregion



    }
}
