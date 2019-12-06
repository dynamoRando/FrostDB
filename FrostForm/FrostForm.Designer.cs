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
            this.listTables = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelRemoteAddress
            // 
            this.labelRemoteAddress.AutoSize = true;
            this.labelRemoteAddress.Location = new System.Drawing.Point(12, 32);
            this.labelRemoteAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRemoteAddress.Name = "labelRemoteAddress";
            this.labelRemoteAddress.Size = new System.Drawing.Size(106, 15);
            this.labelRemoteAddress.TabIndex = 0;
            this.labelRemoteAddress.Text = "Remote IP Address";
            // 
            // textRemoteAddress
            // 
            this.textRemoteAddress.Location = new System.Drawing.Point(12, 49);
            this.textRemoteAddress.Margin = new System.Windows.Forms.Padding(2);
            this.textRemoteAddress.Name = "textRemoteAddress";
            this.textRemoteAddress.Size = new System.Drawing.Size(207, 23);
            this.textRemoteAddress.TabIndex = 1;
            this.textRemoteAddress.Text = "127.0.0.1";
            // 
            // labelRemotePort
            // 
            this.labelRemotePort.AutoSize = true;
            this.labelRemotePort.Location = new System.Drawing.Point(232, 32);
            this.labelRemotePort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRemotePort.Name = "labelRemotePort";
            this.labelRemotePort.Size = new System.Drawing.Size(70, 15);
            this.labelRemotePort.TabIndex = 2;
            this.labelRemotePort.Text = "RemotePort";
            // 
            // textRemotePort
            // 
            this.textRemotePort.Location = new System.Drawing.Point(232, 49);
            this.textRemotePort.Margin = new System.Windows.Forms.Padding(2);
            this.textRemotePort.Name = "textRemotePort";
            this.textRemotePort.Size = new System.Drawing.Size(59, 23);
            this.textRemotePort.TabIndex = 3;
            this.textRemotePort.Text = "519";
            // 
            // buttonConnectRemote
            // 
            this.buttonConnectRemote.Location = new System.Drawing.Point(304, 47);
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
            this.listDatabases.Location = new System.Drawing.Point(5, 21);
            this.listDatabases.Margin = new System.Windows.Forms.Padding(2);
            this.listDatabases.Name = "listDatabases";
            this.listDatabases.Size = new System.Drawing.Size(82, 229);
            this.listDatabases.TabIndex = 5;
            // 
            // labelDatabaseName
            // 
            this.labelDatabaseName.AutoSize = true;
            this.labelDatabaseName.Location = new System.Drawing.Point(105, 21);
            this.labelDatabaseName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDatabaseName.Name = "labelDatabaseName";
            this.labelDatabaseName.Size = new System.Drawing.Size(38, 15);
            this.labelDatabaseName.TabIndex = 6;
            this.labelDatabaseName.Text = "label1";
            // 
            // labelDatabaseId
            // 
            this.labelDatabaseId.AutoSize = true;
            this.labelDatabaseId.Location = new System.Drawing.Point(105, 36);
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
            this.groupBox1.Controls.Add(this.labelRemotePort);
            this.groupBox1.Controls.Add(this.textRemoteAddress);
            this.groupBox1.Controls.Add(this.labelRemoteAddress);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 87);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instance";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listTables);
            this.groupBox2.Controls.Add(this.labelDatabaseId);
            this.groupBox2.Controls.Add(this.labelDatabaseName);
            this.groupBox2.Controls.Add(this.listDatabases);
            this.groupBox2.Location = new System.Drawing.Point(12, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(780, 260);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Databases";
            // 
            // listTables
            // 
            this.listTables.FormattingEnabled = true;
            this.listTables.ItemHeight = 15;
            this.listTables.Location = new System.Drawing.Point(105, 64);
            this.listTables.Name = "listTables";
            this.listTables.Size = new System.Drawing.Size(114, 184);
            this.listTables.TabIndex = 8;
            // 
            // formFrost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 377);
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listTables;
    }
}

