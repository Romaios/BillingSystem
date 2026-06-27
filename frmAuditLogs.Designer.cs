namespace BillingSystem
{
    partial class frmAuditLogs
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
            lblFrom = new Label();
            dtpFrom = new DateTimePicker();
            lblTo = new Label();
            dtpTo = new DateTimePicker();
            btnSearch = new Button();
            dgvAuditLogs = new DataGridView();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAuditLogs).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(65, 22);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(218, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = " Audit Log Report";
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(65, 90);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(50, 20);
            lblFrom.TabIndex = 1;
            lblFrom.Text = " From:";
            // 
            // dtpFrom
            // 
            dtpFrom.Location = new Point(129, 90);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(250, 27);
            dtpFrom.TabIndex = 2;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(397, 97);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(28, 20);
            lblTo.TabIndex = 3;
            lblTo.Text = "To:";
            // 
            // dtpTo
            // 
            dtpTo.Location = new Point(431, 92);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(250, 27);
            dtpTo.TabIndex = 4;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(701, 93);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // dgvAuditLogs
            // 
            dgvAuditLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAuditLogs.Location = new Point(65, 140);
            dgvAuditLogs.Name = "dgvAuditLogs";
            dgvAuditLogs.RowHeadersWidth = 51;
            dgvAuditLogs.Size = new Size(730, 285);
            dgvAuditLogs.TabIndex = 6;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(701, 440);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(94, 29);
            btnClose.TabIndex = 7;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // frmAuditLogs
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 503);
            Controls.Add(btnClose);
            Controls.Add(dgvAuditLogs);
            Controls.Add(btnSearch);
            Controls.Add(dtpTo);
            Controls.Add(lblTo);
            Controls.Add(dtpFrom);
            Controls.Add(lblFrom);
            Controls.Add(lblTitle);
            Name = "frmAuditLogs";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Audit Log Report";
            Load += frmAuditLogs_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAuditLogs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblFrom;
        private DateTimePicker dtpFrom;
        private Label lblTo;
        private DateTimePicker dtpTo;
        private Button btnSearch;
        private DataGridView dgvAuditLogs;
        private Button btnClose;
    }
}