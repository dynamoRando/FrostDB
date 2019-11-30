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
            // 
            // labelRemoteAddress
            // 
            this.labelRemoteAddress.AutoSize = true;
            this.labelRemoteAddress.Location = new System.Drawing.Point(10, 8);
            this.labelRemoteAddress.Name = "labelRemoteAddress";
            this.labelRemoteAddress.Size = new System.Drawing.Size(106, 15);
            this.labelRemoteAddress.TabIndex = 0;
            this.labelRemoteAddress.Text = "Remote IP Address";
            // 
            // textRemoteAddress
            // 
            this.textRemoteAddress.Location = new System.Drawing.Point(122, 5);
            this.textRemoteAddress.Name = "textRemoteAddress";
            this.textRemoteAddress.Size = new System.Drawing.Size(152, 23);
            this.textRemoteAddress.TabIndex = 1;
            this.textRemoteAddress.Text = "127.0.0.1";
            // 
            // labelRemotePort
            // 
            this.labelRemotePort.AutoSize = true;
            this.labelRemotePort.Location = new System.Drawing.Point(280, 8);
            this.labelRemotePort.Name = "labelRemotePort";
            this.labelRemotePort.Size = new System.Drawing.Size(70, 15);
            this.labelRemotePort.TabIndex = 2;
            this.labelRemotePort.Text = "RemotePort";
            // 
            // textRemotePort
            // 
            this.textRemotePort.Location = new System.Drawing.Point(356, 5);
            this.textRemotePort.Name = "textRemotePort";
            this.textRemotePort.Size = new System.Drawing.Size(82, 23);
            this.textRemotePort.TabIndex = 3;
            this.textRemotePort.Text = "519";
            // 
            // buttonConnectRemote
            // 
            this.buttonConnectRemote.Location = new System.Drawing.Point(444, 5);
            this.buttonConnectRemote.Name = "buttonConnectRemote";
            this.buttonConnectRemote.Size = new System.Drawing.Size(99, 23);
            this.buttonConnectRemote.TabIndex = 4;
            this.buttonConnectRemote.Text = "Connect";
            this.buttonConnectRemote.UseVisualStyleBackColor = true;
            this.buttonConnectRemote.Click += new System.EventHandler(this.buttonConnectRemote_Click);
            // 
            // listDatabases
            // 
            this.listDatabases.FormattingEnabled = true;
            this.listDatabases.ItemHeight = 15;
            this.listDatabases.Location = new System.Drawing.Point(10, 43);
            this.listDatabases.Name = "listDatabases";
            this.listDatabases.Size = new System.Drawing.Size(116, 394);
            this.listDatabases.TabIndex = 5;
            // 
            // formFrost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listDatabases);
            this.Controls.Add(this.buttonConnectRemote);
            this.Controls.Add(this.textRemotePort);
            this.Controls.Add(this.labelRemotePort);
            this.Controls.Add(this.textRemoteAddress);
            this.Controls.Add(this.labelRemoteAddress);
            this.Name = "formFrost";
            this.Text = "FrostForm";
            this.Load += new System.EventHandler(this.formFrost_Load);

        }

        #endregion

        internal System.Windows.Forms.Label labelRemoteAddress;
        internal System.Windows.Forms.TextBox textRemoteAddress;
        internal System.Windows.Forms.Label labelRemotePort;
        internal System.Windows.Forms.TextBox textRemotePort;
        internal System.Windows.Forms.Button buttonConnectRemote;
        internal System.Windows.Forms.ListBox listDatabases;
    }
}

