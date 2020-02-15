using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using FrostForm.Extensions;

namespace FrostForm
{
    public static class TaskExtensions
    {
        public static async Task TimeoutAfter(this Task task, int millisecondsTimeout)
        {
            if (task == await Task.WhenAny(task, Task.Delay(millisecondsTimeout)))
                await task;
            else
                throw new TimeoutException();
        }
    }

    public partial class formFrost : Form
    {
        App _app;
        string _paramIpAddress;
        int _paramPortNumber;
        int _paramLocalPortNumber;
        bool _hasArgs = false;

        public formFrost()
        {
            InitializeComponent();
        }

        public formFrost(string[] args)
        {
            InitializeComponent();
            if (args.Count() >= 3)
            {
                _hasArgs = true;
                _paramIpAddress = args[0];
                _paramPortNumber = Convert.ToInt32(args[1]);
                _paramLocalPortNumber = Convert.ToInt32(args[2]);
            }
        }

        public formFrost(string ipAddress, int dataPort, int consolePort)
        {
            InitializeComponent();
            _paramIpAddress = ipAddress;
            _paramPortNumber = dataPort;
            _paramLocalPortNumber = consolePort;

            _hasArgs = true;
        }

        private async void buttonConnectRemote_Click(object sender, EventArgs e)
        {
            int timeout = 1000;

            var selectedIp = comboRemoteAddress.SelectedItem.ToString();
            string ipAddress = selectedIp;
            int portNumber = Convert.ToInt32(textRemotePort.Text);
            int localPort = Convert.ToInt32(textLocalPort.Text)
;
            _app = new App(this);
            _app.SetupClient(ipAddress, portNumber, localPort);
            AppReference.Client = _app.Client;

            //AppReference.Client.GetDatabases();

            // can do this also to get results
            var task = AppReference.Client.GetDatabasesAsync();
            await task;
            //MessageBox.Show(task.Result.Count.ToString());
            Console.WriteLine(task.Result.Count.ToString());
        }

        private void formFrost_Load(object sender, EventArgs e)
        {
            GetIPAddresses();
            if (_hasArgs)
            {
                textRemotePort.Text = _paramPortNumber.ToString();
                textLocalPort.Text = _paramLocalPortNumber.ToString();
            }
        }

        private void listTables_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonAddDb_Click(object sender, EventArgs e)
        {
            var form = new formNewDb(_app);
            form.Show();
        }

        private void buttonRemoveDb_Click(object sender, EventArgs e)
        {
            var selectedDb = listDatabases.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(selectedDb))
            {
                var result = MessageBox.Show("Are you sure?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    _app.RemoveDb(selectedDb);
                }
            }
        }

        private void buttonAddTable_Click(object sender, EventArgs e)
        {
            var selectedDb = listDatabases.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(selectedDb))
            {
                var form = new formNewTable(selectedDb, _app);
                form.Show();
            }
        }

        private void buttonRemoveTable_Click(object sender, EventArgs e)
        {
            if (listTables.SelectedItem != null && listDatabases.SelectedItem != null)
            {
                var tableName = listTables.SelectedItem.ToString();
                var databaseName = listDatabases.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(databaseName))
                {
                    _app.RemoveTableFromDb(databaseName, tableName);
                }
            }
        }

        private void buttonAddColumn_Click(object sender, EventArgs e)
        {
            if (listDatabases.SelectedItem != null)
            {
                if (listTables.SelectedItem != null)
                {
                    var database = listDatabases.SelectedItem.ToString();
                    var table = listTables.SelectedItem.ToString();
                    if (!string.IsNullOrEmpty(database) && !string.IsNullOrEmpty(table))
                    {
                        var form = new formNewColumn(database, table, _app);
                        form.Show();
                    }
                }
            }
        }

        private void buttonRemoveColumn_Click(object sender, EventArgs e)
        {
            if (IsDbSelected())
            {
                if (listTables.SelectedItem != null)
                {
                    if (listColumns.SelectedItem != null)
                    {
                        var selectedDb = GetSelectedDb();
                        var selectedTable = listTables.SelectedItem.ToString();
                        var selectedColumn = listColumns.SelectedItem.ToString();

                        if (!string.IsNullOrEmpty(selectedDb) && !string.IsNullOrEmpty(selectedTable) && !string.IsNullOrEmpty(selectedColumn))
                        {
                            var result = MessageBox.Show("Are you sure?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.Yes)
                            {
                                _app.RemoveColumnFromTable(selectedDb, selectedTable, selectedColumn);
                            }
                        }
                    }
                }
            }

        }

        private void buttonManageContract_Click(object sender, EventArgs e)
        {
            if (IsDbSelected())
            {
                var db = GetSelectedDb();
                var form = new formManageContract(_app, db);
                form.Show();
            }
        }

        private bool IsDbSelected()
        {
            if (listDatabases.SelectedItem != null)
            {
                var selectedDb = listDatabases.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(selectedDb))
                {
                    return true;
                }
            }

            return false;
        }

        private string GetSelectedDb()
        {
            return listDatabases.SelectedItem.ToString();
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            var form = new FormQueryWindow(_app);
            form.Show();
        }

        private void buttonAddParticipant_Click(object sender, EventArgs e)
        {
            if (IsDbSelected())
            {
                var form = new formAddParticipant(_app, GetSelectedDb());
                form.Show();
            }
        }

        private void buttonManagePartialDbs_Click(object sender, EventArgs e)
        {
            var form = new formPartialDbs(_app);
            form.Show();
        }

        private void buttonMyPendingContracts_Click(object sender, EventArgs e)
        {
            var form = new formManagePendingContract(_app);
            form.Show();
        }

        private void GetIPAddresses()
        {
            comboRemoteAddress.InvokeIfRequired(() =>
            {
                comboRemoteAddress.Items.Clear();

                if (_hasArgs)
                {
                    comboRemoteAddress.Items.Add(_paramIpAddress);
                }

                var addresses = Dns.GetHostByName(Dns.GetHostName()).AddressList.ToList();
                addresses.ForEach(a => comboRemoteAddress.Items.Add(a));

                if (!comboRemoteAddress.Items.Contains("127.0.0.1"))
                {
                    comboRemoteAddress.Items.Add("127.0.0.1");
                }
            });
        }
    }
}
