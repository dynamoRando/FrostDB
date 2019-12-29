using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FrostForm
{
    public partial class formManageContract : Form
    {
        App _app;
        string _databaseName;

        public formManageContract(App app, string databaseName)
        {
            InitializeComponent();
            _app = app;
            _databaseName = databaseName;
        }

        private void formManageContract_Load(object sender, EventArgs e)
        {
            labelDatabaseName.Text = _databaseName;
        }
    }
}
