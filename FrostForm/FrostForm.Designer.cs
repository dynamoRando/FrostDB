namespace FrostForm
{
    partial class formFrost
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelRemoteAddress = new System.Windows.Forms.Label();
            this.textRemoteAddress = new System.Windows.Forms.TextBox();
            this.labelRemotePort = new System.Windows.Forms.Label();
            this.textRemotePort = new System.Windows.Forms.TextBox();
            this.buttonConnectRemote = new System.Windows.Forms.Button();
            this.listDatabases = new System.Windows.Forms.ListBox();
            this.labelDatabaseName = new System.Windows.Forms.Label();
            this.labelDatabaseId = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonManageContract = new System.Windows.Forms.Button();
            this.labelColumnDataType = new System.Windows.Forms.Label();
            this.buttonQuery = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelColumnName = new System.Windows.Forms.Label();
            this.buttonAddParticipant = new System.Windows.Forms.Button();
            this.listPendingParticipants = new System.Windows.Forms.ListBox();
            this.listAcceptedParticipants = new System.Windows.Forms.ListBox();
            this.buttonRemoveColumn = new System.Windows.Forms.Button();
            this.buttonRemoveTable = new System.Windows.Forms.Button();
            this.buttonRemoveDb = new System.Windows.Forms.Button();
            this.buttonAddColumn = new System.Windows.Forms.Button();
            this.buttonAddDb = new System.Windows.Forms.Button();
            this.buttonAddTable = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.listColumns = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listTables = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelRemoteAddress
            // 
            this.labelRemoteAddress.AutoSize = true;
            this.labelRemoteAddress.Location = new System.Drawing.Point(12, 19);
            this.labelRemoteAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRemoteAddress.Name = "labelRemoteAddress";
            this.labelRemoteAddress.Size = new System.Drawing.Size(106, 15);
            this.labelRemoteAddress.TabIndex = 0;
            this.labelRemoteAddress.Text = "Remote IP Address";
            // 
            // textRemoteAddress
            // 
            this.textRemoteAddress.Location = new System.Drawing.Point(12, 36);
            this.textRemoteAddress.Margin = new System.Windows.Forms.Padding(2);
            this.textRemoteAddress.Name = "textRemoteAddress";
            this.textRemoteAddress.Size = new System.Drawing.Size(207, 23);
            this.textRemoteAddress.TabIndex = 1;
            this.textRemoteAddress.Text = "127.0.0.1";
            // 
            // labelRemotePort
            // 
            this.labelRemotePort.AutoSize = true;
            this.labelRemotePort.Location = new System.Drawing.Point(232, 19);
            this.labelRemotePort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRemotePort.Name = "labelRemotePort";
            this.labelRemotePort.Size = new System.Drawing.Size(70, 15);
            this.labelRemotePort.TabIndex = 2;
            this.labelRemotePort.Text = "RemotePort";
            // 
            // textRemotePort
            // 
            this.textRemotePort.Location = new System.Drawing.Point(232, 36);
            this.textRemotePort.Margin = new System.Windows.Forms.Padding(2);
            this.textRemotePort.Name = "textRemotePort";
            this.textRemotePort.Size = new System.Drawing.Size(59, 23);
            this.textRemotePort.TabIndex = 3;
            this.textRemotePort.Text = "519";
            // 
            // buttonConnectRemote
            // 
            this.buttonConnectRemote.Location = new System.Drawing.Point(311, 36);
            this.buttonConnectRemote.Margin = new System.Windows.Forms.Padding(2);
            this.buttonConnectRemote.Name = "buttonConnectRemote";
            this.buttonConnectRemote.Size = new System.Drawing.Size(69, 25);
            this.buttonConnectRemote.TabIndex = 4;
            this.buttonConnectRemote.Text = "Connect";
            this.buttonConnectRemote.UseVisualStyleBackColor = true;
            this.buttonConnectRemote.Click += new System.EventHandler(this.buttonConnectRemote_Click);
            // 
            // listDatabases
            // 
            this.listDatabases.FormattingEnabled = true;
            this.listDatabases.ItemHeight = 15;
            this.listDatabases.Location = new System.Drawing.Point(5, 96);
            this.listDatabases.Margin = new System.Windows.Forms.Padding(2);
            this.listDatabases.Name = "listDatabases";
            this.listDatabases.Size = new System.Drawing.Size(82, 154);
            this.listDatabases.TabIndex = 5;
            // 
            // labelDatabaseName
            // 
            this.labelDatabaseName.AutoSize = true;
            this.labelDatabaseName.Location = new System.Drawing.Point(6, 28);
            this.labelDatabaseName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDatabaseName.Name = "labelDatabaseName";
            this.labelDatabaseName.Size = new System.Drawing.Size(38, 15);
            this.labelDatabaseName.TabIndex = 6;
            this.labelDatabaseName.Text = "label1";
            // 
            // labelDatabaseId
            // 
            this.labelDatabaseId.AutoSize = true;
            this.labelDatabaseId.Location = new System.Drawing.Point(6, 43);
            this.labelDatabaseId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDatabaseId.Name = "labelDatabaseId";
            this.labelDatabaseId.Size = new System.Drawing.Size(38, 15);
            this.labelDatabaseId.TabIndex = 7;
            this.labelDatabaseId.Text = "label2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonConnectRemote);
            this.groupBox1.Controls.Add(this.textRemotePort);
            this.groupBox1.Controls.Add(this.textRemoteAddress);
            this.groupBox1.Controls.Add(this.labelRemotePort);
            this.groupBox1.Controls.Add(this.labelRemoteAddress);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 71);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instance";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonManageContract);
            this.groupBox2.Controls.Add(this.labelColumnDataType);
            this.groupBox2.Controls.Add(this.buttonQuery);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.labelColumnName);
            this.groupBox2.Controls.Add(this.buttonAddParticipant);
            this.groupBox2.Controls.Add(this.listPendingParticipants);
            this.groupBox2.Controls.Add(this.listAcceptedParticipants);
            this.groupBox2.Controls.Add(this.buttonRemoveColumn);
            this.groupBox2.Controls.Add(this.buttonRemoveTable);
            this.groupBox2.Controls.Add(this.buttonRemoveDb);
            this.groupBox2.Controls.Add(this.buttonAddColumn);
            this.groupBox2.Controls.Add(this.buttonAddDb);
            this.groupBox2.Controls.Add(this.buttonAddTable);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.listColumns);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.labelDatabaseName);
            this.groupBox2.Controls.Add(this.labelDatabaseId);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.listTables);
            this.groupBox2.Controls.Add(this.listDatabases);
            this.groupBox2.Location = new System.Drawing.Point(12, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 332);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Databases";
            // 
            // buttonManageContract
            // 
            this.buttonManageContract.Location = new System.Drawing.Point(642, 14);
            this.buttonManageContract.Name = "buttonManageContract";
            this.buttonManageContract.Size = new System.Drawing.Size(107, 23);
            this.buttonManageContract.TabIndex = 22;
            this.buttonManageContract.Text = "Manage Contract";
            this.buttonManageContract.UseVisualStyleBackColor = true;
            // 
            // labelColumnDataType
            // 
            this.labelColumnDataType.AutoSize = true;
            this.labelColumnDataType.Location = new System.Drawing.Point(311, 106);
            this.labelColumnDataType.Name = "labelColumnDataType";
            this.labelColumnDataType.Size = new System.Drawing.Size(124, 15);
            this.labelColumnDataType.TabIndex = 15;
            this.labelColumnDataType.Text = "labelColumnDataType";
            // 
            // buttonQuery
            // 
            this.buttonQuery.Location = new System.Drawing.Point(642, 43);
            this.buttonQuery.Name = "buttonQuery";
            this.buttonQuery.Size = new System.Drawing.Size(107, 23);
            this.buttonQuery.TabIndex = 27;
            this.buttonQuery.Text = "Query Window";
            this.buttonQuery.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(441, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 23;
            this.label2.Text = "Participants:";
            // 
            // labelColumnName
            // 
            this.labelColumnName.AutoSize = true;
            this.labelColumnName.Location = new System.Drawing.Point(311, 91);
            this.labelColumnName.Name = "labelColumnName";
            this.labelColumnName.Size = new System.Drawing.Size(107, 15);
            this.labelColumnName.TabIndex = 14;
            this.labelColumnName.Text = "labelColumnName";
            // 
            // buttonAddParticipant
            // 
            this.buttonAddParticipant.Location = new System.Drawing.Point(441, 261);
            this.buttonAddParticipant.Name = "buttonAddParticipant";
            this.buttonAddParticipant.Size = new System.Drawing.Size(139, 23);
            this.buttonAddParticipant.TabIndex = 25;
            this.buttonAddParticipant.Text = "+ Participant";
            this.buttonAddParticipant.UseVisualStyleBackColor = true;
            // 
            // listPendingParticipants
            // 
            this.listPendingParticipants.FormattingEnabled = true;
            this.listPendingParticipants.ItemHeight = 15;
            this.listPendingParticipants.Location = new System.Drawing.Point(601, 91);
            this.listPendingParticipants.Name = "listPendingParticipants";
            this.listPendingParticipants.Size = new System.Drawing.Size(148, 154);
            this.listPendingParticipants.TabIndex = 26;
            // 
            // listAcceptedParticipants
            // 
            this.listAcceptedParticipants.FormattingEnabled = true;
            this.listAcceptedParticipants.ItemHeight = 15;
            this.listAcceptedParticipants.Location = new System.Drawing.Point(441, 91);
            this.listAcceptedParticipants.Name = "listAcceptedParticipants";
            this.listAcceptedParticipants.Size = new System.Drawing.Size(139, 154);
            this.listAcceptedParticipants.TabIndex = 24;
            // 
            // buttonRemoveColumn
            // 
            this.buttonRemoveColumn.Location = new System.Drawing.Point(208, 293);
            this.buttonRemoveColumn.Name = "buttonRemoveColumn";
            this.buttonRemoveColumn.Size = new System.Drawing.Size(83, 23);
            this.buttonRemoveColumn.TabIndex = 21;
            this.buttonRemoveColumn.Text = "- Column";
            this.buttonRemoveColumn.UseVisualStyleBackColor = true;
            // 
            // buttonRemoveTable
            // 
            this.buttonRemoveTable.Location = new System.Drawing.Point(105, 293);
            this.buttonRemoveTable.Name = "buttonRemoveTable";
            this.buttonRemoveTable.Size = new System.Drawing.Size(87, 23);
            this.buttonRemoveTable.TabIndex = 20;
            this.buttonRemoveTable.Text = "- Table";
            this.buttonRemoveTable.UseVisualStyleBackColor = true;
            // 
            // buttonRemoveDb
            // 
            this.buttonRemoveDb.Location = new System.Drawing.Point(5, 293);
            this.buttonRemoveDb.Name = "buttonRemoveDb";
            this.buttonRemoveDb.Size = new System.Drawing.Size(82, 23);
            this.buttonRemoveDb.TabIndex = 19;
            this.buttonRemoveDb.Text = "- DB";
            this.buttonRemoveDb.UseVisualStyleBackColor = true;
            // 
            // buttonAddColumn
            // 
            this.buttonAddColumn.Location = new System.Drawing.Point(208, 264);
            this.buttonAddColumn.Name = "buttonAddColumn";
            this.buttonAddColumn.Size = new System.Drawing.Size(83, 23);
            this.buttonAddColumn.TabIndex = 18;
            this.buttonAddColumn.Text = "+ Column";
            this.buttonAddColumn.UseVisualStyleBackColor = true;
            // 
            // buttonAddDb
            // 
            this.buttonAddDb.Location = new System.Drawing.Point(5, 264);
            this.buttonAddDb.Name = "buttonAddDb";
            this.buttonAddDb.Size = new System.Drawing.Size(82, 23);
            this.buttonAddDb.TabIndex = 16;
            this.buttonAddDb.Text = "+ DB";
            this.buttonAddDb.UseVisualStyleBackColor = true;
            this.buttonAddDb.Click += new System.EventHandler(this.buttonAddDb_Click);
            // 
            // buttonAddTable
            // 
            this.buttonAddTable.Location = new System.Drawing.Point(105, 264);
            this.buttonAddTable.Name = "buttonAddTable";
            this.buttonAddTable.Size = new System.Drawing.Size(87, 23);
            this.buttonAddTable.TabIndex = 17;
            this.buttonAddTable.Text = "+ Table";
            this.buttonAddTable.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(208, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Columns:";
            // 
            // listColumns
            // 
            this.listColumns.FormattingEnabled = true;
            this.listColumns.ItemHeight = 15;
            this.listColumns.Location = new System.Drawing.Point(208, 94);
            this.listColumns.Name = "listColumns";
            this.listColumns.Size = new System.Drawing.Size(83, 154);
            this.listColumns.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Databases:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Tables:";
            // 
            // listTables
            // 
            this.listTables.FormattingEnabled = true;
            this.listTables.ItemHeight = 15;
            this.listTables.Location = new System.Drawing.Point(105, 94);
            this.listTables.Name = "listTables";
            this.listTables.Size = new System.Drawing.Size(87, 154);
            this.listTables.TabIndex = 8;
            this.listTables.SelectedIndexChanged += new System.EventHandler(this.listTables_SelectedIndexChanged);
            // 
            // formFrost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 421);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "formFrost";
            this.Text = "FrostForm";
            this.Load += new System.EventHandler(this.formFrost_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label labelRemoteAddress;
        internal System.Windows.Forms.TextBox textRemoteAddress;
        internal System.Windows.Forms.Label labelRemotePort;
        internal System.Windows.Forms.TextBox textRemotePort;
        internal System.Windows.Forms.Button buttonConnectRemote;
        internal System.Windows.Forms.ListBox listDatabases;
        internal System.Windows.Forms.Label labelDatabaseName;
        internal System.Windows.Forms.Label labelDatabaseId;
        internal System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.ListBox listTables;
        internal System.Windows.Forms.ListBox listColumns;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label labelColumnDataType;
        internal System.Windows.Forms.Label labelColumnName;
        internal System.Windows.Forms.Button buttonRemoveColumn;
        internal System.Windows.Forms.Button buttonRemoveTable;
        internal System.Windows.Forms.Button buttonRemoveDb;
        internal System.Windows.Forms.Button buttonAddColumn;
        internal System.Windows.Forms.Button buttonAddDb;
        internal System.Windows.Forms.Button buttonAddTable;
        internal System.Windows.Forms.Button buttonAddParticipant;
        internal System.Windows.Forms.ListBox listAcceptedParticipants;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button buttonManageContract;
        internal System.Windows.Forms.Button buttonQuery;
        internal System.Windows.Forms.ListBox listPendingParticipants;
    }
}

