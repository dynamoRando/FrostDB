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
            // formManagePendingContract
            // 
            this.ClientSize = new System.Drawing.Size(830, 555);
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
    }
}