namespace BillingSystem
{
    partial class CustomerListForm
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
            components = new System.ComponentModel.Container();
            lblTitle = new Label();
            dgvCustomers = new DataGridView();
            Customerid = new DataGridViewTextBoxColumn();
            Fullname = new DataGridViewTextBoxColumn();
            Address = new DataGridViewTextBoxColumn();
            Contactnumber = new DataGridViewTextBoxColumn();
            Email = new DataGridViewTextBoxColumn();
            Balance = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            btnDelete = new Button();
            btnLogout = new Button();
            btnSearch = new Button();
            txtSearch = new TextBox();
            btnAnalytics = new Button();
            btnExportExcel = new Button();
            btnExportPdf = new Button();
            btnAuditLog = new Button();
            btnManagePermissions = new Button();
            statusStrip1 = new StatusStrip();
            lblStatusUser = new ToolStripStatusLabel();
            lblStatusSep = new ToolStripStatusLabel();
            lblStatusTime = new ToolStripStatusLabel();
            statusTimer = new System.Windows.Forms.Timer(components);
            pnlBottom = new Panel();
            pnlTop = new Panel();
            btnChangePassword = new Button();
            btnViewBillng = new Button();
            btnusermanagement = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTitle.Location = new Point(12, 36);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(106, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Customer List";
            // 
            // dgvCustomers
            // 
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Columns.AddRange(new DataGridViewColumn[] { Customerid, Fullname, Address, Contactnumber, Email, Balance });
            dgvCustomers.Location = new Point(12, 81);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersWidth = 51;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(758, 188);
            dgvCustomers.TabIndex = 1;
            dgvCustomers.CellContentClick += dgvCustomers_CellContentClick;
            dgvCustomers.CellContentDoubleClick += dgvCustomers_CellDoubleClick;
            dgvCustomers.SelectionChanged += dgvCustomers_SelectionChanged;
            // 
            // Customerid
            // 
            Customerid.HeaderText = "ID";
            Customerid.MinimumWidth = 6;
            Customerid.Name = "Customerid";
            Customerid.ReadOnly = true;
            // 
            // Fullname
            // 
            Fullname.HeaderText = "Full Name";
            Fullname.MinimumWidth = 6;
            Fullname.Name = "Fullname";
            Fullname.ReadOnly = true;
            // 
            // Address
            // 
            Address.HeaderText = "Address";
            Address.MinimumWidth = 6;
            Address.Name = "Address";
            Address.ReadOnly = true;
            // 
            // Contactnumber
            // 
            Contactnumber.HeaderText = "Contact No";
            Contactnumber.MinimumWidth = 6;
            Contactnumber.Name = "Contactnumber";
            Contactnumber.ReadOnly = true;
            // 
            // Email
            // 
            Email.HeaderText = "Email";
            Email.MinimumWidth = 6;
            Email.Name = "Email";
            Email.ReadOnly = true;
            // 
            // Balance
            // 
            Balance.HeaderText = "Balance";
            Balance.MinimumWidth = 6;
            Balance.Name = "Balance";
            Balance.ReadOnly = true;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdd.Location = new Point(12, 291);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add\r\n";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.Location = new Point(112, 291);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnLogout
            // 
            btnLogout.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLogout.Location = new Point(212, 291);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(94, 29);
            btnLogout.TabIndex = 4;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.Location = new Point(285, 32);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(109, 29);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click_1;
            btnSearch.KeyPress += txtSearch_KeyPress;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(414, 36);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(356, 27);
            txtSearch.TabIndex = 6;
            txtSearch.TextChanged += textBox1_TextChanged;
            txtSearch.KeyPress += txtSearch_KeyPress;
            // 
            // btnAnalytics
            // 
            btnAnalytics.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAnalytics.Location = new Point(12, 338);
            btnAnalytics.Name = "btnAnalytics";
            btnAnalytics.Size = new Size(94, 29);
            btnAnalytics.TabIndex = 7;
            btnAnalytics.Text = " Analytics";
            btnAnalytics.UseVisualStyleBackColor = true;
            btnAnalytics.Click += btnAnalytics_Click;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExportExcel.Location = new Point(394, 291);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(177, 29);
            btnExportExcel.TabIndex = 8;
            btnExportExcel.Text = "Export to Excel";
            btnExportExcel.UseVisualStyleBackColor = true;
            btnExportExcel.Click += btnExportExcel_Click;
            // 
            // btnExportPdf
            // 
            btnExportPdf.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExportPdf.Location = new Point(394, 338);
            btnExportPdf.Name = "btnExportPdf";
            btnExportPdf.Size = new Size(177, 29);
            btnExportPdf.TabIndex = 9;
            btnExportPdf.Text = "Export to PDF";
            btnExportPdf.UseVisualStyleBackColor = true;
            btnExportPdf.Click += btnExportPdf_Click;
            // 
            // btnAuditLog
            // 
            btnAuditLog.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAuditLog.Location = new Point(112, 338);
            btnAuditLog.Name = "btnAuditLog";
            btnAuditLog.Size = new Size(94, 29);
            btnAuditLog.TabIndex = 10;
            btnAuditLog.Text = "Audit Log";
            btnAuditLog.UseVisualStyleBackColor = true;
            btnAuditLog.Click += btnAuditLog_Click;
            // 
            // btnManagePermissions
            // 
            btnManagePermissions.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnManagePermissions.Location = new Point(593, 338);
            btnManagePermissions.Name = "btnManagePermissions";
            btnManagePermissions.Size = new Size(177, 29);
            btnManagePermissions.TabIndex = 11;
            btnManagePermissions.Text = "Manage Permissions\r\n";
            btnManagePermissions.UseVisualStyleBackColor = true;
            btnManagePermissions.Click += btnManagePermissions_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatusUser, lblStatusSep, lblStatusTime });
            statusStrip1.Location = new Point(0, 469);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(782, 26);
            statusStrip1.TabIndex = 12;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatusUser
            // 
            lblStatusUser.Name = "lblStatusUser";
            lblStatusUser.Size = new Size(208, 20);
            lblStatusUser.Text = " User: [username] | Role: [role]";
            // 
            // lblStatusSep
            // 
            lblStatusSep.Name = "lblStatusSep";
            lblStatusSep.Size = new Size(403, 20);
            lblStatusSep.Spring = true;
            lblStatusSep.Text = " ";
            // 
            // lblStatusTime
            // 
            lblStatusTime.Name = "lblStatusTime";
            lblStatusTime.Size = new Size(156, 20);
            lblStatusTime.Text = " current date and time";
            // 
            // statusTimer
            // 
            statusTimer.Enabled = true;
            statusTimer.Interval = 1000;
            statusTimer.Tick += statusTimer_Tick;
            // 
            // pnlBottom
            // 
            pnlBottom.Location = new Point(12, 426);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(758, 34);
            pnlBottom.TabIndex = 13;
            // 
            // pnlTop
            // 
            pnlTop.Location = new Point(16, 10);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(755, 16);
            pnlTop.TabIndex = 14;
            // 
            // btnChangePassword
            // 
            btnChangePassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnChangePassword.Location = new Point(593, 291);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(177, 29);
            btnChangePassword.TabIndex = 15;
            btnChangePassword.Text = "Change Password";
            btnChangePassword.UseVisualStyleBackColor = true;
            btnChangePassword.Click += btnChangePassword_Click;
            // 
            // btnViewBillng
            // 
            btnViewBillng.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnViewBillng.Location = new Point(212, 338);
            btnViewBillng.Name = "btnViewBillng";
            btnViewBillng.Size = new Size(147, 29);
            btnViewBillng.TabIndex = 16;
            btnViewBillng.Text = "View Billing";
            btnViewBillng.UseVisualStyleBackColor = true;
            btnViewBillng.Click += btnViewBillng_Click;
            // 
            // btnusermanagement
            // 
            btnusermanagement.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnusermanagement.Location = new Point(595, 383);
            btnusermanagement.Name = "btnusermanagement";
            btnusermanagement.Size = new Size(175, 29);
            btnusermanagement.TabIndex = 17;
            btnusermanagement.Text = "User Management";
            btnusermanagement.UseVisualStyleBackColor = true;
            btnusermanagement.Click += btnusermanagement_Click;
            // 
            // CustomerListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 495);
            Controls.Add(btnusermanagement);
            Controls.Add(btnViewBillng);
            Controls.Add(btnChangePassword);
            Controls.Add(pnlTop);
            Controls.Add(pnlBottom);
            Controls.Add(statusStrip1);
            Controls.Add(btnManagePermissions);
            Controls.Add(btnAuditLog);
            Controls.Add(btnExportPdf);
            Controls.Add(btnExportExcel);
            Controls.Add(btnAnalytics);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(btnLogout);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(dgvCustomers);
            Controls.Add(lblTitle);
            MaximizeBox = false;
            Name = "CustomerListForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Billig System - Customer List";
            Load += CustomerListForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvCustomers;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnLogout;
        private Button btnSearch;
        private TextBox txtSearch;
        private DataGridViewTextBoxColumn Customerid;
        private DataGridViewTextBoxColumn Fullname;
        private DataGridViewTextBoxColumn Address;
        private DataGridViewTextBoxColumn Contactnumber;
        private DataGridViewTextBoxColumn Email;
        private DataGridViewTextBoxColumn Balance;
        private Button btnAnalytics;
        private Button btnExportExcel;
        private Button btnExportPdf;
        private Button btnAuditLog;
        private Button btnManagePermissions;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatusUser;
        private ToolStripStatusLabel lblStatusSep;
        private ToolStripStatusLabel lblStatusTime;
        private System.Windows.Forms.Timer statusTimer;
        private Panel pnlBottom;
        private Panel pnlTop;
        private Button btnChangePassword;
        private Button btnViewBillng;
        private Button btnusermanagement;
    }
}