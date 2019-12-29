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
            this.buttonRemoveColumn = new System.Windows.Forms.Button();
            this.listColumns = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonAddColumn = new System.Windows.Forms.Button();
            this.comboDataType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textColumnName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.labelDatabaseName.Location = new System.Drawing.Point(206, 12);
            this.labelDatabaseName.Name = "labelDatabaseName";
            this.labelDatabaseName.Size = new System.Drawing.Size(87, 15);
            this.labelDatabaseName.TabIndex = 0;
            this.labelDatabaseName.Text = "DatabaseName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Adding Table For Database:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRemoveColumn);
            this.groupBox1.Controls.Add(this.listColumns);
            this.groupBox1.Location = new System.Drawing.Point(19, 37);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(246, 292);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Columns";
            // 
            // buttonRemoveColumn
            // 
            this.buttonRemoveColumn.Location = new System.Drawing.Point(13, 252);
            this.buttonRemoveColumn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonRemoveColumn.Name = "buttonRemoveColumn";
            this.buttonRemoveColumn.Size = new System.Drawing.Size(228, 22);
            this.buttonRemoveColumn.TabIndex = 1;
            this.buttonRemoveColumn.Text = "Remove Column";
            this.buttonRemoveColumn.UseVisualStyleBackColor = true;
            this.buttonRemoveColumn.Click += new System.EventHandler(this.buttonRemoveColumn_Click);
            // 
            // listColumns
            // 
            this.listColumns.FormattingEnabled = true;
            this.listColumns.ItemHeight = 15;
            this.listColumns.Location = new System.Drawing.Point(13, 20);
            this.listColumns.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listColumns.Name = "listColumns";
            this.listColumns.Size = new System.Drawing.Size(228, 229);
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
            this.groupBox2.Location = new System.Drawing.Point(270, 123);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(419, 161);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add Column";
            // 
            // buttonAddColumn
            // 
            this.buttonAddColumn.Location = new System.Drawing.Point(271, 128);
            this.buttonAddColumn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAddColumn.Name = "buttonAddColumn";
            this.buttonAddColumn.Size = new System.Drawing.Size(130, 22);
            this.buttonAddColumn.TabIndex = 5;
            this.buttonAddColumn.Text = "Add Column";
            this.buttonAddColumn.UseVisualStyleBackColor = true;
            this.buttonAddColumn.Click += new System.EventHandler(this.buttonAddColumn_Click);
            // 
            // comboDataType
            // 
            this.comboDataType.FormattingEnabled = true;
            this.comboDataType.Location = new System.Drawing.Point(16, 94);
            this.comboDataType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboDataType.Name = "comboDataType";
            this.comboDataType.Size = new System.Drawing.Size(386, 23);
            this.comboDataType.TabIndex = 4;
            this.comboDataType.SelectedIndexChanged += new System.EventHandler(this.comboDataType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Column Data Type:";
            // 
            // textColumnName
            // 
            this.textColumnName.Location = new System.Drawing.Point(16, 42);
            this.textColumnName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textColumnName.Name = "textColumnName";
            this.textColumnName.Size = new System.Drawing.Size(386, 23);
            this.textColumnName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Column Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-76, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // labelColumnName
            // 
            this.labelColumnName.AutoSize = true;
            this.labelColumnName.Location = new System.Drawing.Point(286, 47);
            this.labelColumnName.Name = "labelColumnName";
            this.labelColumnName.Size = new System.Drawing.Size(85, 15);
            this.labelColumnName.TabIndex = 4;
            this.labelColumnName.Text = "Column Name";
            // 
            // labelColumnDataType
            // 
            this.labelColumnDataType.AutoSize = true;
            this.labelColumnDataType.Location = new System.Drawing.Point(286, 74);
            this.labelColumnDataType.Name = "labelColumnDataType";
            this.labelColumnDataType.Size = new System.Drawing.Size(105, 15);
            this.labelColumnDataType.TabIndex = 5;
            this.labelColumnDataType.Text = "Column Data Type";
            // 
            // buttonCreateTable
            // 
            this.buttonCreateTable.Location = new System.Drawing.Point(542, 338);
            this.buttonCreateTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCreateTable.Name = "buttonCreateTable";
            this.buttonCreateTable.Size = new System.Drawing.Size(130, 22);
            this.buttonCreateTable.TabIndex = 6;
            this.buttonCreateTable.Text = "Create Table";
            this.buttonCreateTable.UseVisualStyleBackColor = true;
            this.buttonCreateTable.Click += new System.EventHandler(this.buttonCreateTable_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(270, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Table Name:";
            // 
            // textBoxTableName
            // 
            this.textBoxTableName.Location = new System.Drawing.Point(279, 314);
            this.textBoxTableName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxTableName.Name = "textBoxTableName";
            this.textBoxTableName.Size = new System.Drawing.Size(392, 23);
            this.textBoxTableName.TabIndex = 8;
            // 
            // formNewTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 364);
            this.Controls.Add(this.buttonCreateTable);
            this.Controls.Add(this.textBoxTableName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelColumnDataType);
            this.Controls.Add(this.labelColumnName);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelDatabaseName);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "formNewTable";
            this.Text = "New Table";
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