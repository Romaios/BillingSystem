namespace BillingSystem
{
    partial class frmBillingHistory
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
            lblCustomerName = new Label();
            dgvBilling = new DataGridView();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvBilling).BeginInit();
            SuspendLayout();
            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCustomerName.Location = new Point(16, 9);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(178, 32);
            lblCustomerName.TabIndex = 0;
            lblCustomerName.Text = "Billing History";
            // 
            // dgvBilling
            // 
            dgvBilling.AllowUserToAddRows = false;
            dgvBilling.AllowUserToDeleteRows = false;
            dgvBilling.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBilling.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBilling.Location = new Point(16, 56);
            dgvBilling.Name = "dgvBilling";
            dgvBilling.ReadOnly = true;
            dgvBilling.RowHeadersWidth = 51;
            dgvBilling.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBilling.Size = new Size(740, 300);
            dgvBilling.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(636, 366);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(120, 40);
            btnClose.TabIndex = 2;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // frmBillingHistory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(772, 420);
            Controls.Add(btnClose);
            Controls.Add(dgvBilling);
            Controls.Add(lblCustomerName);
            Name = "frmBillingHistory";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Billing History";
            Load += frmBillingHistory_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBilling).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.DataGridView dgvBilling;
        private System.Windows.Forms.Button btnClose;
    }
}
