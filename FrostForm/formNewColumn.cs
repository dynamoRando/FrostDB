using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FrostForm
{
    public partial class formNewColumn : Form
    {
        string _databaseName;
        string _tableName;
        App _app;

        string _intType = "System.Int32";
        string _floatType = "System.Single";
        string _dateTimeType = "System.DateTime";
        string _stringType = "System.String";

        public formNewColumn(string databaseName, string tableName, App app)
        {
            InitializeComponent();
            _databaseName = databaseName;
            _tableName = tableName;
            _app = app;
        }

        private void formNewColumn_Load(object sender, EventArgs e)
        {
            comboDataType.Items.Clear();
            comboDataType.Items.Add(_intType);
            comboDataType.Items.Add(_floatType);
            comboDataType.Items.Add(_dateTimeType);
            comboDataType.Items.Add(_stringType);

            labelDatabaseName.Text = _databaseName;
            labelTableName.Text = _tableName;

        }

        private void buttonAddColumn_Click(object sender, EventArgs e)
        {
            var columnName = textColumnName.Text;

            if (comboDataType.SelectedItem != null)
            {
                var dataType = comboDataType.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(dataType) && !string.IsNullOrEmpty(columnName))
                {
                    _app.AddColumnToTable(_databaseName, _tableName, columnName, dataType);
                    Close();
                }
            }

        }
    }
}
