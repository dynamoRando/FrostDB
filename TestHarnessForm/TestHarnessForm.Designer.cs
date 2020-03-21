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
            this.buttonLoadTestSetup = new System.Windows.Forms.Button();
            this.textLoadTestSetup = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonSaveTestSetup = new System.Windows.Forms.Button();
            this.textTestSetupLocation = new System.Windows.Forms.TextBox();
            this.buttonClearForm = new System.Windows.Forms.Button();
            this.textFormConsolePort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonKillProcess = new System.Windows.Forms.Button();
            this.buttonConnectToProcess = new System.Windows.Forms.Button();
            this.listRunningProcesses = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonLaunchTestProcess = new System.Windows.Forms.Button();
            this.textRootDirectory = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textConsolePort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textDataPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textLogFile = new System.Windows.Forms.TextBox();
            this.buttonWatchLogFile = new System.Windows.Forms.Button();
            this.buttonLoadLogFile = new System.Windows.Forms.Button();
            this.textLogFileLocation = new System.Windows.Forms.TextBox();
            this.comboProcessAddress = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboProcessAddress);
            this.groupBox1.Controls.Add(this.buttonLoadTestSetup);
            this.groupBox1.Controls.Add(this.textLoadTestSetup);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.buttonSaveTestSetup);
            this.groupBox1.Controls.Add(this.textTestSetupLocation);
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
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 414);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Running Processes:";
            // 
            // buttonLoadTestSetup
            // 
            this.buttonLoadTestSetup.Location = new System.Drawing.Point(469, 375);
            this.buttonLoadTestSetup.Name = "buttonLoadTestSetup";
            this.buttonLoadTestSetup.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadTestSetup.TabIndex = 21;
            this.buttonLoadTestSetup.Text = "Load";
            this.buttonLoadTestSetup.UseVisualStyleBackColor = true;
            this.buttonLoadTestSetup.Click += new System.EventHandler(this.buttonLoadTestSetup_Click);
            // 
            // textLoadTestSetup
            // 
            this.textLoadTestSetup.Location = new System.Drawing.Point(208, 346);
            this.textLoadTestSetup.Name = "textLoadTestSetup";
            this.textLoadTestSetup.Size = new System.Drawing.Size(336, 23);
            this.textLoadTestSetup.TabIndex = 20;
            this.textLoadTestSetup.Text = "C:\\FrostTest\\setup.txt";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(208, 328);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 15);
            this.label8.TabIndex = 19;
            this.label8.Text = "Load Setup";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(208, 250);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "Save Setup";
            // 
            // buttonSaveTestSetup
            // 
            this.buttonSaveTestSetup.Location = new System.Drawing.Point(469, 297);
            this.buttonSaveTestSetup.Name = "buttonSaveTestSetup";
            this.buttonSaveTestSetup.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveTestSetup.TabIndex = 18;
            this.buttonSaveTestSetup.Text = "Save";
            this.buttonSaveTestSetup.UseVisualStyleBackColor = true;
            this.buttonSaveTestSetup.Click += new System.EventHandler(this.buttonSaveTestSetup_Click);
            // 
            // textTestSetupLocation
            // 
            this.textTestSetupLocation.Location = new System.Drawing.Point(208, 268);
            this.textTestSetupLocation.Name = "textTestSetupLocation";
            this.textTestSetupLocation.Size = new System.Drawing.Size(336, 23);
            this.textTestSetupLocation.TabIndex = 17;
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
            // textFormConsolePort
            // 
            this.textFormConsolePort.Location = new System.Drawing.Point(444, 179);
            this.textFormConsolePort.Name = "textFormConsolePort";
            this.textFormConsolePort.Size = new System.Drawing.Size(100, 23);
            this.textFormConsolePort.TabIndex = 15;
            this.textFormConsolePort.TextChanged += new System.EventHandler(this.textFormConsolePort_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(334, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Form Console Port";
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
            // buttonConnectToProcess
            // 
            this.buttonConnectToProcess.Location = new System.Drawing.Point(208, 179);
            this.buttonConnectToProcess.Name = "buttonConnectToProcess";
            this.buttonConnectToProcess.Size = new System.Drawing.Size(120, 23);
            this.buttonConnectToProcess.TabIndex = 11;
            this.buttonConnectToProcess.Text = "Connect Form";
            this.buttonConnectToProcess.UseVisualStyleBackColor = true;
            this.buttonConnectToProcess.Click += new System.EventHandler(this.buttonConnectToProcess_Click);
            // 
            // listRunningProcesses
            // 
            this.listRunningProcesses.FormattingEnabled = true;
            this.listRunningProcesses.ItemHeight = 15;
            this.listRunningProcesses.Location = new System.Drawing.Point(18, 179);
            this.listRunningProcesses.Name = "listRunningProcesses";
            this.listRunningProcesses.Size = new System.Drawing.Size(170, 214);
            this.listRunningProcesses.TabIndex = 10;
            this.listRunningProcesses.SelectedIndexChanged += new System.EventHandler(this.listRunningProcesses_SelectedIndexChanged);
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
            // buttonLaunchTestProcess
            // 
            this.buttonLaunchTestProcess.Location = new System.Drawing.Point(18, 110);
            this.buttonLaunchTestProcess.Name = "buttonLaunchTestProcess";
            this.buttonLaunchTestProcess.Size = new System.Drawing.Size(75, 23);
            this.buttonLaunchTestProcess.TabIndex = 8;
            this.buttonLaunchTestProcess.Text = "Launch";
            this.buttonLaunchTestProcess.UseVisualStyleBackColor = true;
            this.buttonLaunchTestProcess.Click += new System.EventHandler(this.buttonLaunchTestProcess_Click);
            // 
            // textRootDirectory
            // 
            this.textRootDirectory.Location = new System.Drawing.Point(99, 69);
            this.textRootDirectory.Name = "textRootDirectory";
            this.textRootDirectory.Size = new System.Drawing.Size(445, 23);
            this.textRootDirectory.TabIndex = 7;
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
            // textConsolePort
            // 
            this.textConsolePort.Location = new System.Drawing.Point(444, 26);
            this.textConsolePort.Name = "textConsolePort";
            this.textConsolePort.Size = new System.Drawing.Size(100, 23);
            this.textConsolePort.TabIndex = 5;
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
            // textDataPort
            // 
            this.textDataPort.Location = new System.Drawing.Point(286, 27);
            this.textDataPort.Name = "textDataPort";
            this.textDataPort.Size = new System.Drawing.Size(74, 23);
            this.textDataPort.TabIndex = 3;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textLogFile);
            this.groupBox2.Controls.Add(this.buttonWatchLogFile);
            this.groupBox2.Controls.Add(this.buttonLoadLogFile);
            this.groupBox2.Controls.Add(this.textLogFileLocation);
            this.groupBox2.Location = new System.Drawing.Point(606, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(493, 398);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logging";
            // 
            // textLogFile
            // 
            this.textLogFile.Location = new System.Drawing.Point(6, 85);
            this.textLogFile.Multiline = true;
            this.textLogFile.Name = "textLogFile";
            this.textLogFile.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textLogFile.Size = new System.Drawing.Size(481, 293);
            this.textLogFile.TabIndex = 3;
            // 
            // buttonWatchLogFile
            // 
            this.buttonWatchLogFile.Location = new System.Drawing.Point(331, 56);
            this.buttonWatchLogFile.Name = "buttonWatchLogFile";
            this.buttonWatchLogFile.Size = new System.Drawing.Size(75, 23);
            this.buttonWatchLogFile.TabIndex = 2;
            this.buttonWatchLogFile.Text = "Watch";
            this.buttonWatchLogFile.UseVisualStyleBackColor = true;
            this.buttonWatchLogFile.Click += new System.EventHandler(this.buttonWatchLogFile_Click);
            // 
            // buttonLoadLogFile
            // 
            this.buttonLoadLogFile.Location = new System.Drawing.Point(412, 56);
            this.buttonLoadLogFile.Name = "buttonLoadLogFile";
            this.buttonLoadLogFile.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadLogFile.TabIndex = 1;
            this.buttonLoadLogFile.Text = "Load";
            this.buttonLoadLogFile.UseVisualStyleBackColor = true;
            // 
            // textLogFileLocation
            // 
            this.textLogFileLocation.Location = new System.Drawing.Point(6, 27);
            this.textLogFileLocation.Name = "textLogFileLocation";
            this.textLogFileLocation.Size = new System.Drawing.Size(481, 23);
            this.textLogFileLocation.TabIndex = 0;
            // 
            // comboProcessAddress
            // 
            this.comboProcessAddress.FormattingEnabled = true;
            this.comboProcessAddress.Location = new System.Drawing.Point(78, 27);
            this.comboProcessAddress.Name = "comboProcessAddress";
            this.comboProcessAddress.Size = new System.Drawing.Size(143, 23);
            this.comboProcessAddress.TabIndex = 22;
            // 
            // TestHarnessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "TestHarnessForm";
            this.Text = "Test Harness";
            this.Load += new System.EventHandler(this.TestHarnessForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textFormConsolePort;
        private System.Windows.Forms.Button buttonLoadTestSetup;
        private System.Windows.Forms.TextBox textLoadTestSetup;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonSaveTestSetup;
        private System.Windows.Forms.TextBox textTestSetupLocation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textLogFile;
        private System.Windows.Forms.Button buttonWatchLogFile;
        private System.Windows.Forms.Button buttonLoadLogFile;
        private System.Windows.Forms.TextBox textLogFileLocation;
        private System.Windows.Forms.ComboBox comboProcessAddress;
    }
}

