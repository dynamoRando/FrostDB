namespace FrostForm
{
    partial class formNewDb
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
            this.buttonAddDb = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textboxDbName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonAddDb
            // 
            this.buttonAddDb.Location = new System.Drawing.Point(478, 4);
            this.buttonAddDb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonAddDb.Name = "buttonAddDb";
            this.buttonAddDb.Size = new System.Drawing.Size(86, 31);
            this.buttonAddDb.TabIndex = 0;
            this.buttonAddDb.Text = "Add DB";
            this.buttonAddDb.UseVisualStyleBackColor = true;
            this.buttonAddDb.Click += new System.EventHandler(this.buttonAddDb_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(570, 4);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 31);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Database Name:";
            // 
            // textboxDbName
            // 
            this.textboxDbName.Location = new System.Drawing.Point(127, 4);
            this.textboxDbName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textboxDbName.Name = "textboxDbName";
            this.textboxDbName.Size = new System.Drawing.Size(330, 27);
            this.textboxDbName.TabIndex = 3;
            // 
            // formNewDb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 48);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAddDb);
            this.Controls.Add(this.textboxDbName);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "formNewDb";
            this.Text = "Add New Database";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddDb;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textboxDbName;
    }
}