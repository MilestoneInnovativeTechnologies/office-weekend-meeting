namespace ePlusReplication
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgview = new System.Windows.Forms.DataGridView();
            this.Branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastReplication = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FetchedQuery = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VerifiedQuery = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppliedQuery = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NextReplication = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgview)).BeginInit();
            this.SuspendLayout();
            // 
            // dgview
            // 
            this.dgview.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Branch,
            this.LastReplication,
            this.FetchedQuery,
            this.VerifiedQuery,
            this.AppliedQuery,
            this.NextReplication,
            this.Status});
            this.dgview.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.dgview.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgview.GridColor = System.Drawing.Color.DarkGray;
            this.dgview.Location = new System.Drawing.Point(87, 6);
            this.dgview.MultiSelect = false;
            this.dgview.Name = "dgview";
            this.dgview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgview.ShowEditingIcon = false;
            this.dgview.Size = new System.Drawing.Size(845, 150);
            this.dgview.TabIndex = 0;
            this.dgview.UseWaitCursor = true;
            this.dgview.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgview_CellDoubleClick);
            this.dgview.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgview_DefaultValuesNeeded);
            this.dgview.BindingContextChanged += new System.EventHandler(this.Form1_Load);
            // 
            // Branch
            // 
            this.Branch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Branch.DataPropertyName = "Branch";
            this.Branch.HeaderText = "Branch";
            this.Branch.Name = "Branch";
            // 
            // LastReplication
            // 
            this.LastReplication.DataPropertyName = "LastReplication";
            dataGridViewCellStyle6.Format = "G";
            dataGridViewCellStyle6.NullValue = null;
            this.LastReplication.DefaultCellStyle = dataGridViewCellStyle6;
            this.LastReplication.HeaderText = "Last Replication";
            this.LastReplication.Name = "LastReplication";
            this.LastReplication.Width = 120;
            // 
            // FetchedQuery
            // 
            this.FetchedQuery.DataPropertyName = "FETCHEDQUERY";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.NullValue = null;
            this.FetchedQuery.DefaultCellStyle = dataGridViewCellStyle7;
            this.FetchedQuery.HeaderText = "Fetched Query";
            this.FetchedQuery.Name = "FetchedQuery";
            // 
            // VerifiedQuery
            // 
            this.VerifiedQuery.DataPropertyName = "VERIFIEDQUERY";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.NullValue = null;
            this.VerifiedQuery.DefaultCellStyle = dataGridViewCellStyle8;
            this.VerifiedQuery.HeaderText = "Pending for Verification";
            this.VerifiedQuery.Name = "VerifiedQuery";
            // 
            // AppliedQuery
            // 
            this.AppliedQuery.DataPropertyName = "APPLIEDQUERY";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.NullValue = null;
            this.AppliedQuery.DefaultCellStyle = dataGridViewCellStyle9;
            this.AppliedQuery.HeaderText = "Pending for Updation";
            this.AppliedQuery.Name = "AppliedQuery";
            // 
            // NextReplication
            // 
            this.NextReplication.DataPropertyName = "NextReplication";
            this.NextReplication.HeaderText = "NextReplication";
            this.NextReplication.Name = "NextReplication";
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            dataGridViewCellStyle10.NullValue = null;
            this.Status.DefaultCellStyle = dataGridViewCellStyle10;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.SystemColors.Window;
            this.txtStatus.Location = new System.Drawing.Point(87, 162);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(403, 321);
            this.txtStatus.TabIndex = 3;
            this.txtStatus.UseWaitCursor = true;
            // 
            // txtDetails
            // 
            this.txtDetails.BackColor = System.Drawing.SystemColors.Window;
            this.txtDetails.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.txtDetails.Location = new System.Drawing.Point(514, 162);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.ReadOnly = true;
            this.txtDetails.Size = new System.Drawing.Size(418, 321);
            this.txtDetails.TabIndex = 4;
            this.txtDetails.UseWaitCursor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAdd.Location = new System.Drawing.Point(463, 133);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Update";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.UseWaitCursor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1040, 487);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtDetails);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.dgview);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.Name = "Form1";
            this.Text = "Form1";
            this.UseWaitCursor = true;
            ((System.ComponentModel.ISupportInitialize)(this.dgview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgview;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtDetails;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Branch;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastReplication;
        private System.Windows.Forms.DataGridViewTextBoxColumn FetchedQuery;
        private System.Windows.Forms.DataGridViewTextBoxColumn VerifiedQuery;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppliedQuery;
        private System.Windows.Forms.DataGridViewTextBoxColumn NextReplication;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}