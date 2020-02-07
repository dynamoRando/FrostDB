namespace FrostForm
{
    partial class formManagePendingContract
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.listPendingContracts = new System.Windows.Forms.ListBox();
            this.buttonLoadPendingContracts = new System.Windows.Forms.Button();
            this.textDatabaseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textDatabaseIpAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textDatabasePortNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textDatabaseDescription = new System.Windows.Forms.TextBox();
            this.buttonAcceptContract = new System.Windows.Forms.Button();
            this.buttonDeclineContract = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pending Contracts";
            // 
            // listPendingContracts
            // 
            this.listPendingContracts.FormattingEnabled = true;
            this.listPendingContracts.ItemHeight = 15;
            this.listPendingContracts.Location = new System.Drawing.Point(12, 81);
            this.listPendingContracts.Name = "listPendingContracts";
            this.listPendingContracts.Size = new System.Drawing.Size(222, 274);
            this.listPendingContracts.TabIndex = 1;
            this.listPendingContracts.SelectedIndexChanged += new System.EventHandler(this.listPendingContracts_SelectedIndexChanged);
            // 
            // buttonLoadPendingContracts
            // 
            this.buttonLoadPendingContracts.Location = new System.Drawing.Point(12, 21);
            this.buttonLoadPendingContracts.Name = "buttonLoadPendingContracts";
            this.buttonLoadPendingContracts.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadPendingContracts.TabIndex = 2;
            this.buttonLoadPendingContracts.Text = "Load";
            this.buttonLoadPendingContracts.UseVisualStyleBackColor = true;
            this.buttonLoadPendingContracts.Click += new System.EventHandler(this.buttonLoadPendingContracts_Click);
            // 
            // textDatabaseName
            // 
            this.textDatabaseName.Location = new System.Drawing.Point(404, 83);
            this.textDatabaseName.Name = "textDatabaseName";
            this.textDatabaseName.Size = new System.Drawing.Size(317, 23);
            this.textDatabaseName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Database Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(258, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Database IP Address:";
            // 
            // textDatabaseIpAddress
            // 
            this.textDatabaseIpAddress.Location = new System.Drawing.Point(404, 111);
            this.textDatabaseIpAddress.Name = "textDatabaseIpAddress";
            this.textDatabaseIpAddress.Size = new System.Drawing.Size(317, 23);
            this.textDatabaseIpAddress.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(258, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Database Port Number:";
            // 
            // textDatabasePortNumber
            // 
            this.textDatabasePortNumber.Location = new System.Drawing.Point(404, 140);
            this.textDatabasePortNumber.Name = "textDatabasePortNumber";
            this.textDatabasePortNumber.Size = new System.Drawing.Size(317, 23);
            this.textDatabasePortNumber.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(258, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Database Description";
            // 
            // textDatabaseDescription
            // 
            this.textDatabaseDescription.Location = new System.Drawing.Point(404, 169);
            this.textDatabaseDescription.Multiline = true;
            this.textDatabaseDescription.Name = "textDatabaseDescription";
            this.textDatabaseDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textDatabaseDescription.Size = new System.Drawing.Size(317, 135);
            this.textDatabaseDescription.TabIndex = 10;
            // 
            // buttonAcceptContract
            // 
            this.buttonAcceptContract.Location = new System.Drawing.Point(404, 332);
            this.buttonAcceptContract.Name = "buttonAcceptContract";
            this.buttonAcceptContract.Size = new System.Drawing.Size(75, 23);
            this.buttonAcceptContract.TabIndex = 11;
            this.buttonAcceptContract.Text = "Accept";
            this.buttonAcceptContract.UseVisualStyleBackColor = true;
            // 
            // buttonDeclineContract
            // 
            this.buttonDeclineContract.Location = new System.Drawing.Point(485, 332);
            this.buttonDeclineContract.Name = "buttonDeclineContract";
            this.buttonDeclineContract.Size = new System.Drawing.Size(75, 23);
            this.buttonDeclineContract.TabIndex = 12;
            this.buttonDeclineContract.Text = "Decline";
            this.buttonDeclineContract.UseVisualStyleBackColor = true;
            // 
            // formManagePendingContract
            // 
            this.ClientSize = new System.Drawing.Size(830, 555);
            this.Controls.Add(this.buttonDeclineContract);
            this.Controls.Add(this.buttonAcceptContract);
            this.Controls.Add(this.textDatabaseDescription);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textDatabaseName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textDatabasePortNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textDatabaseIpAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonLoadPendingContracts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listPendingContracts);
            this.Name = "formManagePendingContract";
            this.Text = "Pending Contracts";
            this.Load += new System.EventHandler(this.formManagePendingContract_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listPendingContracts;
        private System.Windows.Forms.Button buttonLoadPendingContracts;
        private System.Windows.Forms.TextBox textDatabaseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textDatabaseIpAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textDatabasePortNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textDatabaseDescription;
        private System.Windows.Forms.Button buttonAcceptContract;
        private System.Windows.Forms.Button buttonDeclineContract;
    }
}