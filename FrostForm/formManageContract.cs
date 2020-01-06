using System;
using System.Linq;
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
        const string _PARTICIPANT = "Participant";
        const string _PROCESS = "Process";
        ContractInfo _contractInfo;

        public formManageContract(App app, string databaseName)
        {
            InitializeComponent();
            _app = app;
            _databaseName = databaseName;
        }

        private async void formManageContract_Load(object sender, EventArgs e)
        {
            labelDatabaseName.Text = _databaseName;

            _contractInfo = await _app.GetContractInformationAsync(_databaseName);

            if (_contractInfo is null)
            {
                _contractInfo = new ContractInfo();
            }

            PopulateContractInfo(_contractInfo);
        }

        private void buttonSaveContract_Click(object sender, EventArgs e)
        {
            var contractDescription = textboxDescription.Text;
            _app.UpdateContractInformation(_databaseName, contractDescription, _contractInfo.SchemaData);
        }

        private void PopulateContractInfo(ContractInfo contractInfo)
        {
            textboxDescription.InvokeIfRequired(() =>
            {
                textboxDescription.Text = contractInfo.ContractDescription;
            });

            textboxSchema.InvokeIfRequired(() =>
            {
                foreach (var t in contractInfo.TableNames)
                {
                    textboxSchema.Text += t + ", ";
                }
            });

            listboxAuthorTables.InvokeIfRequired(() =>
            {
                foreach (var t in contractInfo.TableNames)
                {
                    listboxAuthorTables.Items.Add(t);
                }
            });

            listboxParticipantTables.InvokeIfRequired(() =>
            {
                foreach (var t in contractInfo.TableNames)
                {
                    listboxParticipantTables.Items.Add(t);
                }
            });

            textboxSchema.Text = textboxSchema.Text.TrimEnd();
            textboxSchema.Text = textboxSchema.Text.TrimEnd(',');
        }

        private void buttonSaveParticipantRights_Click(object sender, EventArgs e)
        {
            if (listboxParticipantTables.SelectedItem != null)
            {
                var selectedTable = listboxParticipantTables.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(selectedTable))
                {
                    (string, string, List<string>) permission;
                    permission.Item1 = selectedTable;
                    permission.Item2 = _PARTICIPANT;
                    permission.Item3 = new List<string>();

                    if (checkParticipantRead.Checked)
                    {
                        permission.Item3.Add("Read");
                    }

                    if (checkParticipantWrite.Checked)
                    {
                        permission.Item3.Add("Insert");
                    }

                    if (checkParticipantModify.Checked)
                    {
                        permission.Item3.Add("Update");
                    }

                    if (checkParticipantDelete.Checked)
                    {
                        permission.Item3.Add("Delete");
                    }

                    if (ContainsTablePermission(selectedTable, _PARTICIPANT))
                    {
                        RemovePermission(selectedTable, _PARTICIPANT);
                    }

                    _contractInfo.SchemaData.Add(permission);
                }
            }
        }

        private (string, string, List<string>) GetPermission(string tableName, string cooperator)
        {
            var item = _contractInfo.SchemaData.Where(p => p.Item1 == tableName && p.Item2 == cooperator).FirstOrDefault();
            if (item.Item1 == null && item.Item2 == null && item.Item3 == null)
            {
                item.Item1 = string.Empty;
                item.Item2 = string.Empty;
                item.Item3 = new List<string>();
            }

            if (item.Item3 == null)
            {
                item.Item3 = new List<string>();
            }

            return item;
        }


        private bool ContainsTablePermission(string tableName, string cooperator)
        {
            return _contractInfo.SchemaData.Any(p => p.Item1 == tableName && p.Item2 == cooperator);
        }

        private void RemovePermission(string tableName, string cooperator)
        {
            var item = _contractInfo.SchemaData.Where(p => p.Item1 == tableName && p.Item2 == cooperator).FirstOrDefault();
            _contractInfo.SchemaData.Remove(item);
        }

        private void listboxParticipantTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listboxParticipantTables.SelectedItem != null)
            {
                var selectedTable = listboxParticipantTables.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(selectedTable))
                {

                    ClearCheckboxParticipant();

                    var permission = GetPermission(selectedTable, _PARTICIPANT);

                    if (permission.Item3.Contains("Read"))
                    {
                        checkParticipantRead.Checked = true;
                    }

                    if (permission.Item3.Contains("Insert"))
                    {
                        checkParticipantWrite.Checked = true;
                    }

                    if (permission.Item3.Contains("Update"))
                    {
                        checkParticipantModify.Checked = true;
                    }

                    if (permission.Item3.Contains("Delete"))
                    {
                        checkParticipantDelete.Checked = true;
                    }
                }
            }
        }

        private void buttonSaveAuthorRights_Click(object sender, EventArgs e)
        {
            if (listboxAuthorTables.SelectedItem != null)
            {
                var selectedTable = listboxAuthorTables.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(selectedTable))
                {
                    (string, string, List<string>) permission;
                    permission.Item1 = selectedTable;
                    permission.Item2 = _PROCESS;
                    permission.Item3 = new List<string>();

                    if (checkAuthorRead.Checked)
                    {
                        permission.Item3.Add("Read");
                    }

                    if (checkAuthorWrite.Checked)
                    {
                        permission.Item3.Add("Insert");
                    }

                    if (checkAuthorModify.Checked)
                    {
                        permission.Item3.Add("Update");
                    }

                    if (checkAuthorDelete.Checked)
                    {
                        permission.Item3.Add("Delete");
                    }

                    if (ContainsTablePermission(selectedTable, _PROCESS))
                    {
                        RemovePermission(selectedTable, _PROCESS);
                    }

                    _contractInfo.SchemaData.Add(permission);
                }
            }
        }

        private void listboxAuthorTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listboxAuthorTables.SelectedItem != null)
            {
                var selectedTable = listboxAuthorTables.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(selectedTable))
                {
                    ClearCheckboxAuthor();

                    var permission = GetPermission(selectedTable, _PROCESS);

                    if (permission.Item3.Contains("Read"))
                    {
                        checkAuthorRead.Checked = true;
                    }

                    if (permission.Item3.Contains("Insert"))
                    {
                        checkAuthorWrite.Checked = true;
                    }

                    if (permission.Item3.Contains("Update"))
                    {
                        checkAuthorModify.Checked = true;
                    }

                    if (permission.Item3.Contains("Delete"))
                    {
                        checkAuthorDelete.Checked = true;
                    }
                }
            }
        }

        private void ClearCheckboxParticipant()
        {
            checkParticipantRead.Checked = false;
            checkParticipantWrite.Checked = false;
            checkParticipantModify.Checked = false;
            checkParticipantDelete.Checked = false;
        }

        private void ClearCheckboxAuthor()
        {
            checkAuthorRead.Checked = false;
            checkAuthorWrite.Checked = false;
            checkAuthorModify.Checked = false;
            checkAuthorDelete.Checked = false;
        }
    }
}
