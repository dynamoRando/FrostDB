using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FrostCommon.ConsoleMessages;
using FrostDbClient;
using FrostForm.Extensions;
using System.Linq;

namespace FrostForm
{
    public partial class formManagePendingContract : Form
    {
        App _app;
        ContractInfo _currentContract;

        public formManagePendingContract(App app)
        {
            InitializeComponent();
            _app = app;
        }

        private void formManagePendingContract_Load(object sender, EventArgs e)
        {
            _app.Client.EventManager.StartListening(ClientEvents.GetProcessPendingContractInfo, AddPendingProcessContractInfo);
        }

        private void buttonLoadPendingContracts_Click(object sender, EventArgs e)
        {
            _app.GetPendingContractInformation();
        }

        private void AddPendingProcessContractInfo(IEventArgs args)
        {
            List<ContractInfo> item;
            if (_app.Client.Info.ProcessPendingContracts.TryGetValue(string.Empty, out item))
            {
                this.listPendingContracts.InvokeIfRequired(() =>
                {
                    item.ForEach(i =>
                    {
                        this.listPendingContracts.Items.Add(i.DatabaseName);
                    });
                });
            }
        }

        private void listPendingContracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPendingContracts.SelectedItem != null)
            {
                var dbName = listPendingContracts.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(dbName))
                {
                    var item = GetContractInfoForDb(dbName);
                    _currentContract = item;

                    textDatabaseName.Text = item.DatabaseName;
                    textDatabaseIpAddress.Text = item.Location.IpAddress;
                    textDatabasePortNumber.Text = item.Location.PortNumber.ToString();
                    textDatabaseDescription.Text = item.ContractDescription;
                }
            }
        }

        private ContractInfo GetContractInfoForDb(string dbName)
        {
            List<ContractInfo> item;
            ContractInfo info = null;

            if (_app.Client.Info.ProcessPendingContracts.TryGetValue(string.Empty, out item))
            {
                info = item.Where(i => i.DatabaseName == dbName).FirstOrDefault();
            }

            return info;
        }

        private void buttonAcceptContract_Click(object sender, EventArgs e)
        {
            if (_currentContract != null)
            {
                _app.AcceptContract(_currentContract);
            }
        }

        private void buttonDeclineContract_Click(object sender, EventArgs e)
        {
            if (_currentContract != null)
            {
                _app.RejectContract(_currentContract);
            }
        }
    }
}
