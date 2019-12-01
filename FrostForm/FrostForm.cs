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
        public formFrost()
        {
            InitializeComponent();
        }

        private async void buttonConnectRemote_Click(object sender, EventArgs e)
        {
            string ipAddress = textRemoteAddress.Text;
            int portNumber = Convert.ToInt32(textRemotePort.Text);

            App app = new App(this);
            app.SetupClient(ipAddress, portNumber);
            AppReference.Client = app.Client;

            //AppReference.Client.GetDatabases();

            // can do this also to get results
            var task = AppReference.Client.GetDatabasesAsync();
            await task;
            MessageBox.Show(task.Result.Count.ToString());
        }

        private void formFrost_Load(object sender, EventArgs e)
        {
        }
    }
}
