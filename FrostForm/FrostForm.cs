using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrostForm
{
    public partial class formFrost : Form
    {
        App _app;
        public formFrost()
        {
            InitializeComponent();
        }

        private async void buttonConnectRemote_Click(object sender, EventArgs e)
        {
            string ipAddress = textRemoteAddress.Text;
            int portNumber = Convert.ToInt32(textRemotePort.Text);

            _app = new App(this);
            _app.SetupClient(ipAddress, portNumber);
            AppReference.Client = _app.Client;

            //AppReference.Client.GetDatabases();

            // can do this also to get results
            var task = AppReference.Client.GetDatabasesAsync();
            await task;
            MessageBox.Show(task.Result.Count.ToString());
        }

        private void formFrost_Load(object sender, EventArgs e)
        {
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
    }
}
