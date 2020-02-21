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
using System.Threading;

namespace FrostForm
{
    public partial class formManagePendingContract : Form
    {
        App _app;
        ContractInfo _currentContract;
        List<ContractInfo> _pendingContracts;

        public formManagePendingContract(App app)
        {
            InitializeComponent();
            _app = app;
            _pendingContracts = new List<ContractInfo>();
        }

        private void formManagePendingContract_Load(object sender, EventArgs e)
        {
            //_app.Client.EventManager.StartListening(ClientEvents.GetProcessPendingContractInfo, AddPendingProcessContractInfo);
        }

        private async void buttonLoadPendingContracts_Click(object sender, EventArgs e)
        {
            //_app.GetPendingContractInformation();

            this.listPendingContracts.InvokeIfRequired(() =>
            {
                this.listPendingContracts.Items.Clear();
            });

            _pendingContracts = await _app.GetPendingContractInformationAsync();

            this.listPendingContracts.InvokeIfRequired(() =>
            {
                _pendingContracts.ForEach(i =>
                {
                    this.listPendingContracts.Items.Add(i.DatabaseName);
                });
            });
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
                    _currentContract = _pendingContracts.Where(p => p.DatabaseName == dbName).DefaultIfEmpty(new ContractInfo()).First();

                    textDatabaseName.Text = _currentContract.DatabaseName;
                    textDatabaseIpAddress.Text = _currentContract.Location.IpAddress;
                    textDatabasePortNumber.Text = _currentContract.Location.PortNumber.ToString();
                    textDatabaseDescription.Text = _currentContract.ContractDescription;
                }
            }
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
