namespace PreReplicationSettings
{
    partial class ReplicationWizard
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
            this.btnMaster = new System.Windows.Forms.Button();
            this.btnLogCreation = new System.Windows.Forms.Button();
            this.rbBranch = new System.Windows.Forms.RadioButton();
            this.rbHeadoffice = new System.Windows.Forms.RadioButton();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.grpSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMaster
            // 
            this.btnMaster.Location = new System.Drawing.Point(10, 38);
            this.btnMaster.Name = "btnMaster";
            this.btnMaster.Size = new System.Drawing.Size(171, 23);
            this.btnMaster.TabIndex = 2;
            this.btnMaster.TabStop = false;
            this.btnMaster.Text = "Branch Mapping";
            this.btnMaster.UseVisualStyleBackColor = true;
            this.btnMaster.Click += new System.EventHandler(this.btnMaster_Click);
            // 
            // btnLogCreation
            // 
            this.btnLogCreation.AutoEllipsis = true;
            this.btnLogCreation.Location = new System.Drawing.Point(10, 85);
            this.btnLogCreation.Name = "btnLogCreation";
            this.btnLogCreation.Size = new System.Drawing.Size(171, 23);
            this.btnLogCreation.TabIndex = 3;
            this.btnLogCreation.Text = "Log Creation";
            this.btnLogCreation.UseVisualStyleBackColor = true;
            this.btnLogCreation.Click += new System.EventHandler(this.btnLogCreation_Click);
            // 
            // rbBranch
            // 
            this.rbBranch.AutoSize = true;
            this.rbBranch.Location = new System.Drawing.Point(12, 94);
            this.rbBranch.Name = "rbBranch";
            this.rbBranch.Size = new System.Drawing.Size(59, 17);
            this.rbBranch.TabIndex = 6;
            this.rbBranch.Text = "Branch";
            this.rbBranch.UseVisualStyleBackColor = true;
            this.rbBranch.CheckedChanged += new System.EventHandler(this.rbBranch_CheckedChanged);
            // 
            // rbHeadoffice
            // 
            this.rbHeadoffice.AutoSize = true;
            this.rbHeadoffice.Location = new System.Drawing.Point(10, 50);
            this.rbHeadoffice.Name = "rbHeadoffice";
            this.rbHeadoffice.Size = new System.Drawing.Size(106, 17);
            this.rbHeadoffice.TabIndex = 5;
            this.rbHeadoffice.Text = "Master Database";
            this.rbHeadoffice.UseVisualStyleBackColor = true;
            this.rbHeadoffice.CheckedChanged += new System.EventHandler(this.rbHeadoffice_CheckedChanged);
            // 
            // grpSettings
            // 
            this.grpSettings.Controls.Add(this.btnMaster);
            this.grpSettings.Controls.Add(this.btnLogCreation);
            this.grpSettings.Location = new System.Drawing.Point(117, 9);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(200, 141);
            this.grpSettings.TabIndex = 7;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "Replication Tool";
            // 
            // ReplicationWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 163);
            this.Controls.Add(this.rbBranch);
            this.Controls.Add(this.rbHeadoffice);
            this.Controls.Add(this.grpSettings);
            this.MaximizeBox = false;
            this.Name = "ReplicationWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Replication Wizard";
            this.Load += new System.EventHandler(this.ReplicationWizard_Load);
            this.grpSettings.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMaster;
        private System.Windows.Forms.Button btnLogCreation;
        private System.Windows.Forms.RadioButton rbBranch;
        private System.Windows.Forms.RadioButton rbHeadoffice;
        private System.Windows.Forms.GroupBox grpSettings;
    }
}

