using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FrostForm
{
    public partial class formAddParticipant : Form
    {
        App _app;
        string _databaseName;

        public formAddParticipant(App app, string databaseName)
        {
            InitializeComponent();
            _app = app;
            _databaseName = databaseName;
        }

        private void formAddParticipant_Load(object sender, EventArgs e)
        {
            labelDatabaseName.Text = _databaseName;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddParticipant_Click(object sender, EventArgs e)
        {
            var ipAddress = textboxIPAddress.Text;
            var portNumber = textboxPortNumber.Text;
            if (!string.IsNullOrEmpty(ipAddress) && !string.IsNullOrEmpty(portNumber))
            {
                _app.AddParticipantToDb(ipAddress, portNumber, _databaseName);
            }
        }
    }
}
