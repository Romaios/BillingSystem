namespace BillingSystem
{
    partial class frmManagePermissions
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
            lblTitle = new Label();
            lblRole = new Label();
            cmbRole = new ComboBox();
            chkAddCustomer = new CheckBox();
            chkEditCustomer = new CheckBox();
            chkDeleteCustomer = new CheckBox();
            chkAnalytics = new CheckBox();
            chkExportExcel = new CheckBox();
            chkExportPdf = new CheckBox();
            chkAuditLogs = new CheckBox();
            btnSave = new Button();
            btnClose = new Button();
            chkChangePassword = new CheckBox();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F);
            lblTitle.Location = new Point(37, 39);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(189, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Manage Permissions";
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Location = new Point(48, 85);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(86, 20);
            lblRole.TabIndex = 1;
            lblRole.Text = "Select Role:";
            // 
            // cmbRole
            // 
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(141, 77);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(151, 28);
            cmbRole.TabIndex = 2;
            cmbRole.SelectedIndexChanged += cmbRole_SelectedIndexChanged;
            // 
            // chkAddCustomer
            // 
            chkAddCustomer.AutoSize = true;
            chkAddCustomer.Location = new Point(140, 130);
            chkAddCustomer.Name = "chkAddCustomer";
            chkAddCustomer.Size = new Size(126, 24);
            chkAddCustomer.TabIndex = 9;
            chkAddCustomer.Text = "Add Customer";
            chkAddCustomer.UseVisualStyleBackColor = true;
            // 
            // chkEditCustomer
            // 
            chkEditCustomer.AutoSize = true;
            chkEditCustomer.Location = new Point(140, 160);
            chkEditCustomer.Name = "chkEditCustomer";
            chkEditCustomer.Size = new Size(124, 24);
            chkEditCustomer.TabIndex = 10;
            chkEditCustomer.Text = "Edit Customer";
            chkEditCustomer.UseVisualStyleBackColor = true;
            // 
            // chkDeleteCustomer
            // 
            chkDeleteCustomer.AutoSize = true;
            chkDeleteCustomer.Location = new Point(140, 190);
            chkDeleteCustomer.Name = "chkDeleteCustomer";
            chkDeleteCustomer.Size = new Size(142, 24);
            chkDeleteCustomer.TabIndex = 11;
            chkDeleteCustomer.Text = "Delete Customer";
            chkDeleteCustomer.UseVisualStyleBackColor = true;
            // 
            // chkAnalytics
            // 
            chkAnalytics.AutoSize = true;
            chkAnalytics.Location = new Point(140, 220);
            chkAnalytics.Name = "chkAnalytics";
            chkAnalytics.Size = new Size(90, 24);
            chkAnalytics.TabIndex = 12;
            chkAnalytics.Text = "Analytics";
            chkAnalytics.UseVisualStyleBackColor = true;
            // 
            // chkExportExcel
            // 
            chkExportExcel.AutoSize = true;
            chkExportExcel.Location = new Point(140, 250);
            chkExportExcel.Name = "chkExportExcel";
            chkExportExcel.Size = new Size(130, 24);
            chkExportExcel.TabIndex = 13;
            chkExportExcel.Text = "Export to Excel";
            chkExportExcel.UseVisualStyleBackColor = true;
            // 
            // chkExportPdf
            // 
            chkExportPdf.AutoSize = true;
            chkExportPdf.Location = new Point(140, 280);
            chkExportPdf.Name = "chkExportPdf";
            chkExportPdf.Size = new Size(122, 24);
            chkExportPdf.TabIndex = 14;
            chkExportPdf.Text = "Export to PDF";
            chkExportPdf.UseVisualStyleBackColor = true;
            // 
            // chkAuditLogs
            // 
            chkAuditLogs.AutoSize = true;
            chkAuditLogs.Location = new Point(140, 310);
            chkAuditLogs.Name = "chkAuditLogs";
            chkAuditLogs.Size = new Size(102, 24);
            chkAuditLogs.TabIndex = 15;
            chkAuditLogs.Text = "Audit Logs";
            chkAuditLogs.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(12, 372);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(135, 29);
            btnSave.TabIndex = 16;
            btnSave.Text = "Save Permissions";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(326, 372);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(94, 29);
            btnClose.TabIndex = 17;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // chkChangePassword
            // 
            chkChangePassword.AutoSize = true;
            chkChangePassword.Location = new Point(141, 340);
            chkChangePassword.Name = "chkChangePassword";
            chkChangePassword.Size = new Size(146, 24);
            chkChangePassword.TabIndex = 18;
            chkChangePassword.Text = "Change Password";
            chkChangePassword.UseVisualStyleBackColor = true;
            // 
            // frmManagePermissions
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(432, 430);
            Controls.Add(chkChangePassword);
            Controls.Add(btnClose);
            Controls.Add(btnSave);
            Controls.Add(chkAuditLogs);
            Controls.Add(chkExportPdf);
            Controls.Add(chkExportExcel);
            Controls.Add(chkAnalytics);
            Controls.Add(chkDeleteCustomer);
            Controls.Add(chkEditCustomer);
            Controls.Add(chkAddCustomer);
            Controls.Add(cmbRole);
            Controls.Add(lblRole);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "frmManagePermissions";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ManagePermissions";
            Load += frmManagePermissions_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblRole;
        private ComboBox cmbRole;
        private CheckBox chkAddCustomer;
        private CheckBox chkEditCustomer;
        private CheckBox chkDeleteCustomer;
        private CheckBox chkAnalytics;
        private CheckBox chkExportExcel;
        private CheckBox chkExportPdf;
        private CheckBox chkAuditLogs;
        private Button btnSave;
        private Button btnClose;
        private CheckBox chkChangePassword;
    }
}