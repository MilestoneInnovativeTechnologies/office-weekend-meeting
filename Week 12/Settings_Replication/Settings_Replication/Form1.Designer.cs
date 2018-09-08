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
            this.lblServer = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.txtDb = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblUpdate = new System.Windows.Forms.Button();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lstBranch = new System.Windows.Forms.ListBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtbranchName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(248, 100);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(38, 13);
            this.lblServer.TabIndex = 1;
            this.lblServer.Text = "Server";
            //this.lblServer.Click += new System.EventHandler(this.lblServer_Click);
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(248, 131);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(53, 13);
            this.lblDatabase.TabIndex = 3;
            this.lblDatabase.Text = "Database";
            //this.lblDatabase.Click += new System.EventHandler(this.lblDatabase_Click);
            // 
            // txtDb
            // 
            this.txtDb.Location = new System.Drawing.Point(327, 128);
            this.txtDb.Name = "txtDb";
            this.txtDb.Size = new System.Drawing.Size(100, 20);
            this.txtDb.TabIndex = 4;
            this.txtDb.TextChanged += new System.EventHandler(this.txtDb_TextChanged);
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(248, 164);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 5;
            this.lblPort.Text = "Port";
            //this.lblPort.Click += new System.EventHandler(this.lblPort_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(327, 161);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 6;
            this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            // 
            // lblUpdate
            // 
            this.lblUpdate.Location = new System.Drawing.Point(239, 202);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(75, 23);
            this.lblUpdate.TabIndex = 7;
            this.lblUpdate.Text = "UPDATE";
            this.lblUpdate.UseVisualStyleBackColor = true;
            this.lblUpdate.Click += new System.EventHandler(this.lblUpdate_Click);
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(327, 97);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(100, 20);
            this.txtServer.TabIndex = 8;
            this.txtServer.TextChanged += new System.EventHandler(this.txtServer_TextChanged);
            // 
            // lstBranch
            // 
            this.lstBranch.FormattingEnabled = true;
            this.lstBranch.Location = new System.Drawing.Point(71, 52);
            this.lstBranch.Name = "lstBranch";
            this.lstBranch.Size = new System.Drawing.Size(116, 173);
            this.lstBranch.TabIndex = 9;
            this.lstBranch.Click += new System.EventHandler(this.lstBranch_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(352, 202);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtbranchName
            // 
            this.txtbranchName.Location = new System.Drawing.Point(221, 52);
            this.txtbranchName.Multiline = true;
            this.txtbranchName.Name = "txtbranchName";
            this.txtbranchName.ReadOnly = true;
            this.txtbranchName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtbranchName.ShortcutsEnabled = false;
            this.txtbranchName.Size = new System.Drawing.Size(237, 20);
            this.txtbranchName.TabIndex = 11;
            // 
            // txtDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 312);
            this.Controls.Add(this.txtbranchName);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lstBranch);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.lblUpdate);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtDb);
            this.Controls.Add(this.lblDatabase);
            this.Controls.Add(this.lblServer);
            this.MaximizeBox = false;
            this.Name = "txtDatabase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.TextBox txtDb;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button lblUpdate;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.ListBox lstBranch;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtbranchName;
    }
}

