﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FrostForm
{
    public partial class FormQueryWindow : Form
    {
        App _app;
        public FormQueryWindow(App app)
        {
            _app = app;
            InitializeComponent();
        }
    }
}
