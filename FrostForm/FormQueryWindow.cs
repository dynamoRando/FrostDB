using FrostCommon.ConsoleMessages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrostForm
{
    public partial class FormQueryWindow : Form
    {
        App _app;
        List<string> _databases;
        string _currentSelectedDb;
        string _currentSelectedTable;

        public FormQueryWindow(App app)
        {
            _app = app;
            _databases = new List<string>();
            InitializeComponent();
            LoadDatabases();
        }

        private async Task<List<string>> GetDatabasesAsync()
        {
            var task = _app.Client.GetDatabasesAsync();
            await task;

            return task.Result;
        }

        private async void LoadDatabases()
        {
            comboDatabase.Items.Clear();

            _databases = await GetDatabasesAsync();
            foreach (var db in _databases)
            {
                comboDatabase.Items.Add(db);
            }
        }

        private async void comboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboDatabase.SelectedItem != null)
            {
                var selectedDb = comboDatabase.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(selectedDb))
                {
                    _currentSelectedDb = selectedDb;
                    var result = await _app.Client.GetDatabaseInfoAsync(selectedDb);

                    comboTables.Items.Clear();

                    foreach (var t in result.Tables)
                    {
                        comboTables.Items.Add(t.Item2);
                    }

                    LoadParticipants();
                }
            }
        }

        private async void buttonLoadDatabases_Click(object sender, EventArgs e)
        {
            comboDatabase.Items.Clear();
            LoadDatabases();
        }

        private async void comboTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboTables.SelectedItem != null)
            {
                var selectedTable = comboTables.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(selectedTable))
                {
                    _currentSelectedTable = comboTables.SelectedItem.ToString();
                    LoadColumnInformation();
                }
            }
        }

        private async void LoadColumnInformation()
        {
            DatabaseInfo db;
            TableInfo table;
            if (_app.Client.Info.DatabaseInfos.TryGetValue(_currentSelectedDb, out db))
            {
                if (_app.Client.Info.TableInfos.TryGetValue(_currentSelectedTable, out table))
                {
                    var info = await _app.Client.GetTableInfoAsync(db.Id, table.TableId, _currentSelectedTable);
                    listColumns.Items.Clear();
                    foreach (var c in info.Columns)
                    {
                        listColumns.Items.Add($"{c.Item1} [{c.Item2.ToString()}]");
                    }
                }
            }
        }

        private async void LoadParticipants()
        {
            AcceptedContractInfo item;
            if (_app.Client.Info.AcceptedContractInfos.TryGetValue(_currentSelectedDb, out item))
            {
                listParticipants.Items.Clear();
                foreach(var p in item.AcceptedContracts)
                {
                    listParticipants.Items.Add(p);
                }
            }
        }

        private async void buttonExecute_Click(object sender, EventArgs e)
        {
            var queryText = textQuery.Text;
            if (!string.IsNullOrEmpty(queryText))
            {
                var result = await _app.Client.ExecuteCommandAsync(queryText);
                if (result.IsSuccessful)
                {
                    textResults.Text = $"Message: {result.Message} {Environment.NewLine}  Rows Affected:  {result.NumberOfRowsAffected.ToString()}";

                    if (result.JsonData.Length > 0)
                    {
                        textResults.Text += Environment.NewLine + result.JsonData;
                    }
                }
                else
                {
                    textResults.Text = result.Message;
                }
            }
        }
    }
}
