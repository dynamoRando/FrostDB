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

        }
    }
}
