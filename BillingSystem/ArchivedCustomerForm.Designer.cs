namespace BillingSystem
{
    partial class ArchivedCustomerForm
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
            dgvArchivedLIst = new DataGridView();
            colCustomerID = new DataGridViewTextBoxColumn();
            colFullName = new DataGridViewTextBoxColumn();
            colAddress = new DataGridViewTextBoxColumn();
            colContactNumber = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colBalance = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            btnUnarchive = new Button();
            btnClose = new Button();
            pnlTop = new Panel();
            pnlBottom = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvArchivedLIst).BeginInit();
            pnlTop.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(12, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(268, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Archived Customers (0)";
            // 
            // dgvArchivedLIst
            // 
            dgvArchivedLIst.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvArchivedLIst.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvArchivedLIst.Columns.AddRange(new DataGridViewColumn[] { colCustomerID, colFullName, colAddress, colContactNumber, colEmail, colBalance, colStatus });
            dgvArchivedLIst.Location = new Point(12, 68);
            dgvArchivedLIst.MultiSelect = false;
            dgvArchivedLIst.Name = "dgvArchivedLIst";
            dgvArchivedLIst.ReadOnly = true;
            dgvArchivedLIst.RowHeadersWidth = 51;
            dgvArchivedLIst.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvArchivedLIst.Size = new Size(924, 368);
            dgvArchivedLIst.TabIndex = 1;
            dgvArchivedLIst.SelectionChanged += dgvArchivedLIst_SelectionChanged;
            // 
            // colCustomerID
            // 
            colCustomerID.HeaderText = "ID";
            colCustomerID.MinimumWidth = 6;
            colCustomerID.Name = "colCustomerID";
            colCustomerID.ReadOnly = true;
            // 
            // colFullName
            // 
            colFullName.HeaderText = "Full Name";
            colFullName.MinimumWidth = 6;
            colFullName.Name = "colFullName";
            colFullName.ReadOnly = true;
            // 
            // colAddress
            // 
            colAddress.HeaderText = "Address";
            colAddress.MinimumWidth = 6;
            colAddress.Name = "colAddress";
            colAddress.ReadOnly = true;
            // 
            // colContactNumber
            // 
            colContactNumber.HeaderText = "Contact No.";
            colContactNumber.MinimumWidth = 6;
            colContactNumber.Name = "colContactNumber";
            colContactNumber.ReadOnly = true;
            // 
            // colEmail
            // 
            colEmail.HeaderText = "Email";
            colEmail.MinimumWidth = 6;
            colEmail.Name = "colEmail";
            colEmail.ReadOnly = true;
            // 
            // colBalance
            // 
            colBalance.HeaderText = "Balance";
            colBalance.MinimumWidth = 6;
            colBalance.Name = "colBalance";
            colBalance.ReadOnly = true;
            // 
            // colStatus
            // 
            colStatus.HeaderText = "Status";
            colStatus.MinimumWidth = 6;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            // 
            // btnUnarchive
            // 
            btnUnarchive.Location = new Point(678, 12);
            btnUnarchive.Name = "btnUnarchive";
            btnUnarchive.Size = new Size(124, 40);
            btnUnarchive.TabIndex = 2;
            btnUnarchive.Text = "Unarchive";
            btnUnarchive.UseVisualStyleBackColor = true;
            btnUnarchive.Click += btnUnarchive_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(808, 12);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(124, 40);
            btnClose.TabIndex = 3;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(948, 62);
            pnlTop.TabIndex = 4;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnUnarchive);
            pnlBottom.Controls.Add(btnClose);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 442);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(948, 64);
            pnlBottom.TabIndex = 5;
            // 
            // ArchivedCustomerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(948, 506);
            Controls.Add(pnlBottom);
            Controls.Add(pnlTop);
            Controls.Add(dgvArchivedLIst);
            Name = "ArchivedCustomerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Archived Customers";
            Load += ArchivedCustomerForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvArchivedLIst).EndInit();
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvArchivedLIst;
        private Button btnUnarchive;
        private Button btnClose;
        private Panel pnlTop;
        private Panel pnlBottom;
        private DataGridViewTextBoxColumn colCustomerID;
        private DataGridViewTextBoxColumn colFullName;
        private DataGridViewTextBoxColumn colAddress;
        private DataGridViewTextBoxColumn colContactNumber;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colBalance;
        private DataGridViewTextBoxColumn colStatus;
    }
}
