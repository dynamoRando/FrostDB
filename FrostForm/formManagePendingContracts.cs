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
    }
}
