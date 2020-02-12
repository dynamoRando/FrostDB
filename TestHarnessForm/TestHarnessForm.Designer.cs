namespace TestHarnessForm
{
    partial class TestHarnessForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textProcessAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textDataPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textConsolePort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textRootDirectory = new System.Windows.Forms.TextBox();
            this.buttonLaunchTestProcess = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.listRunningProcesses = new System.Windows.Forms.ListBox();
            this.buttonConnectToProcess = new System.Windows.Forms.Button();
            this.buttonClearForm = new System.Windows.Forms.Button();
            this.buttonKillProcess = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textFormConsolePort = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonClearForm);
            this.groupBox1.Controls.Add(this.textFormConsolePort);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.buttonKillProcess);
            this.groupBox1.Controls.Add(this.buttonConnectToProcess);
            this.groupBox1.Controls.Add(this.listRunningProcesses);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.buttonLaunchTestProcess);
            this.groupBox1.Controls.Add(this.textRootDirectory);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textConsolePort);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textDataPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textProcessAddress);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 359);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Running Processes:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address";
            // 
            // textProcessAddress
            // 
            this.textProcessAddress.Location = new System.Drawing.Point(75, 27);
            this.textProcessAddress.Name = "textProcessAddress";
            this.textProcessAddress.Size = new System.Drawing.Size(146, 23);
            this.textProcessAddress.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Data Port";
            // 
            // textDataPort
            // 
            this.textDataPort.Location = new System.Drawing.Point(286, 27);
            this.textDataPort.Name = "textDataPort";
            this.textDataPort.Size = new System.Drawing.Size(74, 23);
            this.textDataPort.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(363, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Console Port";
            // 
            // textConsolePort
            // 
            this.textConsolePort.Location = new System.Drawing.Point(444, 26);
            this.textConsolePort.Name = "textConsolePort";
            this.textConsolePort.Size = new System.Drawing.Size(100, 23);
            this.textConsolePort.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Root Directory";
            // 
            // textRootDirectory
            // 
            this.textRootDirectory.Location = new System.Drawing.Point(99, 69);
            this.textRootDirectory.Name = "textRootDirectory";
            this.textRootDirectory.Size = new System.Drawing.Size(445, 23);
            this.textRootDirectory.TabIndex = 7;
            // 
            // buttonLaunchTestProcess
            // 
            this.buttonLaunchTestProcess.Location = new System.Drawing.Point(18, 110);
            this.buttonLaunchTestProcess.Name = "buttonLaunchTestProcess";
            this.buttonLaunchTestProcess.Size = new System.Drawing.Size(75, 23);
            this.buttonLaunchTestProcess.TabIndex = 8;
            this.buttonLaunchTestProcess.Text = "Launch";
            this.buttonLaunchTestProcess.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Running Processes:";
            // 
            // listRunningProcesses
            // 
            this.listRunningProcesses.FormattingEnabled = true;
            this.listRunningProcesses.ItemHeight = 15;
            this.listRunningProcesses.Location = new System.Drawing.Point(18, 179);
            this.listRunningProcesses.Name = "listRunningProcesses";
            this.listRunningProcesses.Size = new System.Drawing.Size(170, 169);
            this.listRunningProcesses.TabIndex = 10;
            // 
            // buttonConnectToProcess
            // 
            this.buttonConnectToProcess.Location = new System.Drawing.Point(208, 179);
            this.buttonConnectToProcess.Name = "buttonConnectToProcess";
            this.buttonConnectToProcess.Size = new System.Drawing.Size(120, 23);
            this.buttonConnectToProcess.TabIndex = 11;
            this.buttonConnectToProcess.Text = "Connect Form";
            this.buttonConnectToProcess.UseVisualStyleBackColor = true;
            // 
            // buttonClearForm
            // 
            this.buttonClearForm.Location = new System.Drawing.Point(469, 110);
            this.buttonClearForm.Name = "buttonClearForm";
            this.buttonClearForm.Size = new System.Drawing.Size(75, 23);
            this.buttonClearForm.TabIndex = 12;
            this.buttonClearForm.Text = "Clear Form";
            this.buttonClearForm.UseVisualStyleBackColor = true;
            // 
            // buttonKillProcess
            // 
            this.buttonKillProcess.Location = new System.Drawing.Point(208, 208);
            this.buttonKillProcess.Name = "buttonKillProcess";
            this.buttonKillProcess.Size = new System.Drawing.Size(120, 23);
            this.buttonKillProcess.TabIndex = 13;
            this.buttonKillProcess.Text = "Kill Process";
            this.buttonKillProcess.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(334, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Form Console Port";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // textFormConsolePort
            // 
            this.textFormConsolePort.Location = new System.Drawing.Point(444, 179);
            this.textFormConsolePort.Name = "textFormConsolePort";
            this.textFormConsolePort.Size = new System.Drawing.Size(100, 23);
            this.textFormConsolePort.TabIndex = 15;
            // 
            // TestHarnessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "TestHarnessForm";
            this.Text = "Test Harness";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonKillProcess;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonClearForm;
        private System.Windows.Forms.Button buttonConnectToProcess;
        private System.Windows.Forms.ListBox listRunningProcesses;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonLaunchTestProcess;
        private System.Windows.Forms.TextBox textRootDirectory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textConsolePort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textDataPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textProcessAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textFormConsolePort;
    }
}

