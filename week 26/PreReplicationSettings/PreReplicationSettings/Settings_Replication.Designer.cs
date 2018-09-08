namespace Settings_Replication
{
    partial class txtDatabase
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
            this.lblUpdate = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lstBranch = new System.Windows.Forms.ListBox();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.txtbranchName = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtDb = new System.Windows.Forms.TextBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.sptCMapping = new System.Windows.Forms.SplitContainer();
            this.grpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sptCMapping)).BeginInit();
            this.sptCMapping.Panel1.SuspendLayout();
            this.sptCMapping.Panel2.SuspendLayout();
            this.sptCMapping.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUpdate
            // 
            this.lblUpdate.AutoEllipsis = true;
            this.lblUpdate.Location = new System.Drawing.Point(326, 4);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(77, 23);
            this.lblUpdate.TabIndex = 10;
            this.lblUpdate.Text = "Update";
            this.lblUpdate.UseVisualStyleBackColor = true;
            this.lblUpdate.Click += new System.EventHandler(this.lblUpdate_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(239, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "< Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lstBranch
            // 
            this.lstBranch.FormattingEnabled = true;
            this.lstBranch.Location = new System.Drawing.Point(19, 42);
            this.lstBranch.Name = "lstBranch";
            this.lstBranch.Size = new System.Drawing.Size(96, 121);
            this.lstBranch.TabIndex = 0;
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.txtbranchName);
            this.grpDetails.Controls.Add(this.txtServer);
            this.grpDetails.Controls.Add(this.txtPort);
            this.grpDetails.Controls.Add(this.lblPort);
            this.grpDetails.Controls.Add(this.txtDb);
            this.grpDetails.Controls.Add(this.lblDatabase);
            this.grpDetails.Controls.Add(this.lblServer);
            this.grpDetails.Location = new System.Drawing.Point(125, 11);
            this.grpDetails.Margin = new System.Windows.Forms.Padding(1);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(287, 183);
            this.grpDetails.TabIndex = 1;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Branch Details";
            // 
            // txtbranchName
            // 
            this.txtbranchName.Location = new System.Drawing.Point(33, 42);
            this.txtbranchName.Multiline = true;
            this.txtbranchName.Name = "txtbranchName";
            this.txtbranchName.ReadOnly = true;
            this.txtbranchName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtbranchName.ShortcutsEnabled = false;
            this.txtbranchName.Size = new System.Drawing.Size(237, 20);
            this.txtbranchName.TabIndex = 2;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(139, 81);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(100, 20);
            this.txtServer.TabIndex = 4;
            this.txtServer.TextChanged += new System.EventHandler(this.txtServer_TextChanged);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(139, 145);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 8;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(60, 148);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 7;
            this.lblPort.Text = "Port";
            // 
            // txtDb
            // 
            this.txtDb.Location = new System.Drawing.Point(139, 112);
            this.txtDb.Name = "txtDb";
            this.txtDb.Size = new System.Drawing.Size(100, 20);
            this.txtDb.TabIndex = 6;
            this.txtDb.TextChanged += new System.EventHandler(this.txtDb_TextChanged_1);
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(60, 115);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(53, 13);
            this.lblDatabase.TabIndex = 5;
            this.lblDatabase.Text = "Database";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(60, 84);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(38, 13);
            this.lblServer.TabIndex = 3;
            this.lblServer.Text = "Server";
            // 
            // sptCMapping
            // 
            this.sptCMapping.Location = new System.Drawing.Point(5, 10);
            this.sptCMapping.Name = "sptCMapping";
            this.sptCMapping.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sptCMapping.Panel1
            // 
            this.sptCMapping.Panel1.Controls.Add(this.grpDetails);
            this.sptCMapping.Panel1.Controls.Add(this.lstBranch);
            // 
            // sptCMapping.Panel2
            // 
            this.sptCMapping.Panel2.Controls.Add(this.btnBack);
            this.sptCMapping.Panel2.Controls.Add(this.lblUpdate);
            this.sptCMapping.Panel2.Margin = new System.Windows.Forms.Padding(1);
            this.sptCMapping.Size = new System.Drawing.Size(424, 232);
            this.sptCMapping.SplitterDistance = 200;
            this.sptCMapping.TabIndex = 22;
            // 
            // txtDatabase
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 246);
            this.Controls.Add(this.sptCMapping);
            this.MaximizeBox = false;
            this.Name = "txtDatabase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mapping";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            //this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.sptCMapping.Panel1.ResumeLayout(false);
            this.sptCMapping.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sptCMapping)).EndInit();
            this.sptCMapping.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button lblUpdate;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ListBox lstBranch;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.TextBox txtbranchName;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtDb;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.SplitContainer sptCMapping;
    }
}

