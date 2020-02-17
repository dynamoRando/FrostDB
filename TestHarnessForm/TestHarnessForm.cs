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
        (string, int, int, int) _selectedProcessTuple;
        List<(string, int, int, int, string)> _testItems = new List<(string, int, int, int, string)>();

        public TestHarnessForm()
        {
            InitializeComponent();
        }

        private void SetupProcess(Process process)
        {
            process.LoadDatabases();
            process.StartRemoteServer();
            process.StartConsoleServer();
        }

        private void buttonLaunchTestProcess_Click(object sender, EventArgs e)
        {
            var formConsolePort = textFormConsolePort.Text;

            if (!string.IsNullOrEmpty(formConsolePort))
            {
                var process = new Process
            (this.textProcessAddress.Text, Convert.ToInt32(textDataPort.Text),
            Convert.ToInt32(textConsolePort.Text), textRootDirectory.Text);

                SetupProcess(process);

                (string, int, int, int, string) item;
                item.Item1 = textProcessAddress.Text;
                item.Item2 = Convert.ToInt32(textDataPort.Text);
                item.Item3 = Convert.ToInt32(textConsolePort.Text);
                item.Item4 = Convert.ToInt32(textFormConsolePort.Text);
                item.Item5 = textRootDirectory.Text;

                _testItems.Add(item);

                listRunningProcesses.InvokeIfRequired(() =>
                {
                    listRunningProcesses.Items.Add(process.GetConfiguration().Address
                        + ":" + process.GetConfiguration().DataServerPort.ToString()
                        + ":" + process.Configuration.ConsoleServerPort.ToString()
                        + ":" + formConsolePort
                        );
                });

                _runningProcesses.Add(process);
            }

        }

        private void buttonConnectToProcess_Click(object sender, EventArgs e)
        {
            var formConsolePort = textFormConsolePort.Text;
            if (!string.IsNullOrEmpty(formConsolePort))
            {
                LaunchFrostForm(_selectedProcessTuple.Item1, _selectedProcessTuple.Item3, Convert.ToInt32(formConsolePort));
            }
        }

        private void LaunchFrostForm(string ipAddress, int consolePort, int consoleRemotePort)
        {
            FrostForm.Program.Main(ipAddress, consolePort, consoleRemotePort);
        }

        private void listRunningProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listRunningProcesses.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(listRunningProcesses.SelectedItem.ToString()))
                {
                    _selectedProcess = listRunningProcesses.SelectedItem.ToString();
                    _selectedProcessTuple = GetTuple(_selectedProcess);
                }
            }
        }

        private (string, int, int, int) GetTuple(string tupleString)
        {
            (string, int, int, int) item;
            var items = tupleString.Split(':');
            item.Item1 = items[0];
            item.Item2 = Convert.ToInt32(items[1]);
            item.Item3 = Convert.ToInt32(items[2]);
            item.Item4 = Convert.ToInt32(items[3]);

            return item;
        }

        private TestProcess GetTestProcess((string, int, int, int, string) item)
        {
            var process = new TestProcess();
            process.IPAddress = item.Item1;
            process.DataPort = item.Item2;
            process.ConsolePort = item.Item3;
            process.ConsoleResponsePort = item.Item4;
            process.RootDirectory = item.Item5;

            return process;
        }

        private void buttonSaveTestSetup_Click(object sender, EventArgs e)
        {
            var testSetup = new TestSetup();
            var harness = new TestSetupHarness();

            foreach(var x in _testItems)
            {
                var testItem = GetTestProcess(x);
                testSetup.Items.Add(testItem);
            }

            harness.SaveSetup(textTestSetupLocation.Text, testSetup);
        }

        private (string, int, int, int, string) GetItem(TestProcess process)
        {
            (string, int, int, int, string) item;
            item.Item1 = process.IPAddress;
            item.Item2 = process.DataPort;
            item.Item3 = process.ConsolePort;
            item.Item4 = process.ConsoleResponsePort;
            item.Item5 = process.RootDirectory;

            return item;
        }

        private void buttonLoadTestSetup_Click(object sender, EventArgs e)
        {
            _testItems.Clear();
            _runningProcesses.Clear();

            var harness = new TestSetupHarness();
            var setup = harness.LoadSetup(textLoadTestSetup.Text);

            foreach(var x in setup.Items)
            {
                var process = new Process(x.IPAddress, x.DataPort, x.ConsolePort, x.RootDirectory);
                SetupProcess(process);

                listRunningProcesses.InvokeIfRequired(() =>
                {
                    listRunningProcesses.Items.Add(process.GetConfiguration().Address
                        + ":" + process.GetConfiguration().DataServerPort.ToString()
                        + ":" + process.Configuration.ConsoleServerPort.ToString()
                        + ":" + x.ConsoleResponsePort.ToString()
                        );
                });

                _runningProcesses.Add(process);
                _testItems.Add(GetItem(x));

                LaunchFrostForm(x.IPAddress, x.ConsolePort, x.ConsoleResponsePort);
            }
        }

        private void textFormConsolePort_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
