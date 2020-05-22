namespace FrostForm
{
    partial class formAddParticipant
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
            this.textboxIPAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelDatabaseName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxPortNumber = new System.Windows.Forms.TextBox();
            this.buttonAddParticipant = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Participant IP Address:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textboxIPAddress
            // 
            this.textboxIPAddress.Location = new System.Drawing.Point(152, 43);
            this.textboxIPAddress.Name = "textboxIPAddress";
            this.textboxIPAddress.Size = new System.Drawing.Size(384, 23);
            this.textboxIPAddress.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Add Participant For Database:";
            // 
            // labelDatabaseName
            // 
            this.labelDatabaseName.AutoSize = true;
            this.labelDatabaseName.Location = new System.Drawing.Point(190, 9);
            this.labelDatabaseName.Name = "labelDatabaseName";
            this.labelDatabaseName.Size = new System.Drawing.Size(90, 15);
            this.labelDatabaseName.TabIndex = 3;
            this.labelDatabaseName.Text = "Database Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Participant Data Port:";
            // 
            // textboxPortNumber
            // 
            this.textboxPortNumber.Location = new System.Drawing.Point(152, 85);
            this.textboxPortNumber.Name = "textboxPortNumber";
            this.textboxPortNumber.Size = new System.Drawing.Size(384, 23);
            this.textboxPortNumber.TabIndex = 5;
            // 
            // buttonAddParticipant
            // 
            this.buttonAddParticipant.Location = new System.Drawing.Point(408, 114);
            this.buttonAddParticipant.Name = "buttonAddParticipant";
            this.buttonAddParticipant.Size = new System.Drawing.Size(128, 23);
            this.buttonAddParticipant.TabIndex = 6;
            this.buttonAddParticipant.Text = "Add Participant";
            this.buttonAddParticipant.UseVisualStyleBackColor = true;
            this.buttonAddParticipant.Click += new System.EventHandler(this.buttonAddParticipant_Click);
            // 
            // formAddParticipant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 145);
            this.Controls.Add(this.buttonAddParticipant);
            this.Controls.Add(this.textboxPortNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxIPAddress);
            this.Controls.Add(this.labelDatabaseName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "formAddParticipant";
            this.Text = "formAddParticipant";
            this.Load += new System.EventHandler(this.formAddParticipant_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textboxIPAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelDatabaseName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textboxPortNumber;
        private System.Windows.Forms.Button buttonAddParticipant;
    }
}