using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FrostForm
{
    public partial class formManagePendingContract : Form
    {
        App _app;
        
        public formManagePendingContract(App app)
        {
            InitializeComponent();
            _app = app;
        }

    }
}
