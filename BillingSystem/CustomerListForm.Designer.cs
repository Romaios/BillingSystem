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
            contextMenuStripCustomers = new ContextMenuStrip(components);
            archiveCustomerToolStripMenuItem = new ToolStripMenuItem();
            viewArchivedListToolStripMenuItem = new ToolStripMenuItem();
            lblTitle = new Label();
            dgvCustomers = new DataGridView();
            CustomerID = new DataGridViewTextBoxColumn();
            FullName = new DataGridViewTextBoxColumn();
            Address = new DataGridViewTextBoxColumn();
            ContactNumber = new DataGridViewTextBoxColumn();
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
            btnChangePassword = new Button();
            btnViewBilling = new Button();
            statusStrip1 = new StatusStrip();
            lblStatusUser = new ToolStripStatusLabel();
            lblStatusSep = new ToolStripStatusLabel();
            lblStatusTime = new ToolStripStatusLabel();
            pnlTop = new Panel();
            pnlBottom = new Panel();
            btnToggleSidePanel = new Button();
            btnUserManagement = new Button();
            statusTImer = new System.Windows.Forms.Timer(components);
            sidePanelToolTip = new ToolTip(components);
            contextMenuStripCustomers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            statusStrip1.SuspendLayout();
            pnlTop.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStripCustomers
            // 
            contextMenuStripCustomers.ImageScalingSize = new Size(20, 20);
            contextMenuStripCustomers.Items.AddRange(new ToolStripItem[] { archiveCustomerToolStripMenuItem, viewArchivedListToolStripMenuItem });
            contextMenuStripCustomers.Name = "contextMenuStripCustomers";
            contextMenuStripCustomers.Size = new Size(171, 48);
            // 
            // archiveCustomerToolStripMenuItem
            // 
            archiveCustomerToolStripMenuItem.Name = "archiveCustomerToolStripMenuItem";
            archiveCustomerToolStripMenuItem.Size = new Size(170, 22);
            archiveCustomerToolStripMenuItem.Text = "Archive Customer";
            archiveCustomerToolStripMenuItem.Click += archiveCustomerToolStripMenuItem_Click;
            // 
            // viewArchivedListToolStripMenuItem
            // 
            viewArchivedListToolStripMenuItem.Name = "viewArchivedListToolStripMenuItem";
            viewArchivedListToolStripMenuItem.Size = new Size(170, 22);
            viewArchivedListToolStripMenuItem.Text = "View Archived List";
            viewArchivedListToolStripMenuItem.Click += viewArchivedListToolStripMenuItem_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = SystemColors.ControlText;
            lblTitle.Location = new Point(4, 11);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(133, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Customer List";
            lblTitle.Click += lblTitle_Click;
            // 
            // dgvCustomers
            // 
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Columns.AddRange(new DataGridViewColumn[] { CustomerID, FullName, Address, ContactNumber, Email, Balance });
            dgvCustomers.ContextMenuStrip = contextMenuStripCustomers;
            dgvCustomers.Location = new Point(10, 58);
            dgvCustomers.Margin = new Padding(3, 2, 3, 2);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersWidth = 51;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(687, 306);
            dgvCustomers.TabIndex = 1;
            dgvCustomers.CellContentDoubleClick += dgvCustomers_CellDoubleClick;
            dgvCustomers.SelectionChanged += dgvCustomers_SelectionChanged;
            dgvCustomers.MouseDown += dgvCustomers_MouseDown;
            // 
            // CustomerID
            // 
            CustomerID.HeaderText = "ID";
            CustomerID.MinimumWidth = 6;
            CustomerID.Name = "CustomerID";
            CustomerID.ReadOnly = true;
            CustomerID.Resizable = DataGridViewTriState.True;
            // 
            // FullName
            // 
            FullName.HeaderText = "Full Name";
            FullName.MinimumWidth = 6;
            FullName.Name = "FullName";
            FullName.ReadOnly = true;
            // 
            // Address
            // 
            Address.HeaderText = "Address";
            Address.MinimumWidth = 6;
            Address.Name = "Address";
            Address.ReadOnly = true;
            // 
            // ContactNumber
            // 
            ContactNumber.HeaderText = "Contact No.";
            ContactNumber.MinimumWidth = 6;
            ContactNumber.Name = "ContactNumber";
            ContactNumber.ReadOnly = true;
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
            btnAdd.Location = new Point(4, 7);
            btnAdd.Margin = new Padding(3, 2, 3, 2);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(105, 34);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add Customer";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(115, 7);
            btnDelete.Margin = new Padding(3, 2, 3, 2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(105, 34);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(113, 328);
            btnLogout.Margin = new Padding(3, 2, 3, 2);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(105, 32);
            btnLogout.TabIndex = 4;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(581, 6);
            btnSearch.Margin = new Padding(3, 2, 3, 2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(106, 36);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(320, 14);
            txtSearch.Margin = new Padding(3, 2, 3, 2);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(256, 23);
            txtSearch.TabIndex = 6;
            // 
            // btnAnalytics
            // 
            btnAnalytics.Location = new Point(4, 46);
            btnAnalytics.Margin = new Padding(3, 2, 3, 2);
            btnAnalytics.Name = "btnAnalytics";
            btnAnalytics.Size = new Size(105, 32);
            btnAnalytics.TabIndex = 7;
            btnAnalytics.Text = "Analytics";
            btnAnalytics.UseVisualStyleBackColor = true;
            btnAnalytics.Click += btnAnalytics_Click;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Location = new Point(4, 289);
            btnExportExcel.Margin = new Padding(3, 2, 3, 2);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(105, 34);
            btnExportExcel.TabIndex = 8;
            btnExportExcel.Text = "Export To Excel";
            btnExportExcel.UseVisualStyleBackColor = true;
            btnExportExcel.Click += btnExportExcel_Click;
            // 
            // btnExportPdf
            // 
            btnExportPdf.Location = new Point(3, 328);
            btnExportPdf.Margin = new Padding(3, 2, 3, 2);
            btnExportPdf.Name = "btnExportPdf";
            btnExportPdf.Size = new Size(105, 32);
            btnExportPdf.TabIndex = 9;
            btnExportPdf.Text = "Export To PDF";
            btnExportPdf.UseVisualStyleBackColor = true;
            btnExportPdf.Click += btnExportPdf_Click;
            // 
            // btnAuditLog
            // 
            btnAuditLog.Location = new Point(115, 46);
            btnAuditLog.Margin = new Padding(3, 2, 3, 2);
            btnAuditLog.Name = "btnAuditLog";
            btnAuditLog.Size = new Size(105, 32);
            btnAuditLog.TabIndex = 10;
            btnAuditLog.Text = "Audit Log";
            btnAuditLog.UseVisualStyleBackColor = true;
            btnAuditLog.Click += btnAuditLog_Click;
            // 
            // btnManagePermissions
            // 
            btnManagePermissions.Location = new Point(116, 82);
            btnManagePermissions.Margin = new Padding(3, 2, 3, 2);
            btnManagePermissions.Name = "btnManagePermissions";
            btnManagePermissions.Size = new Size(103, 40);
            btnManagePermissions.TabIndex = 11;
            btnManagePermissions.Text = "Manage Permissions";
            btnManagePermissions.UseVisualStyleBackColor = true;
            btnManagePermissions.Click += btnManagePermissions_Click;
            // 
            // btnChangePassword
            // 
            btnChangePassword.Location = new Point(4, 82);
            btnChangePassword.Margin = new Padding(3, 2, 3, 2);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(105, 40);
            btnChangePassword.TabIndex = 15;
            btnChangePassword.Text = "Change Password";
            btnChangePassword.UseVisualStyleBackColor = true;
            btnChangePassword.Click += btnChangePassword_Click;
            // 
            // btnViewBilling
            // 
            btnViewBilling.Location = new Point(113, 289);
            btnViewBilling.Margin = new Padding(3, 2, 3, 2);
            btnViewBilling.Name = "btnViewBilling";
            btnViewBilling.Size = new Size(105, 34);
            btnViewBilling.TabIndex = 16;
            btnViewBilling.Text = "View Billing";
            btnViewBilling.UseVisualStyleBackColor = true;
            btnViewBilling.Click += btnViewBilling_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatusUser, lblStatusSep, lblStatusTime });
            statusStrip1.Location = new Point(0, 364);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 12, 0);
            statusStrip1.Size = new Size(939, 22);
            statusStrip1.TabIndex = 12;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatusUser
            // 
            lblStatusUser.Name = "lblStatusUser";
            lblStatusUser.Size = new Size(67, 17);
            lblStatusUser.Text = "User/Status";
            // 
            // lblStatusSep
            // 
            lblStatusSep.Name = "lblStatusSep";
            lblStatusSep.Size = new Size(825, 17);
            lblStatusSep.Spring = true;
            // 
            // lblStatusTime
            // 
            lblStatusTime.Name = "lblStatusTime";
            lblStatusTime.Size = new Size(34, 17);
            lblStatusTime.Text = "Time";
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(txtSearch);
            pnlTop.Controls.Add(btnSearch);
            pnlTop.Location = new Point(10, 2);
            pnlTop.Margin = new Padding(3, 2, 3, 2);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(690, 52);
            pnlTop.TabIndex = 13;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnToggleSidePanel);
            pnlBottom.Controls.Add(btnUserManagement);
            pnlBottom.Controls.Add(btnAuditLog);
            pnlBottom.Controls.Add(btnAnalytics);
            pnlBottom.Controls.Add(btnLogout);
            pnlBottom.Controls.Add(btnExportPdf);
            pnlBottom.Controls.Add(btnManagePermissions);
            pnlBottom.Controls.Add(btnExportExcel);
            pnlBottom.Controls.Add(btnAdd);
            pnlBottom.Controls.Add(btnDelete);
            pnlBottom.Controls.Add(btnChangePassword);
            pnlBottom.Controls.Add(btnViewBilling);
            pnlBottom.Location = new Point(705, 2);
            pnlBottom.Margin = new Padding(3, 2, 3, 2);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(223, 362);
            pnlBottom.TabIndex = 14;
            // 
            // btnToggleSidePanel
            // 
            btnToggleSidePanel.Location = new Point(4, 4);
            btnToggleSidePanel.Margin = new Padding(3, 2, 3, 2);
            btnToggleSidePanel.Name = "btnToggleSidePanel";
            btnToggleSidePanel.Size = new Size(214, 25);
            btnToggleSidePanel.TabIndex = 18;
            btnToggleSidePanel.Text = "☰";
            btnToggleSidePanel.UseVisualStyleBackColor = true;
            btnToggleSidePanel.Click += btnToggleSidePanel_Click;
            // 
            // btnUserManagement
            // 
            btnUserManagement.Location = new Point(4, 128);
            btnUserManagement.Margin = new Padding(3, 2, 3, 2);
            btnUserManagement.Name = "btnUserManagement";
            btnUserManagement.Size = new Size(107, 40);
            btnUserManagement.TabIndex = 17;
            btnUserManagement.Text = "User Management";
            btnUserManagement.UseVisualStyleBackColor = true;
            btnUserManagement.Click += btnUserManagement_Click;
            // 
            // statusTImer
            // 
            statusTImer.Enabled = true;
            statusTImer.Interval = 1000;
            statusTImer.Tick += statusTimer_Tick;
            // 
            // CustomerListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(939, 386);
            Controls.Add(pnlBottom);
            Controls.Add(pnlTop);
            Controls.Add(statusStrip1);
            Controls.Add(dgvCustomers);
            Margin = new Padding(3, 2, 3, 2);
            Name = "CustomerListForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Billing System - Customer List";
            Load += CustomerListForm_Load;
            contextMenuStripCustomers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvCustomers;
        private ContextMenuStrip contextMenuStripCustomers;
        private ToolStripMenuItem archiveCustomerToolStripMenuItem;
        private ToolStripMenuItem viewArchivedListToolStripMenuItem;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnLogout;
        private Button btnSearch;
        private TextBox txtSearch;
        private DataGridViewTextBoxColumn CustomerID;
        private DataGridViewTextBoxColumn FullName;
        private DataGridViewTextBoxColumn Address;
        private DataGridViewTextBoxColumn ContactNumber;
        private DataGridViewTextBoxColumn Email;
        private DataGridViewTextBoxColumn Balance;
        private Button btnAnalytics;
        private Button btnExportExcel;
        private Button btnExportPdf;
        private Button btnAuditLog;
        private Button btnManagePermissions;
        private Button btnChangePassword;
        private Button btnViewBilling;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatusUser;
        private ToolStripStatusLabel lblStatusSep;
        private ToolStripStatusLabel lblStatusTime;
        private Panel pnlTop;
        private Panel pnlBottom;
        private System.Windows.Forms.Timer statusTImer;
        private Button btnUserManagement;
        private Button btnToggleSidePanel;
        private ToolTip sidePanelToolTip;
    }
}
