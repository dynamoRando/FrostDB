using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace FrostForm
{
    public partial class formNewTable : Form
    {
        string _databaseName;
        App _app;

        string _intType = "System.Int32";
        string _floatType = "System.Single";
        string _dateTimeType = "System.DateTime";
        string _stringType = "System.String";

        List<(string, Type)> _columns = new List<(string, Type)>();

        public formNewTable(string databaseName, App app)
        {
            InitializeComponent();
            _databaseName = databaseName;
            _app = app;
        }

        private void formNewTable_Load(object sender, EventArgs e)
        {
            labelDatabaseName.Text = _databaseName;
            _columns.Clear();
            AddDataTypes();
        }

        private void comboDataType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonAddColumn_Click(object sender, EventArgs e)
        {
            var columnName = textColumnName.Text;
            var columnType = comboDataType.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(columnType))
            {
                _columns.Add((columnName, Type.GetType(columnType)));
                listColumns.Items.Add(columnName);
            }
        }

        private void AddDataTypes()
        {
            comboDataType.Items.Add(_intType);
            comboDataType.Items.Add(_floatType);
            comboDataType.Items.Add(_dateTimeType);
            comboDataType.Items.Add(_stringType);
        }

        private void listColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listColumns.SelectedItem != null)
            {
                var selectedItem = listColumns.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(selectedItem))
                {
                    var item = _columns.Where(c => c.Item1 == selectedItem).First();
                    labelColumnDataType.Text = item.Item2.ToString();
                    labelColumnName.Text = item.Item1;
                }
            }
        }

        private void buttonCreateTable_Click(object sender, EventArgs e)
        {

        }

        private void buttonRemoveColumn_Click(object sender, EventArgs e)
        {
            (string, Type) item;

            var selectedItem = listColumns.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(selectedItem))
            {
                item = _columns.Where(c => c.Item1 == selectedItem).First();
                _columns.Remove(item);
                listColumns.Items.Remove(selectedItem);
            }
        }
    }
}
