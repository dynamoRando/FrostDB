namespace FrostForm
{
    partial class formNewTable
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
            this.labelDatabaseName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listColumns = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonRemoveColumn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textColumnName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboDataType = new System.Windows.Forms.ComboBox();
            this.buttonAddColumn = new System.Windows.Forms.Button();
            this.labelColumnName = new System.Windows.Forms.Label();
            this.labelColumnDataType = new System.Windows.Forms.Label();
            this.buttonCreateTable = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxTableName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelDatabaseName
            // 
            this.labelDatabaseName.AutoSize = true;
            this.labelDatabaseName.Location = new System.Drawing.Point(236, 16);
            this.labelDatabaseName.Name = "labelDatabaseName";
            this.labelDatabaseName.Size = new System.Drawing.Size(112, 20);
            this.labelDatabaseName.TabIndex = 0;
            this.labelDatabaseName.Text = "DatabaseName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Adding Table For Database:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRemoveColumn);
            this.groupBox1.Controls.Add(this.listColumns);
            this.groupBox1.Location = new System.Drawing.Point(22, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(281, 389);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Columns";
            // 
            // listColumns
            // 
            this.listColumns.FormattingEnabled = true;
            this.listColumns.ItemHeight = 20;
            this.listColumns.Location = new System.Drawing.Point(15, 26);
            this.listColumns.Name = "listColumns";
            this.listColumns.Size = new System.Drawing.Size(260, 304);
            this.listColumns.TabIndex = 0;
            this.listColumns.SelectedIndexChanged += new System.EventHandler(this.listColumns_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonAddColumn);
            this.groupBox2.Controls.Add(this.comboDataType);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textColumnName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(309, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(479, 215);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add Column";
            // 
            // buttonRemoveColumn
            // 
            this.buttonRemoveColumn.Location = new System.Drawing.Point(15, 336);
            this.buttonRemoveColumn.Name = "buttonRemoveColumn";
            this.buttonRemoveColumn.Size = new System.Drawing.Size(260, 29);
            this.buttonRemoveColumn.TabIndex = 1;
            this.buttonRemoveColumn.Text = "Remove Column";
            this.buttonRemoveColumn.UseVisualStyleBackColor = true;
            this.buttonRemoveColumn.Click += new System.EventHandler(this.buttonRemoveColumn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-87, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Column Name:";
            // 
            // textColumnName
            // 
            this.textColumnName.Location = new System.Drawing.Point(18, 56);
            this.textColumnName.Name = "textColumnName";
            this.textColumnName.Size = new System.Drawing.Size(440, 27);
            this.textColumnName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Column Data Type:";
            // 
            // comboDataType
            // 
            this.comboDataType.FormattingEnabled = true;
            this.comboDataType.Location = new System.Drawing.Point(18, 125);
            this.comboDataType.Name = "comboDataType";
            this.comboDataType.Size = new System.Drawing.Size(440, 28);
            this.comboDataType.TabIndex = 4;
            this.comboDataType.SelectedIndexChanged += new System.EventHandler(this.comboDataType_SelectedIndexChanged);
            // 
            // buttonAddColumn
            // 
            this.buttonAddColumn.Location = new System.Drawing.Point(310, 171);
            this.buttonAddColumn.Name = "buttonAddColumn";
            this.buttonAddColumn.Size = new System.Drawing.Size(148, 29);
            this.buttonAddColumn.TabIndex = 5;
            this.buttonAddColumn.Text = "Add Column";
            this.buttonAddColumn.UseVisualStyleBackColor = true;
            this.buttonAddColumn.Click += new System.EventHandler(this.buttonAddColumn_Click);
            // 
            // labelColumnName
            // 
            this.labelColumnName.AutoSize = true;
            this.labelColumnName.Location = new System.Drawing.Point(327, 63);
            this.labelColumnName.Name = "labelColumnName";
            this.labelColumnName.Size = new System.Drawing.Size(104, 20);
            this.labelColumnName.TabIndex = 4;
            this.labelColumnName.Text = "Column Name";
            // 
            // labelColumnDataType
            // 
            this.labelColumnDataType.AutoSize = true;
            this.labelColumnDataType.Location = new System.Drawing.Point(327, 99);
            this.labelColumnDataType.Name = "labelColumnDataType";
            this.labelColumnDataType.Size = new System.Drawing.Size(131, 20);
            this.labelColumnDataType.TabIndex = 5;
            this.labelColumnDataType.Text = "Column Data Type";
            // 
            // buttonCreateTable
            // 
            this.buttonCreateTable.Location = new System.Drawing.Point(619, 451);
            this.buttonCreateTable.Name = "buttonCreateTable";
            this.buttonCreateTable.Size = new System.Drawing.Size(148, 29);
            this.buttonCreateTable.TabIndex = 6;
            this.buttonCreateTable.Text = "Create Table";
            this.buttonCreateTable.UseVisualStyleBackColor = true;
            this.buttonCreateTable.Click += new System.EventHandler(this.buttonCreateTable_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(309, 394);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Table Name:";
            // 
            // textBoxTableName
            // 
            this.textBoxTableName.Location = new System.Drawing.Point(319, 418);
            this.textBoxTableName.Name = "textBoxTableName";
            this.textBoxTableName.Size = new System.Drawing.Size(448, 27);
            this.textBoxTableName.TabIndex = 8;
            // 
            // formNewTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 486);
            this.Controls.Add(this.buttonCreateTable);
            this.Controls.Add(this.textBoxTableName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelColumnDataType);
            this.Controls.Add(this.labelColumnName);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelDatabaseName);
            this.Controls.Add(this.label2);
            this.Name = "formNewTable";
            this.Text = "formNewTable";
            this.Load += new System.EventHandler(this.formNewTable_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDatabaseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listColumns;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonRemoveColumn;
        private System.Windows.Forms.TextBox textColumnName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAddColumn;
        private System.Windows.Forms.ComboBox comboDataType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelColumnName;
        private System.Windows.Forms.Label labelColumnDataType;
        private System.Windows.Forms.Button buttonCreateTable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxTableName;
    }
}