﻿namespace FrostForm
{
    partial class formManageContract
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
            this.labelDatabase = new System.Windows.Forms.Label();
            this.labelDatabaseName = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textboxDescription = new System.Windows.Forms.TextBox();
            this.labelSchema = new System.Windows.Forms.Label();
            this.textboxSchema = new System.Windows.Forms.TextBox();
            this.labelParticipantDataOverview = new System.Windows.Forms.Label();
            this.textboxParticipantDataOverview = new System.Windows.Forms.TextBox();
            this.labelAuthorDataOverview = new System.Windows.Forms.Label();
            this.textboxAuthorDataOverview = new System.Windows.Forms.TextBox();
            this.buttonSaveContract = new System.Windows.Forms.Button();
            this.buttonSaveParticipantRights = new System.Windows.Forms.Button();
            this.checkParticipantRead = new System.Windows.Forms.CheckBox();
            this.checkParticipantWrite = new System.Windows.Forms.CheckBox();
            this.checkParticipantModify = new System.Windows.Forms.CheckBox();
            this.checkParticipantDelete = new System.Windows.Forms.CheckBox();
            this.buttonSaveAuthorRights = new System.Windows.Forms.Button();
            this.checkAuthorRead = new System.Windows.Forms.CheckBox();
            this.checkAuthorWrite = new System.Windows.Forms.CheckBox();
            this.checkAuthorModify = new System.Windows.Forms.CheckBox();
            this.checkAuthorDelete = new System.Windows.Forms.CheckBox();
            this.listboxParticipantTables = new System.Windows.Forms.ListBox();
            this.listboxAuthorTables = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Location = new System.Drawing.Point(38, 54);
            this.labelDatabase.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(105, 30);
            this.labelDatabase.TabIndex = 0;
            this.labelDatabase.Text = "Database:";
            // 
            // labelDatabaseName
            // 
            this.labelDatabaseName.AutoSize = true;
            this.labelDatabaseName.Location = new System.Drawing.Point(147, 54);
            this.labelDatabaseName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelDatabaseName.Name = "labelDatabaseName";
            this.labelDatabaseName.Size = new System.Drawing.Size(156, 30);
            this.labelDatabaseName.TabIndex = 1;
            this.labelDatabaseName.Text = "DatabaseName";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(38, 124);
            this.labelDescription.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(123, 30);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "Description:";
            // 
            // textboxDescription
            // 
            this.textboxDescription.Location = new System.Drawing.Point(38, 160);
            this.textboxDescription.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textboxDescription.Multiline = true;
            this.textboxDescription.Name = "textboxDescription";
            this.textboxDescription.Size = new System.Drawing.Size(1310, 134);
            this.textboxDescription.TabIndex = 3;
            // 
            // labelSchema
            // 
            this.labelSchema.AutoSize = true;
            this.labelSchema.Location = new System.Drawing.Point(38, 329);
            this.labelSchema.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelSchema.Name = "labelSchema";
            this.labelSchema.Size = new System.Drawing.Size(91, 30);
            this.labelSchema.TabIndex = 4;
            this.labelSchema.Text = "Schema:";
            // 
            // textboxSchema
            // 
            this.textboxSchema.Location = new System.Drawing.Point(38, 365);
            this.textboxSchema.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textboxSchema.Multiline = true;
            this.textboxSchema.Name = "textboxSchema";
            this.textboxSchema.Size = new System.Drawing.Size(1310, 149);
            this.textboxSchema.TabIndex = 5;
            // 
            // labelParticipantDataOverview
            // 
            this.labelParticipantDataOverview.AutoSize = true;
            this.labelParticipantDataOverview.Location = new System.Drawing.Point(38, 529);
            this.labelParticipantDataOverview.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelParticipantDataOverview.Name = "labelParticipantDataOverview";
            this.labelParticipantDataOverview.Size = new System.Drawing.Size(257, 30);
            this.labelParticipantDataOverview.TabIndex = 6;
            this.labelParticipantDataOverview.Text = "Participant Data Overview:";
            // 
            // textboxParticipantDataOverview
            // 
            this.textboxParticipantDataOverview.Location = new System.Drawing.Point(38, 577);
            this.textboxParticipantDataOverview.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textboxParticipantDataOverview.Multiline = true;
            this.textboxParticipantDataOverview.Name = "textboxParticipantDataOverview";
            this.textboxParticipantDataOverview.Size = new System.Drawing.Size(1310, 115);
            this.textboxParticipantDataOverview.TabIndex = 7;
            // 
            // labelAuthorDataOverview
            // 
            this.labelAuthorDataOverview.AutoSize = true;
            this.labelAuthorDataOverview.Location = new System.Drawing.Point(38, 1085);
            this.labelAuthorDataOverview.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelAuthorDataOverview.Name = "labelAuthorDataOverview";
            this.labelAuthorDataOverview.Size = new System.Drawing.Size(223, 30);
            this.labelAuthorDataOverview.TabIndex = 8;
            this.labelAuthorDataOverview.Text = "Author Data Overview:";
            // 
            // textboxAuthorDataOverview
            // 
            this.textboxAuthorDataOverview.Location = new System.Drawing.Point(38, 1135);
            this.textboxAuthorDataOverview.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textboxAuthorDataOverview.Multiline = true;
            this.textboxAuthorDataOverview.Name = "textboxAuthorDataOverview";
            this.textboxAuthorDataOverview.Size = new System.Drawing.Size(1310, 129);
            this.textboxAuthorDataOverview.TabIndex = 9;
            // 
            // buttonSaveContract
            // 
            this.buttonSaveContract.Location = new System.Drawing.Point(1167, 1637);
            this.buttonSaveContract.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonSaveContract.Name = "buttonSaveContract";
            this.buttonSaveContract.Size = new System.Drawing.Size(197, 46);
            this.buttonSaveContract.TabIndex = 10;
            this.buttonSaveContract.Text = "Save Contract";
            this.buttonSaveContract.UseVisualStyleBackColor = true;
            this.buttonSaveContract.Click += new System.EventHandler(this.buttonSaveContract_Click);
            // 
            // buttonSaveParticipantRights
            // 
            this.buttonSaveParticipantRights.Location = new System.Drawing.Point(603, 1016);
            this.buttonSaveParticipantRights.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonSaveParticipantRights.Name = "buttonSaveParticipantRights";
            this.buttonSaveParticipantRights.Size = new System.Drawing.Size(129, 46);
            this.buttonSaveParticipantRights.TabIndex = 14;
            this.buttonSaveParticipantRights.Text = "Save";
            this.buttonSaveParticipantRights.UseVisualStyleBackColor = true;
            this.buttonSaveParticipantRights.Click += new System.EventHandler(this.buttonSaveParticipantRights_Click);
            // 
            // checkParticipantRead
            // 
            this.checkParticipantRead.AutoSize = true;
            this.checkParticipantRead.Location = new System.Drawing.Point(603, 732);
            this.checkParticipantRead.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.checkParticipantRead.Name = "checkParticipantRead";
            this.checkParticipantRead.Size = new System.Drawing.Size(85, 34);
            this.checkParticipantRead.TabIndex = 15;
            this.checkParticipantRead.Text = "Read";
            this.checkParticipantRead.UseVisualStyleBackColor = true;
            // 
            // checkParticipantWrite
            // 
            this.checkParticipantWrite.AutoSize = true;
            this.checkParticipantWrite.Location = new System.Drawing.Point(603, 780);
            this.checkParticipantWrite.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.checkParticipantWrite.Name = "checkParticipantWrite";
            this.checkParticipantWrite.Size = new System.Drawing.Size(89, 34);
            this.checkParticipantWrite.TabIndex = 16;
            this.checkParticipantWrite.Text = "Write";
            this.checkParticipantWrite.UseVisualStyleBackColor = true;
            // 
            // checkParticipantModify
            // 
            this.checkParticipantModify.AutoSize = true;
            this.checkParticipantModify.Location = new System.Drawing.Point(603, 830);
            this.checkParticipantModify.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.checkParticipantModify.Name = "checkParticipantModify";
            this.checkParticipantModify.Size = new System.Drawing.Size(104, 34);
            this.checkParticipantModify.TabIndex = 17;
            this.checkParticipantModify.Text = "Modify";
            this.checkParticipantModify.UseVisualStyleBackColor = true;
            // 
            // checkParticipantDelete
            // 
            this.checkParticipantDelete.AutoSize = true;
            this.checkParticipantDelete.Location = new System.Drawing.Point(603, 880);
            this.checkParticipantDelete.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.checkParticipantDelete.Name = "checkParticipantDelete";
            this.checkParticipantDelete.Size = new System.Drawing.Size(99, 34);
            this.checkParticipantDelete.TabIndex = 18;
            this.checkParticipantDelete.Text = "Delete";
            this.checkParticipantDelete.UseVisualStyleBackColor = true;
            // 
            // buttonSaveAuthorRights
            // 
            this.buttonSaveAuthorRights.Location = new System.Drawing.Point(603, 1637);
            this.buttonSaveAuthorRights.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonSaveAuthorRights.Name = "buttonSaveAuthorRights";
            this.buttonSaveAuthorRights.Size = new System.Drawing.Size(129, 46);
            this.buttonSaveAuthorRights.TabIndex = 20;
            this.buttonSaveAuthorRights.Text = "Save";
            this.buttonSaveAuthorRights.UseVisualStyleBackColor = true;
            this.buttonSaveAuthorRights.Click += new System.EventHandler(this.buttonSaveAuthorRights_Click);
            // 
            // checkAuthorRead
            // 
            this.checkAuthorRead.AutoSize = true;
            this.checkAuthorRead.Location = new System.Drawing.Point(603, 1289);
            this.checkAuthorRead.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.checkAuthorRead.Name = "checkAuthorRead";
            this.checkAuthorRead.Size = new System.Drawing.Size(85, 34);
            this.checkAuthorRead.TabIndex = 21;
            this.checkAuthorRead.Text = "Read";
            this.checkAuthorRead.UseVisualStyleBackColor = true;
            // 
            // checkAuthorWrite
            // 
            this.checkAuthorWrite.AutoSize = true;
            this.checkAuthorWrite.Location = new System.Drawing.Point(603, 1339);
            this.checkAuthorWrite.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.checkAuthorWrite.Name = "checkAuthorWrite";
            this.checkAuthorWrite.Size = new System.Drawing.Size(89, 34);
            this.checkAuthorWrite.TabIndex = 22;
            this.checkAuthorWrite.Text = "Write";
            this.checkAuthorWrite.UseVisualStyleBackColor = true;
            // 
            // checkAuthorModify
            // 
            this.checkAuthorModify.AutoSize = true;
            this.checkAuthorModify.Location = new System.Drawing.Point(603, 1389);
            this.checkAuthorModify.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.checkAuthorModify.Name = "checkAuthorModify";
            this.checkAuthorModify.Size = new System.Drawing.Size(104, 34);
            this.checkAuthorModify.TabIndex = 23;
            this.checkAuthorModify.Text = "Modify";
            this.checkAuthorModify.UseVisualStyleBackColor = true;
            // 
            // checkAuthorDelete
            // 
            this.checkAuthorDelete.AutoSize = true;
            this.checkAuthorDelete.Location = new System.Drawing.Point(603, 1439);
            this.checkAuthorDelete.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.checkAuthorDelete.Name = "checkAuthorDelete";
            this.checkAuthorDelete.Size = new System.Drawing.Size(99, 34);
            this.checkAuthorDelete.TabIndex = 24;
            this.checkAuthorDelete.Text = "Delete";
            this.checkAuthorDelete.UseVisualStyleBackColor = true;
            // 
            // listboxParticipantTables
            // 
            this.listboxParticipantTables.FormattingEnabled = true;
            this.listboxParticipantTables.ItemHeight = 30;
            this.listboxParticipantTables.Location = new System.Drawing.Point(38, 724);
            this.listboxParticipantTables.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.listboxParticipantTables.Name = "listboxParticipantTables";
            this.listboxParticipantTables.Size = new System.Drawing.Size(535, 334);
            this.listboxParticipantTables.TabIndex = 25;
            this.listboxParticipantTables.SelectedIndexChanged += new System.EventHandler(this.listboxParticipantTables_SelectedIndexChanged);
            // 
            // listboxAuthorTables
            // 
            this.listboxAuthorTables.FormattingEnabled = true;
            this.listboxAuthorTables.ItemHeight = 30;
            this.listboxAuthorTables.Location = new System.Drawing.Point(38, 1285);
            this.listboxAuthorTables.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.listboxAuthorTables.Name = "listboxAuthorTables";
            this.listboxAuthorTables.Size = new System.Drawing.Size(535, 394);
            this.listboxAuthorTables.TabIndex = 26;
            this.listboxAuthorTables.SelectedIndexChanged += new System.EventHandler(this.listboxAuthorTables_SelectedIndexChanged);
            // 
            // formManageContract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1399, 1703);
            this.Controls.Add(this.buttonSaveAuthorRights);
            this.Controls.Add(this.checkAuthorRead);
            this.Controls.Add(this.checkAuthorWrite);
            this.Controls.Add(this.listboxAuthorTables);
            this.Controls.Add(this.checkAuthorDelete);
            this.Controls.Add(this.buttonSaveParticipantRights);
            this.Controls.Add(this.checkAuthorModify);
            this.Controls.Add(this.checkParticipantRead);
            this.Controls.Add(this.listboxParticipantTables);
            this.Controls.Add(this.checkParticipantModify);
            this.Controls.Add(this.checkParticipantDelete);
            this.Controls.Add(this.checkParticipantWrite);
            this.Controls.Add(this.buttonSaveContract);
            this.Controls.Add(this.labelSchema);
            this.Controls.Add(this.textboxAuthorDataOverview);
            this.Controls.Add(this.labelAuthorDataOverview);
            this.Controls.Add(this.textboxParticipantDataOverview);
            this.Controls.Add(this.labelParticipantDataOverview);
            this.Controls.Add(this.textboxSchema);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.textboxDescription);
            this.Controls.Add(this.labelDatabaseName);
            this.Controls.Add(this.labelDatabase);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "formManageContract";
            this.Text = "formManageContract";
            this.Load += new System.EventHandler(this.formManageContract_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDatabase;
        private System.Windows.Forms.Label labelDatabaseName;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textboxDescription;
        private System.Windows.Forms.Label labelSchema;
        private System.Windows.Forms.TextBox textboxSchema;
        private System.Windows.Forms.Label labelParticipantDataOverview;
        private System.Windows.Forms.TextBox textboxParticipantDataOverview;
        private System.Windows.Forms.Label labelAuthorDataOverview;
        private System.Windows.Forms.TextBox textboxAuthorDataOverview;
        private System.Windows.Forms.Button buttonSaveContract;
        private System.Windows.Forms.Button buttonSaveParticipantRights;
        private System.Windows.Forms.CheckBox checkParticipantRead;
        private System.Windows.Forms.CheckBox checkParticipantWrite;
        private System.Windows.Forms.CheckBox checkParticipantModify;
        private System.Windows.Forms.CheckBox checkParticipantDelete;
        private System.Windows.Forms.Button buttonSaveAuthorRights;
        private System.Windows.Forms.CheckBox checkAuthorRead;
        private System.Windows.Forms.CheckBox checkAuthorWrite;
        private System.Windows.Forms.CheckBox checkAuthorModify;
        private System.Windows.Forms.CheckBox checkAuthorDelete;
        private System.Windows.Forms.ListBox listboxParticipantTables;
        private System.Windows.Forms.ListBox listboxAuthorTables;
    }
}