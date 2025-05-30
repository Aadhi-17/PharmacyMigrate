namespace KRATOS.UserControls
{
    partial class KRATOS
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpbxSqlServer = new System.Windows.Forms.GroupBox();
            this.pnlChooseDatabase = new System.Windows.Forms.Panel();
            this.lblSelectedDatabase = new System.Windows.Forms.Label();
            this.btnStagingDatabase = new System.Windows.Forms.Button();
            this.lblSeperatorDatabase = new System.Windows.Forms.Label();
            this.pnlInputDatabase = new System.Windows.Forms.Panel();
            this.txtpnlInputDatabase = new System.Windows.Forms.TextBox();
            this.lblMandateDb = new System.Windows.Forms.Label();
            this.lblMandateServer = new System.Windows.Forms.Label();
            this.lblSeperatorServer = new System.Windows.Forms.Label();
            this.cmbxServer = new System.Windows.Forms.ComboBox();
            this.lblDatabaseName = new System.Windows.Forms.Label();
            this.lblServerName = new System.Windows.Forms.Label();
            this.pnlManualExecution = new System.Windows.Forms.Panel();
            this.chkShowErrors = new System.Windows.Forms.CheckBox();
            this.rctxtErrorLog = new System.Windows.Forms.RichTextBox();
            this.lblProgressPercentage = new System.Windows.Forms.Label();
            this.prgbxPharmacy = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.grpbxCosmosDb = new System.Windows.Forms.GroupBox();
            this.pnlEnvironment = new System.Windows.Forms.Panel();
            this.cmbxDestEnvrnment = new System.Windows.Forms.ComboBox();
            this.lblSeperatorEnvironment = new System.Windows.Forms.Label();
            this.lblMandateEnvironment = new System.Windows.Forms.Label();
            this.lblEnvironment = new System.Windows.Forms.Label();
            this.grpbxSqlServer.SuspendLayout();
            this.pnlChooseDatabase.SuspendLayout();
            this.pnlInputDatabase.SuspendLayout();
            this.pnlManualExecution.SuspendLayout();
            this.grpbxCosmosDb.SuspendLayout();
            this.pnlEnvironment.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpbxSqlServer
            // 
            this.grpbxSqlServer.BackColor = System.Drawing.Color.White;
            this.grpbxSqlServer.Controls.Add(this.pnlChooseDatabase);
            this.grpbxSqlServer.Controls.Add(this.lblSeperatorDatabase);
            this.grpbxSqlServer.Controls.Add(this.pnlInputDatabase);
            this.grpbxSqlServer.Controls.Add(this.lblMandateDb);
            this.grpbxSqlServer.Controls.Add(this.lblMandateServer);
            this.grpbxSqlServer.Controls.Add(this.lblSeperatorServer);
            this.grpbxSqlServer.Controls.Add(this.cmbxServer);
            this.grpbxSqlServer.Controls.Add(this.lblDatabaseName);
            this.grpbxSqlServer.Controls.Add(this.lblServerName);
            this.grpbxSqlServer.Location = new System.Drawing.Point(23, 31);
            this.grpbxSqlServer.Margin = new System.Windows.Forms.Padding(4);
            this.grpbxSqlServer.Name = "grpbxSqlServer";
            this.grpbxSqlServer.Padding = new System.Windows.Forms.Padding(4);
            this.grpbxSqlServer.Size = new System.Drawing.Size(1102, 128);
            this.grpbxSqlServer.TabIndex = 1;
            this.grpbxSqlServer.TabStop = false;
            this.grpbxSqlServer.Text = "MSSql Server";
            // 
            // pnlChooseDatabase
            // 
            this.pnlChooseDatabase.Controls.Add(this.lblSelectedDatabase);
            this.pnlChooseDatabase.Controls.Add(this.btnStagingDatabase);
            this.pnlChooseDatabase.Location = new System.Drawing.Point(167, 73);
            this.pnlChooseDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.pnlChooseDatabase.Name = "pnlChooseDatabase";
            this.pnlChooseDatabase.Size = new System.Drawing.Size(561, 32);
            this.pnlChooseDatabase.TabIndex = 79;
            // 
            // lblSelectedDatabase
            // 
            this.lblSelectedDatabase.AutoEllipsis = true;
            this.lblSelectedDatabase.BackColor = System.Drawing.Color.White;
            this.lblSelectedDatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSelectedDatabase.Location = new System.Drawing.Point(5, 4);
            this.lblSelectedDatabase.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedDatabase.MaximumSize = new System.Drawing.Size(599, 24);
            this.lblSelectedDatabase.MinimumSize = new System.Drawing.Size(293, 24);
            this.lblSelectedDatabase.Name = "lblSelectedDatabase";
            this.lblSelectedDatabase.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblSelectedDatabase.Size = new System.Drawing.Size(513, 24);
            this.lblSelectedDatabase.TabIndex = 312;
            // 
            // btnStagingDatabase
            // 
            this.btnStagingDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(64)))), ((int)(((byte)(107)))));
            this.btnStagingDatabase.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStagingDatabase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStagingDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStagingDatabase.ForeColor = System.Drawing.Color.White;
            this.btnStagingDatabase.Image = global::KRATOS.Properties.Resources.database_white;
            this.btnStagingDatabase.Location = new System.Drawing.Point(520, 1);
            this.btnStagingDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.btnStagingDatabase.Name = "btnStagingDatabase";
            this.btnStagingDatabase.Size = new System.Drawing.Size(37, 27);
            this.btnStagingDatabase.TabIndex = 311;
            this.btnStagingDatabase.UseVisualStyleBackColor = false;
            this.btnStagingDatabase.Click += new System.EventHandler(this.btnStagingDatabase_Click);
            // 
            // lblSeperatorDatabase
            // 
            this.lblSeperatorDatabase.AutoSize = true;
            this.lblSeperatorDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(64)))), ((int)(((byte)(107)))));
            this.lblSeperatorDatabase.Location = new System.Drawing.Point(153, 80);
            this.lblSeperatorDatabase.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSeperatorDatabase.Name = "lblSeperatorDatabase";
            this.lblSeperatorDatabase.Size = new System.Drawing.Size(10, 16);
            this.lblSeperatorDatabase.TabIndex = 63;
            this.lblSeperatorDatabase.Text = ":";
            // 
            // pnlInputDatabase
            // 
            this.pnlInputDatabase.Controls.Add(this.txtpnlInputDatabase);
            this.pnlInputDatabase.Location = new System.Drawing.Point(167, 71);
            this.pnlInputDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.pnlInputDatabase.Name = "pnlInputDatabase";
            this.pnlInputDatabase.Size = new System.Drawing.Size(561, 32);
            this.pnlInputDatabase.TabIndex = 80;
            // 
            // txtpnlInputDatabase
            // 
            this.txtpnlInputDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtpnlInputDatabase.Location = new System.Drawing.Point(4, 5);
            this.txtpnlInputDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.txtpnlInputDatabase.Name = "txtpnlInputDatabase";
            this.txtpnlInputDatabase.Size = new System.Drawing.Size(552, 22);
            this.txtpnlInputDatabase.TabIndex = 3;
            // 
            // lblMandateDb
            // 
            this.lblMandateDb.AutoSize = true;
            this.lblMandateDb.ForeColor = System.Drawing.Color.Red;
            this.lblMandateDb.Location = new System.Drawing.Point(140, 82);
            this.lblMandateDb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMandateDb.Name = "lblMandateDb";
            this.lblMandateDb.Size = new System.Drawing.Size(12, 16);
            this.lblMandateDb.TabIndex = 62;
            this.lblMandateDb.Text = "*";
            // 
            // lblMandateServer
            // 
            this.lblMandateServer.AutoSize = true;
            this.lblMandateServer.ForeColor = System.Drawing.Color.Red;
            this.lblMandateServer.Location = new System.Drawing.Point(72, 32);
            this.lblMandateServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMandateServer.Name = "lblMandateServer";
            this.lblMandateServer.Size = new System.Drawing.Size(12, 16);
            this.lblMandateServer.TabIndex = 61;
            this.lblMandateServer.Text = "*";
            // 
            // lblSeperatorServer
            // 
            this.lblSeperatorServer.AutoSize = true;
            this.lblSeperatorServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(64)))), ((int)(((byte)(107)))));
            this.lblSeperatorServer.Location = new System.Drawing.Point(153, 31);
            this.lblSeperatorServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSeperatorServer.Name = "lblSeperatorServer";
            this.lblSeperatorServer.Size = new System.Drawing.Size(10, 16);
            this.lblSeperatorServer.TabIndex = 60;
            this.lblSeperatorServer.Text = ":";
            // 
            // cmbxServer
            // 
            this.cmbxServer.BackColor = System.Drawing.SystemColors.Window;
            this.cmbxServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbxServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxServer.FormattingEnabled = true;
            this.cmbxServer.Items.AddRange(new object[] {
            "Raven",
            "Skylark",
            "Aquila",
            "Firefly",
            "hurricane",
            "Blackbird",
            "Griffin",
            "Localhost"});
            this.cmbxServer.Location = new System.Drawing.Point(172, 27);
            this.cmbxServer.Margin = new System.Windows.Forms.Padding(4);
            this.cmbxServer.Name = "cmbxServer";
            this.cmbxServer.Size = new System.Drawing.Size(551, 24);
            this.cmbxServer.TabIndex = 58;
            // 
            // lblDatabaseName
            // 
            this.lblDatabaseName.AutoSize = true;
            this.lblDatabaseName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(64)))), ((int)(((byte)(107)))));
            this.lblDatabaseName.Location = new System.Drawing.Point(23, 81);
            this.lblDatabaseName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDatabaseName.Name = "lblDatabaseName";
            this.lblDatabaseName.Size = new System.Drawing.Size(116, 16);
            this.lblDatabaseName.TabIndex = 57;
            this.lblDatabaseName.Text = "Staging Database";
            // 
            // lblServerName
            // 
            this.lblServerName.AutoSize = true;
            this.lblServerName.BackColor = System.Drawing.Color.White;
            this.lblServerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(64)))), ((int)(((byte)(107)))));
            this.lblServerName.Location = new System.Drawing.Point(27, 31);
            this.lblServerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(47, 16);
            this.lblServerName.TabIndex = 56;
            this.lblServerName.Text = "Server";
            // 
            // pnlManualExecution
            // 
            this.pnlManualExecution.BackColor = System.Drawing.Color.White;
            this.pnlManualExecution.Controls.Add(this.chkShowErrors);
            this.pnlManualExecution.Controls.Add(this.rctxtErrorLog);
            this.pnlManualExecution.Controls.Add(this.lblProgressPercentage);
            this.pnlManualExecution.Controls.Add(this.prgbxPharmacy);
            this.pnlManualExecution.Controls.Add(this.btnCancel);
            this.pnlManualExecution.Controls.Add(this.lblStatus);
            this.pnlManualExecution.Controls.Add(this.btnUpload);
            this.pnlManualExecution.Location = new System.Drawing.Point(23, 259);
            this.pnlManualExecution.Margin = new System.Windows.Forms.Padding(4);
            this.pnlManualExecution.Name = "pnlManualExecution";
            this.pnlManualExecution.Size = new System.Drawing.Size(1120, 253);
            this.pnlManualExecution.TabIndex = 118;
            // 
            // chkShowErrors
            // 
            this.chkShowErrors.AutoSize = true;
            this.chkShowErrors.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkShowErrors.Location = new System.Drawing.Point(75, 51);
            this.chkShowErrors.Margin = new System.Windows.Forms.Padding(4);
            this.chkShowErrors.Name = "chkShowErrors";
            this.chkShowErrors.Size = new System.Drawing.Size(208, 20);
            this.chkShowErrors.TabIndex = 230;
            this.chkShowErrors.Text = "Show Error(s)/Warning(s) Only";
            this.chkShowErrors.UseVisualStyleBackColor = true;
            this.chkShowErrors.CheckedChanged += new System.EventHandler(this.chkShowErrors_CheckedChanged);
            // 
            // rctxtErrorLog
            // 
            this.rctxtErrorLog.Location = new System.Drawing.Point(75, 78);
            this.rctxtErrorLog.Name = "rctxtErrorLog";
            this.rctxtErrorLog.ReadOnly = true;
            this.rctxtErrorLog.Size = new System.Drawing.Size(1026, 160);
            this.rctxtErrorLog.TabIndex = 116;
            this.rctxtErrorLog.Text = "";
            // 
            // lblProgressPercentage
            // 
            this.lblProgressPercentage.AutoSize = true;
            this.lblProgressPercentage.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblProgressPercentage.Font = new System.Drawing.Font("Lucida Sans", 8F, System.Drawing.FontStyle.Bold);
            this.lblProgressPercentage.Location = new System.Drawing.Point(341, 22);
            this.lblProgressPercentage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProgressPercentage.Name = "lblProgressPercentage";
            this.lblProgressPercentage.Size = new System.Drawing.Size(27, 16);
            this.lblProgressPercentage.TabIndex = 115;
            this.lblProgressPercentage.Text = "0%";
            // 
            // prgbxPharmacy
            // 
            this.prgbxPharmacy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgbxPharmacy.Location = new System.Drawing.Point(9, 15);
            this.prgbxPharmacy.Margin = new System.Windows.Forms.Padding(4);
            this.prgbxPharmacy.Name = "prgbxPharmacy";
            this.prgbxPharmacy.Size = new System.Drawing.Size(708, 28);
            this.prgbxPharmacy.TabIndex = 114;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.Location = new System.Drawing.Point(915, 15);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(187, 28);
            this.btnCancel.TabIndex = 113;
            this.btnCancel.Text = "Reset";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(64)))), ((int)(((byte)(107)))));
            this.lblStatus.Location = new System.Drawing.Point(9, 70);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 16);
            this.lblStatus.TabIndex = 110;
            this.lblStatus.Text = "Status:";
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(64)))), ((int)(((byte)(107)))));
            this.btnUpload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpload.ForeColor = System.Drawing.Color.White;
            this.btnUpload.Location = new System.Drawing.Point(723, 15);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(187, 28);
            this.btnUpload.TabIndex = 112;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // grpbxCosmosDb
            // 
            this.grpbxCosmosDb.BackColor = System.Drawing.Color.White;
            this.grpbxCosmosDb.Controls.Add(this.pnlEnvironment);
            this.grpbxCosmosDb.Controls.Add(this.lblSeperatorEnvironment);
            this.grpbxCosmosDb.Controls.Add(this.lblMandateEnvironment);
            this.grpbxCosmosDb.Controls.Add(this.lblEnvironment);
            this.grpbxCosmosDb.Location = new System.Drawing.Point(23, 176);
            this.grpbxCosmosDb.Margin = new System.Windows.Forms.Padding(4);
            this.grpbxCosmosDb.Name = "grpbxCosmosDb";
            this.grpbxCosmosDb.Padding = new System.Windows.Forms.Padding(4);
            this.grpbxCosmosDb.Size = new System.Drawing.Size(1101, 64);
            this.grpbxCosmosDb.TabIndex = 119;
            this.grpbxCosmosDb.TabStop = false;
            this.grpbxCosmosDb.Text = "MySql Config";
            // 
            // pnlEnvironment
            // 
            this.pnlEnvironment.Controls.Add(this.cmbxDestEnvrnment);
            this.pnlEnvironment.Location = new System.Drawing.Point(168, 12);
            this.pnlEnvironment.Margin = new System.Windows.Forms.Padding(4);
            this.pnlEnvironment.Name = "pnlEnvironment";
            this.pnlEnvironment.Size = new System.Drawing.Size(897, 44);
            this.pnlEnvironment.TabIndex = 85;
            // 
            // cmbxDestEnvrnment
            // 
            this.cmbxDestEnvrnment.BackColor = System.Drawing.SystemColors.Window;
            this.cmbxDestEnvrnment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbxDestEnvrnment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxDestEnvrnment.FormattingEnabled = true;
            this.cmbxDestEnvrnment.Items.AddRange(new object[] {
            "SandBox",
            "Live"});
            this.cmbxDestEnvrnment.Location = new System.Drawing.Point(5, 10);
            this.cmbxDestEnvrnment.Margin = new System.Windows.Forms.Padding(4);
            this.cmbxDestEnvrnment.Name = "cmbxDestEnvrnment";
            this.cmbxDestEnvrnment.Size = new System.Drawing.Size(264, 24);
            this.cmbxDestEnvrnment.TabIndex = 65;
            // 
            // lblSeperatorEnvironment
            // 
            this.lblSeperatorEnvironment.AutoSize = true;
            this.lblSeperatorEnvironment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(64)))), ((int)(((byte)(107)))));
            this.lblSeperatorEnvironment.Location = new System.Drawing.Point(153, 26);
            this.lblSeperatorEnvironment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSeperatorEnvironment.Name = "lblSeperatorEnvironment";
            this.lblSeperatorEnvironment.Size = new System.Drawing.Size(10, 16);
            this.lblSeperatorEnvironment.TabIndex = 82;
            this.lblSeperatorEnvironment.Text = ":";
            // 
            // lblMandateEnvironment
            // 
            this.lblMandateEnvironment.AutoSize = true;
            this.lblMandateEnvironment.ForeColor = System.Drawing.Color.Red;
            this.lblMandateEnvironment.Location = new System.Drawing.Point(105, 26);
            this.lblMandateEnvironment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMandateEnvironment.Name = "lblMandateEnvironment";
            this.lblMandateEnvironment.Size = new System.Drawing.Size(12, 16);
            this.lblMandateEnvironment.TabIndex = 81;
            this.lblMandateEnvironment.Text = "*";
            // 
            // lblEnvironment
            // 
            this.lblEnvironment.AutoSize = true;
            this.lblEnvironment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(64)))), ((int)(((byte)(107)))));
            this.lblEnvironment.Location = new System.Drawing.Point(20, 26);
            this.lblEnvironment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnvironment.Name = "lblEnvironment";
            this.lblEnvironment.Size = new System.Drawing.Size(81, 16);
            this.lblEnvironment.TabIndex = 0;
            this.lblEnvironment.Text = "Environment";
            // 
            // KRATOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.grpbxCosmosDb);
            this.Controls.Add(this.pnlManualExecution);
            this.Controls.Add(this.grpbxSqlServer);
            this.Name = "KRATOS";
            this.Size = new System.Drawing.Size(1187, 531);
            this.grpbxSqlServer.ResumeLayout(false);
            this.grpbxSqlServer.PerformLayout();
            this.pnlChooseDatabase.ResumeLayout(false);
            this.pnlInputDatabase.ResumeLayout(false);
            this.pnlInputDatabase.PerformLayout();
            this.pnlManualExecution.ResumeLayout(false);
            this.pnlManualExecution.PerformLayout();
            this.grpbxCosmosDb.ResumeLayout(false);
            this.grpbxCosmosDb.PerformLayout();
            this.pnlEnvironment.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpbxSqlServer;
        private System.Windows.Forms.Panel pnlChooseDatabase;
        private System.Windows.Forms.Label lblSelectedDatabase;
        private System.Windows.Forms.Button btnStagingDatabase;
        private System.Windows.Forms.Label lblSeperatorDatabase;
        private System.Windows.Forms.Panel pnlInputDatabase;
        private System.Windows.Forms.TextBox txtpnlInputDatabase;
        private System.Windows.Forms.Label lblMandateDb;
        private System.Windows.Forms.Label lblMandateServer;
        private System.Windows.Forms.Label lblSeperatorServer;
        private System.Windows.Forms.ComboBox cmbxServer;
        private System.Windows.Forms.Label lblDatabaseName;
        private System.Windows.Forms.Label lblServerName;
        private System.Windows.Forms.Panel pnlManualExecution;
        internal System.Windows.Forms.Label lblProgressPercentage;
        private System.Windows.Forms.ProgressBar prgbxPharmacy;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.GroupBox grpbxCosmosDb;
        private System.Windows.Forms.Panel pnlEnvironment;
        private System.Windows.Forms.ComboBox cmbxDestEnvrnment;
        private System.Windows.Forms.Label lblSeperatorEnvironment;
        private System.Windows.Forms.Label lblMandateEnvironment;
        private System.Windows.Forms.Label lblEnvironment;
        private System.Windows.Forms.RichTextBox rctxtErrorLog;
        private System.Windows.Forms.CheckBox chkShowErrors;
    }
}
