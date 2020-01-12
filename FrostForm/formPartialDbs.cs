using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FrostForm
{
    public partial class formPartialDbs : Form
    {
        App _app;

        public formPartialDbs(App app)
        {
            InitializeComponent();
            _app = app;
        }

        private void formPartialDbs_Load(object sender, EventArgs e)
        {

        }
    }
}
