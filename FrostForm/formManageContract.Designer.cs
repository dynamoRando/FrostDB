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
            this.textboxParticipantDataOverviewRemarks = new System.Windows.Forms.TextBox();
            this.textboxAuthorDataOverviewRemarks = new System.Windows.Forms.TextBox();
            this.comboParticipantItems = new System.Windows.Forms.ComboBox();
            this.buttonSaveParticipantRights = new System.Windows.Forms.Button();
            this.checkParticipantRead = new System.Windows.Forms.CheckBox();
            this.checkParticipantWrite = new System.Windows.Forms.CheckBox();
            this.checkParticipantModify = new System.Windows.Forms.CheckBox();
            this.checkParticipantDelete = new System.Windows.Forms.CheckBox();
            this.comboAuthorItems = new System.Windows.Forms.ComboBox();
            this.buttonSaveAuthorRights = new System.Windows.Forms.Button();
            this.checkAuthorRead = new System.Windows.Forms.CheckBox();
            this.checkAuthorWrite = new System.Windows.Forms.CheckBox();
            this.checkAuthorModify = new System.Windows.Forms.CheckBox();
            this.checkAuthorDelete = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Location = new System.Drawing.Point(22, 27);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(58, 15);
            this.labelDatabase.TabIndex = 0;
            this.labelDatabase.Text = "Database:";
            // 
            // labelDatabaseName
            // 
            this.labelDatabaseName.AutoSize = true;
            this.labelDatabaseName.Location = new System.Drawing.Point(86, 27);
            this.labelDatabaseName.Name = "labelDatabaseName";
            this.labelDatabaseName.Size = new System.Drawing.Size(87, 15);
            this.labelDatabaseName.TabIndex = 1;
            this.labelDatabaseName.Text = "DatabaseName";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(22, 62);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(70, 15);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "Description:";
            // 
            // textboxDescription
            // 
            this.textboxDescription.Location = new System.Drawing.Point(22, 80);
            this.textboxDescription.Multiline = true;
            this.textboxDescription.Name = "textboxDescription";
            this.textboxDescription.Size = new System.Drawing.Size(766, 119);
            this.textboxDescription.TabIndex = 3;
            // 
            // labelSchema
            // 
            this.labelSchema.AutoSize = true;
            this.labelSchema.Location = new System.Drawing.Point(22, 218);
            this.labelSchema.Name = "labelSchema";
            this.labelSchema.Size = new System.Drawing.Size(52, 15);
            this.labelSchema.TabIndex = 4;
            this.labelSchema.Text = "Schema:";
            // 
            // textboxSchema
            // 
            this.textboxSchema.Location = new System.Drawing.Point(22, 236);
            this.textboxSchema.Multiline = true;
            this.textboxSchema.Name = "textboxSchema";
            this.textboxSchema.Size = new System.Drawing.Size(766, 139);
            this.textboxSchema.TabIndex = 5;
            // 
            // labelParticipantDataOverview
            // 
            this.labelParticipantDataOverview.AutoSize = true;
            this.labelParticipantDataOverview.Location = new System.Drawing.Point(22, 395);
            this.labelParticipantDataOverview.Name = "labelParticipantDataOverview";
            this.labelParticipantDataOverview.Size = new System.Drawing.Size(146, 15);
            this.labelParticipantDataOverview.TabIndex = 6;
            this.labelParticipantDataOverview.Text = "Participant Data Overview:";
            // 
            // textboxParticipantDataOverview
            // 
            this.textboxParticipantDataOverview.Location = new System.Drawing.Point(22, 413);
            this.textboxParticipantDataOverview.Multiline = true;
            this.textboxParticipantDataOverview.Name = "textboxParticipantDataOverview";
            this.textboxParticipantDataOverview.Size = new System.Drawing.Size(766, 102);
            this.textboxParticipantDataOverview.TabIndex = 7;
            // 
            // labelAuthorDataOverview
            // 
            this.labelAuthorDataOverview.AutoSize = true;
            this.labelAuthorDataOverview.Location = new System.Drawing.Point(22, 716);
            this.labelAuthorDataOverview.Name = "labelAuthorDataOverview";
            this.labelAuthorDataOverview.Size = new System.Drawing.Size(126, 15);
            this.labelAuthorDataOverview.TabIndex = 8;
            this.labelAuthorDataOverview.Text = "Author Data Overview:";
            // 
            // textboxAuthorDataOverview
            // 
            this.textboxAuthorDataOverview.Location = new System.Drawing.Point(22, 734);
            this.textboxAuthorDataOverview.Multiline = true;
            this.textboxAuthorDataOverview.Name = "textboxAuthorDataOverview";
            this.textboxAuthorDataOverview.Size = new System.Drawing.Size(766, 132);
            this.textboxAuthorDataOverview.TabIndex = 9;
            // 
            // buttonSaveContract
            // 
            this.buttonSaveContract.Location = new System.Drawing.Point(673, 1067);
            this.buttonSaveContract.Name = "buttonSaveContract";
            this.buttonSaveContract.Size = new System.Drawing.Size(115, 23);
            this.buttonSaveContract.TabIndex = 10;
            this.buttonSaveContract.Text = "Save Contract";
            this.buttonSaveContract.UseVisualStyleBackColor = true;
            // 
            // textboxParticipantDataOverviewRemarks
            // 
            this.textboxParticipantDataOverviewRemarks.Location = new System.Drawing.Point(22, 521);
            this.textboxParticipantDataOverviewRemarks.Multiline = true;
            this.textboxParticipantDataOverviewRemarks.Name = "textboxParticipantDataOverviewRemarks";
            this.textboxParticipantDataOverviewRemarks.Size = new System.Drawing.Size(397, 192);
            this.textboxParticipantDataOverviewRemarks.TabIndex = 11;
            // 
            // textboxAuthorDataOverviewRemarks
            // 
            this.textboxAuthorDataOverviewRemarks.Location = new System.Drawing.Point(22, 873);
            this.textboxAuthorDataOverviewRemarks.Multiline = true;
            this.textboxAuthorDataOverviewRemarks.Name = "textboxAuthorDataOverviewRemarks";
            this.textboxAuthorDataOverviewRemarks.Size = new System.Drawing.Size(397, 188);
            this.textboxAuthorDataOverviewRemarks.TabIndex = 12;
            // 
            // comboParticipantItems
            // 
            this.comboParticipantItems.FormattingEnabled = true;
            this.comboParticipantItems.Location = new System.Drawing.Point(435, 533);
            this.comboParticipantItems.Name = "comboParticipantItems";
            this.comboParticipantItems.Size = new System.Drawing.Size(353, 23);
            this.comboParticipantItems.TabIndex = 13;
            // 
            // buttonSaveParticipantRights
            // 
            this.buttonSaveParticipantRights.Location = new System.Drawing.Point(713, 562);
            this.buttonSaveParticipantRights.Name = "buttonSaveParticipantRights";
            this.buttonSaveParticipantRights.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveParticipantRights.TabIndex = 14;
            this.buttonSaveParticipantRights.Text = "Save";
            this.buttonSaveParticipantRights.UseVisualStyleBackColor = true;
            // 
            // checkParticipantRead
            // 
            this.checkParticipantRead.AutoSize = true;
            this.checkParticipantRead.Location = new System.Drawing.Point(435, 576);
            this.checkParticipantRead.Name = "checkParticipantRead";
            this.checkParticipantRead.Size = new System.Drawing.Size(52, 19);
            this.checkParticipantRead.TabIndex = 15;
            this.checkParticipantRead.Text = "Read";
            this.checkParticipantRead.UseVisualStyleBackColor = true;
            // 
            // checkParticipantWrite
            // 
            this.checkParticipantWrite.AutoSize = true;
            this.checkParticipantWrite.Location = new System.Drawing.Point(435, 600);
            this.checkParticipantWrite.Name = "checkParticipantWrite";
            this.checkParticipantWrite.Size = new System.Drawing.Size(54, 19);
            this.checkParticipantWrite.TabIndex = 16;
            this.checkParticipantWrite.Text = "Write";
            this.checkParticipantWrite.UseVisualStyleBackColor = true;
            // 
            // checkParticipantModify
            // 
            this.checkParticipantModify.AutoSize = true;
            this.checkParticipantModify.Location = new System.Drawing.Point(435, 625);
            this.checkParticipantModify.Name = "checkParticipantModify";
            this.checkParticipantModify.Size = new System.Drawing.Size(64, 19);
            this.checkParticipantModify.TabIndex = 17;
            this.checkParticipantModify.Text = "Modify";
            this.checkParticipantModify.UseVisualStyleBackColor = true;
            // 
            // checkParticipantDelete
            // 
            this.checkParticipantDelete.AutoSize = true;
            this.checkParticipantDelete.Location = new System.Drawing.Point(435, 650);
            this.checkParticipantDelete.Name = "checkParticipantDelete";
            this.checkParticipantDelete.Size = new System.Drawing.Size(59, 19);
            this.checkParticipantDelete.TabIndex = 18;
            this.checkParticipantDelete.Text = "Delete";
            this.checkParticipantDelete.UseVisualStyleBackColor = true;
            // 
            // comboAuthorItems
            // 
            this.comboAuthorItems.FormattingEnabled = true;
            this.comboAuthorItems.Location = new System.Drawing.Point(435, 887);
            this.comboAuthorItems.Name = "comboAuthorItems";
            this.comboAuthorItems.Size = new System.Drawing.Size(353, 23);
            this.comboAuthorItems.TabIndex = 19;
            // 
            // buttonSaveAuthorRights
            // 
            this.buttonSaveAuthorRights.Location = new System.Drawing.Point(713, 916);
            this.buttonSaveAuthorRights.Name = "buttonSaveAuthorRights";
            this.buttonSaveAuthorRights.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveAuthorRights.TabIndex = 20;
            this.buttonSaveAuthorRights.Text = "Save";
            this.buttonSaveAuthorRights.UseVisualStyleBackColor = true;
            // 
            // checkAuthorRead
            // 
            this.checkAuthorRead.AutoSize = true;
            this.checkAuthorRead.Location = new System.Drawing.Point(435, 939);
            this.checkAuthorRead.Name = "checkAuthorRead";
            this.checkAuthorRead.Size = new System.Drawing.Size(52, 19);
            this.checkAuthorRead.TabIndex = 21;
            this.checkAuthorRead.Text = "Read";
            this.checkAuthorRead.UseVisualStyleBackColor = true;
            // 
            // checkAuthorWrite
            // 
            this.checkAuthorWrite.AutoSize = true;
            this.checkAuthorWrite.Location = new System.Drawing.Point(435, 964);
            this.checkAuthorWrite.Name = "checkAuthorWrite";
            this.checkAuthorWrite.Size = new System.Drawing.Size(54, 19);
            this.checkAuthorWrite.TabIndex = 22;
            this.checkAuthorWrite.Text = "Write";
            this.checkAuthorWrite.UseVisualStyleBackColor = true;
            // 
            // checkAuthorModify
            // 
            this.checkAuthorModify.AutoSize = true;
            this.checkAuthorModify.Location = new System.Drawing.Point(435, 989);
            this.checkAuthorModify.Name = "checkAuthorModify";
            this.checkAuthorModify.Size = new System.Drawing.Size(64, 19);
            this.checkAuthorModify.TabIndex = 23;
            this.checkAuthorModify.Text = "Modify";
            this.checkAuthorModify.UseVisualStyleBackColor = true;
            // 
            // checkAuthorDelete
            // 
            this.checkAuthorDelete.AutoSize = true;
            this.checkAuthorDelete.Location = new System.Drawing.Point(435, 1014);
            this.checkAuthorDelete.Name = "checkAuthorDelete";
            this.checkAuthorDelete.Size = new System.Drawing.Size(59, 19);
            this.checkAuthorDelete.TabIndex = 24;
            this.checkAuthorDelete.Text = "Delete";
            this.checkAuthorDelete.UseVisualStyleBackColor = true;
            // 
            // formManageContract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 1100);
            this.Controls.Add(this.checkAuthorDelete);
            this.Controls.Add(this.checkAuthorModify);
            this.Controls.Add(this.checkAuthorWrite);
            this.Controls.Add(this.checkAuthorRead);
            this.Controls.Add(this.buttonSaveParticipantRights);
            this.Controls.Add(this.buttonSaveAuthorRights);
            this.Controls.Add(this.comboAuthorItems);
            this.Controls.Add(this.checkParticipantDelete);
            this.Controls.Add(this.checkParticipantModify);
            this.Controls.Add(this.checkParticipantWrite);
            this.Controls.Add(this.checkParticipantRead);
            this.Controls.Add(this.comboParticipantItems);
            this.Controls.Add(this.textboxAuthorDataOverviewRemarks);
            this.Controls.Add(this.textboxParticipantDataOverviewRemarks);
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
        private System.Windows.Forms.TextBox textboxParticipantDataOverviewRemarks;
        private System.Windows.Forms.TextBox textboxAuthorDataOverviewRemarks;
        private System.Windows.Forms.ComboBox comboParticipantItems;
        private System.Windows.Forms.Button buttonSaveParticipantRights;
        private System.Windows.Forms.CheckBox checkParticipantRead;
        private System.Windows.Forms.CheckBox checkParticipantWrite;
        private System.Windows.Forms.CheckBox checkParticipantModify;
        private System.Windows.Forms.CheckBox checkParticipantDelete;
        private System.Windows.Forms.ComboBox comboAuthorItems;
        private System.Windows.Forms.Button buttonSaveAuthorRights;
        private System.Windows.Forms.CheckBox checkAuthorRead;
        private System.Windows.Forms.CheckBox checkAuthorWrite;
        private System.Windows.Forms.CheckBox checkAuthorModify;
        private System.Windows.Forms.CheckBox checkAuthorDelete;
    }
}