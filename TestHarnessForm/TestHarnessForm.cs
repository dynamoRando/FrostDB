using FrostDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrostForm;

namespace TestHarnessForm
{
    public partial class TestHarnessForm : Form
    {
        List<Process> _runningProcesses = new List<Process>();
        string _selectedProcess;
        (string, int) _selectedProcessTuple;

        public TestHarnessForm()
        {
            InitializeComponent();
        }

        private void buttonLaunchTestProcess_Click(object sender, EventArgs e)
        {
            var process = new Process
                (this.textProcessAddress.Text, Convert.ToInt32(textDataPort.Text),
                Convert.ToInt32(textConsolePort.Text), textRootDirectory.Text);

            process.LoadDatabases();
            process.StartRemoteServer();
            process.StartConsoleServer();

            listRunningProcesses.InvokeIfRequired(() => {
                listRunningProcesses.Items.Add(process.GetConfiguration().Address + ":" + process.GetConfiguration().DataServerPort.ToString());
            });

            _runningProcesses.Add(process);
        }

        private void buttonConnectToProcess_Click(object sender, EventArgs e)
        {
            var formConsolePort = textFormConsolePort.Text;
            if (!string.IsNullOrEmpty(formConsolePort))
            {
                FrostForm.Program.Main(_selectedProcessTuple.Item1, _selectedProcessTuple.Item2, Convert.ToInt32(formConsolePort));
            }
        }

        private void listRunningProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listRunningProcesses.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(listRunningProcesses.SelectedItem.ToString()))
                {
                    _selectedProcess = listRunningProcesses.SelectedItem.ToString();
                    var items = _selectedProcess.Split(':');
                    _selectedProcessTuple.Item1 = items[0];
                    _selectedProcessTuple.Item2 = Convert.ToInt32(items[1]);
                }
            }
        }
    }
}
