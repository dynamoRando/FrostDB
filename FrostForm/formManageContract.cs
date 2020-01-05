using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FrostCommon.ConsoleMessages;
using FrostForm.Extensions;

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

        private async void formManageContract_Load(object sender, EventArgs e)
        {
            labelDatabaseName.Text = _databaseName;
            var contractInfo = await _app.GetContractInformationAsync(_databaseName);
            PopulateContractInfo(contractInfo);
        }

        private void buttonSaveContract_Click(object sender, EventArgs e)
        {
            var contractDescription = textboxDescription.Text;
            var schemaData = new List<(string, List<(string, List<string>)>)>();

            _app.UpdateContractInformation(contractDescription, schemaData);
        }

        private void PopulateContractInfo(ContractInfo contractInfo)
        {
            textboxDescription.InvokeIfRequired(() =>
            {
                textboxDescription.Text = contractInfo.ContractDescription;
            });

            textboxSchema.InvokeIfRequired(() => 
            {
                foreach(var t in contractInfo.TableNames)
                {
                    textboxSchema.Text += t + ", ";
                }
            });

            textboxSchema.Text = textboxSchema.Text.TrimEnd();
            textboxSchema.Text = textboxSchema.Text.TrimEnd(',');
        }
    }
}
