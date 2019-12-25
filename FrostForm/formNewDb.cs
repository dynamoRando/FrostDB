using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FrostForm
{
    public partial class formNewDb : Form
    {
        App _app;

        public formNewDb(App app)
        {
            InitializeComponent();
            _app = app;
        }

        private void buttonAddDb_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textboxDbName.Text))
            {
                _app.AddNewDb(textboxDbName.Text);
            }
        }
    }
}
