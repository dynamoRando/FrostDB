namespace FrostForm
{
    partial class FormQueryWindow
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboExampleQuery = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.textResults = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textQuery = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboTables = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboDatabase = new System.Windows.Forms.ComboBox();
            this.buttonLoadDatabases = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.listColumns = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.listParticipants = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboExampleQuery);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.buttonClear);
            this.groupBox1.Controls.Add(this.buttonExecute);
            this.groupBox1.Controls.Add(this.textResults);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textQuery);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboTables);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboDatabase);
            this.groupBox1.Location = new System.Drawing.Point(46, 86);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Size = new System.Drawing.Size(1305, 1016);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Query";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // comboExampleQuery
            // 
            this.comboExampleQuery.FormattingEnabled = true;
            this.comboExampleQuery.Location = new System.Drawing.Point(171, 249);
            this.comboExampleQuery.Name = "comboExampleQuery";
            this.comboExampleQuery.Size = new System.Drawing.Size(1105, 38);
            this.comboExampleQuery.TabIndex = 12;
            this.comboExampleQuery.SelectedIndexChanged += new System.EventHandler(this.comboExampleQuery_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 249);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(153, 30);
            this.label8.TabIndex = 11;
            this.label8.Text = "Example Query";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(1041, 176);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(115, 46);
            this.buttonClear.TabIndex = 5;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(1166, 176);
            this.buttonExecute.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(110, 46);
            this.buttonExecute.TabIndex = 4;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // textResults
            // 
            this.textResults.Location = new System.Drawing.Point(12, 642);
            this.textResults.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textResults.Multiline = true;
            this.textResults.Name = "textResults";
            this.textResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textResults.Size = new System.Drawing.Size(1279, 358);
            this.textResults.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 606);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 30);
            this.label5.TabIndex = 9;
            this.label5.Text = "Results";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 301);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 30);
            this.label4.TabIndex = 8;
            this.label4.Text = "Query";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-69, 174);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 30);
            this.label3.TabIndex = 7;
            this.label3.Text = "label3";
            // 
            // textQuery
            // 
            this.textQuery.Location = new System.Drawing.Point(10, 337);
            this.textQuery.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textQuery.Multiline = true;
            this.textQuery.Name = "textQuery";
            this.textQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textQuery.Size = new System.Drawing.Size(1281, 259);
            this.textQuery.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 124);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 30);
            this.label2.TabIndex = 2;
            this.label2.Text = "Table";
            // 
            // comboTables
            // 
            this.comboTables.FormattingEnabled = true;
            this.comboTables.Location = new System.Drawing.Point(115, 118);
            this.comboTables.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.comboTables.Name = "comboTables";
            this.comboTables.Size = new System.Drawing.Size(1161, 38);
            this.comboTables.TabIndex = 3;
            this.comboTables.SelectedIndexChanged += new System.EventHandler(this.comboTables_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Database";
            // 
            // comboDatabase
            // 
            this.comboDatabase.FormattingEnabled = true;
            this.comboDatabase.Location = new System.Drawing.Point(115, 52);
            this.comboDatabase.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.comboDatabase.Name = "comboDatabase";
            this.comboDatabase.Size = new System.Drawing.Size(1161, 38);
            this.comboDatabase.TabIndex = 1;
            this.comboDatabase.SelectedIndexChanged += new System.EventHandler(this.comboDatabase_SelectedIndexChanged);
            // 
            // buttonLoadDatabases
            // 
            this.buttonLoadDatabases.Location = new System.Drawing.Point(46, 24);
            this.buttonLoadDatabases.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonLoadDatabases.Name = "buttonLoadDatabases";
            this.buttonLoadDatabases.Size = new System.Drawing.Size(175, 46);
            this.buttonLoadDatabases.TabIndex = 1;
            this.buttonLoadDatabases.Text = "Load Databases";
            this.buttonLoadDatabases.UseVisualStyleBackColor = true;
            this.buttonLoadDatabases.Click += new System.EventHandler(this.buttonLoadDatabases_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1382, 110);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 30);
            this.label6.TabIndex = 2;
            this.label6.Text = "Columns";
            // 
            // listColumns
            // 
            this.listColumns.FormattingEnabled = true;
            this.listColumns.ItemHeight = 30;
            this.listColumns.Location = new System.Drawing.Point(1382, 146);
            this.listColumns.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.listColumns.Name = "listColumns";
            this.listColumns.Size = new System.Drawing.Size(277, 934);
            this.listColumns.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1666, 110);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 30);
            this.label7.TabIndex = 4;
            this.label7.Text = "Participant";
            // 
            // listParticipants
            // 
            this.listParticipants.FormattingEnabled = true;
            this.listParticipants.ItemHeight = 30;
            this.listParticipants.Location = new System.Drawing.Point(1666, 146);
            this.listParticipants.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.listParticipants.Name = "listParticipants";
            this.listParticipants.Size = new System.Drawing.Size(265, 934);
            this.listParticipants.TabIndex = 5;
            // 
            // FormQueryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1954, 1118);
            this.Controls.Add(this.listParticipants);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listColumns);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonLoadDatabases);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "FormQueryWindow";
            this.Text = "FormQueryWindow";
            this.Load += new System.EventHandler(this.FormQueryWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textQuery;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboTables;
        private System.Windows.Forms.ComboBox comboDatabase;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textResults;
        private System.Windows.Forms.Button buttonLoadDatabases;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listColumns;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox listParticipants;
        private System.Windows.Forms.ComboBox comboExampleQuery;
        private System.Windows.Forms.Label label8;
    }
}