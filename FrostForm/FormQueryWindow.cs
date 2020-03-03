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
            foreach(var db in _databases)
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
                    var result = await _app.Client.GetDatabaseInfoAsync(selectedDb);

                    comboTables.Items.Clear();

                    foreach(var t in result.Tables)
                    {
                        comboTables.Items.Add(t.Item2);
                    }
                }
            }
        }
    }
}
