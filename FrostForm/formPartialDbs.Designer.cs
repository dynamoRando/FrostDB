namespace FrostForm
{
    partial class formPartialDbs
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
            this.label2 = new System.Windows.Forms.Label();
            this.listPartialDatabases = new System.Windows.Forms.ListBox();
            this.buttonAcceptContract = new System.Windows.Forms.Button();
            this.buttonRejectContract = new System.Windows.Forms.Button();
            this.buttonViewPendingContract = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.listTables = new System.Windows.Forms.ListBox();
            this.buttonRunQuery = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pending Contracts ";
            // 
            // listPendingContracts
            // 
            this.listPendingContracts.FormattingEnabled = true;
            this.listPendingContracts.ItemHeight = 15;
            this.listPendingContracts.Location = new System.Drawing.Point(12, 39);
            this.listPendingContracts.Name = "listPendingContracts";
            this.listPendingContracts.Size = new System.Drawing.Size(184, 319);
            this.listPendingContracts.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Partial Databases";
            // 
            // listPartialDatabases
            // 
            this.listPartialDatabases.FormattingEnabled = true;
            this.listPartialDatabases.ItemHeight = 15;
            this.listPartialDatabases.Location = new System.Drawing.Point(202, 39);
            this.listPartialDatabases.Name = "listPartialDatabases";
            this.listPartialDatabases.Size = new System.Drawing.Size(181, 319);
            this.listPartialDatabases.TabIndex = 3;
            // 
            // buttonAcceptContract
            // 
            this.buttonAcceptContract.Location = new System.Drawing.Point(12, 364);
            this.buttonAcceptContract.Name = "buttonAcceptContract";
            this.buttonAcceptContract.Size = new System.Drawing.Size(184, 23);
            this.buttonAcceptContract.TabIndex = 4;
            this.buttonAcceptContract.Text = "Accept Contract";
            this.buttonAcceptContract.UseVisualStyleBackColor = true;
            // 
            // buttonRejectContract
            // 
            this.buttonRejectContract.Location = new System.Drawing.Point(12, 393);
            this.buttonRejectContract.Name = "buttonRejectContract";
            this.buttonRejectContract.Size = new System.Drawing.Size(184, 23);
            this.buttonRejectContract.TabIndex = 5;
            this.buttonRejectContract.Text = "Reject Contract";
            this.buttonRejectContract.UseVisualStyleBackColor = true;
            // 
            // buttonViewPendingContract
            // 
            this.buttonViewPendingContract.Location = new System.Drawing.Point(12, 452);
            this.buttonViewPendingContract.Name = "buttonViewPendingContract";
            this.buttonViewPendingContract.Size = new System.Drawing.Size(184, 23);
            this.buttonViewPendingContract.TabIndex = 6;
            this.buttonViewPendingContract.Text = "VIew Contract";
            this.buttonViewPendingContract.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(391, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tables";
            // 
            // listTables
            // 
            this.listTables.FormattingEnabled = true;
            this.listTables.ItemHeight = 15;
            this.listTables.Location = new System.Drawing.Point(389, 39);
            this.listTables.Name = "listTables";
            this.listTables.Size = new System.Drawing.Size(189, 319);
            this.listTables.TabIndex = 8;
            // 
            // buttonRunQuery
            // 
            this.buttonRunQuery.Location = new System.Drawing.Point(202, 364);
            this.buttonRunQuery.Name = "buttonRunQuery";
            this.buttonRunQuery.Size = new System.Drawing.Size(181, 23);
            this.buttonRunQuery.TabIndex = 9;
            this.buttonRunQuery.Text = "Run Query";
            this.buttonRunQuery.UseVisualStyleBackColor = true;
            // 
            // formPartialDbs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 487);
            this.Controls.Add(this.buttonRunQuery);
            this.Controls.Add(this.listTables);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonViewPendingContract);
            this.Controls.Add(this.buttonRejectContract);
            this.Controls.Add(this.buttonAcceptContract);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listPartialDatabases);
            this.Controls.Add(this.listPendingContracts);
            this.Controls.Add(this.label1);
            this.Name = "formPartialDbs";
            this.Text = "formPartialDbs";
            this.Load += new System.EventHandler(this.formPartialDbs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listPendingContracts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listPartialDatabases;
        private System.Windows.Forms.Button buttonAcceptContract;
        private System.Windows.Forms.Button buttonRejectContract;
        private System.Windows.Forms.Button buttonViewPendingContract;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listTables;
        private System.Windows.Forms.Button buttonRunQuery;
    }
}