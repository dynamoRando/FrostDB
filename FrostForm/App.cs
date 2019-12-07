using FrostCommon.ConsoleMessages;
using FrostDbClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FrostForm
{
    public class App
    {
        #region Private Fields
        FrostClient _client;
        formFrost _form;
        
        string _currentSelectedDbName = string.Empty;
        string _currentSelectedTableName = string.Empty;
        Guid? _currentDbId;
        Guid? _currentTableId;
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
        public void SetupClient(string remoteAddress, int remotePort)
        {
            _client = new FrostClient(remoteAddress, "127.0.0.1", remotePort, 520);
            ListenForAppEvents();
        }
        #endregion

        #region Private Methods
        private void ListenForAppEvents()
        {
            _client.EventManager.StartListening(ClientEvents.GotDatabaseNames, AddDbNames);
            _client.EventManager.StartListening(ClientEvents.GotDatabaseInfo, ShowDbInfo);
            _client.EventManager.StartListening(ClientEvents.GotTableInfo, ShowTableInfo);
        }

        private void AddDbNames(IEventArgs args)
        {
            List<string> dbs = _client.Info.DatabaseNames;

            if (_form.listDatabases.InvokeRequired)
            {
                _form.listDatabases.Invoke(new MethodInvoker(delegate {
                    _form.listDatabases.Items.Clear();
                    dbs.ForEach(d => _form.listDatabases.Items.Add(d)); }));
            }
        }

        private void ShowTableInfo(IEventArgs args)
        {
            TableInfo item;
            if (_client.Info.TableInfos.TryGetValue(_currentSelectedTableName, out item))
            {
                if (_form.listColumns.InvokeRequired)
                {
                    _form.listColumns.Invoke(new MethodInvoker(delegate 
                    {
                        _form.listColumns.Items.Clear();
                        item.Columns.ForEach(i => _form.listColumns.Items.Add(i.Item1));
                    }));
                }
            }
        }

        private void ShowDbInfo(IEventArgs args)
        {
            string currentDb = string.Empty;

            if (_form.listDatabases.InvokeRequired)
            {
                _form.listDatabases.Invoke(new MethodInvoker(delegate { 
                    currentDb = _form.listDatabases.SelectedItem.ToString();
                    _currentSelectedDbName = currentDb;
                }));
            }

            if (currentDb != string.Empty)
            {
                DatabaseInfo item;
                if (_client.Info.DatabaseInfos.TryGetValue(currentDb, out item))
                {
                    if (_form.labelDatabaseName.InvokeRequired)
                    {
                        _form.listDatabases.Invoke(new MethodInvoker(delegate { _form.labelDatabaseName.Text = item.Name; }));
                    }

                    if (_form.labelDatabaseId.InvokeRequired)
                    {
                        _form.listDatabases.Invoke(new MethodInvoker(delegate { 
                            _form.labelDatabaseId.Text = item.Id.ToString();
                            _currentDbId = item.Id;
                        }));
                    }

                    if (_form.listTables.InvokeRequired)
                    {
                        _form.listTables.Invoke(new MethodInvoker(delegate {
                            _form.listTables.Items.Clear();
                            item.Tables.ForEach(t => _form.listTables.Items.Add(t.Item2));
                        }));
                    }

                    if (_form.listColumns.InvokeRequired)
                    {
                        _form.listColumns.Invoke(new MethodInvoker(delegate 
                        {
                            _form.listColumns.Items.Clear();
                        }));
                    }
                }
            }
        }

        private void ListenForFormEvents()
        {
            _form.listDatabases.SelectedIndexChanged += ListDatabases_SelectedIndexChanged;
            _form.listTables.SelectedIndexChanged += ListTables_SelectedIndexChanged;
        }

        private void ListTables_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (_form.listTables.SelectedItem != null)
            {
                string currentTable = _form.listTables.SelectedItem.ToString();
                if (currentTable != string.Empty)
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

                if (currentDb != string.Empty)
                {
                    _currentSelectedDbName = currentDb;
                    _client.GetDatabaseInfo(currentDb);
                }
            }
        }
        #endregion



    }
}
