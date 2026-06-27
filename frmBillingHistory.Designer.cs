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
            dgvBilling = new DataGridView();
            lblCustomerName = new Label();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvBilling).BeginInit();
            SuspendLayout();
            // 
            // dgvBilling
            // 
            dgvBilling.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBilling.Location = new Point(80, 66);
            dgvBilling.Name = "dgvBilling";
            dgvBilling.RowHeadersWidth = 51;
            dgvBilling.Size = new Size(900, 280);
            dgvBilling.TabIndex = 0;
            dgvBilling.CellContentClick += dgvBilling_CellContentClick;
            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCustomerName.Location = new Point(80, 25);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(176, 28);
            lblCustomerName.TabIndex = 1;
            lblCustomerName.Text = "BILLING HISTORY";
            // 
            // btnClose
            // 
            btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnClose.Location = new Point(857, 370);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(123, 43);
            btnClose.TabIndex = 2;
            btnClose.Text = "C L O S E";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // frmBillingHistory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1040, 450);
            Controls.Add(btnClose);
            Controls.Add(lblCustomerName);
            Controls.Add(dgvBilling);
            Name = "frmBillingHistory";
            Text = "Billing History";
            Load += frmBillingHistory_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBilling).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvBilling;
        private Label lblCustomerName;
        private Button btnClose;
    }
}