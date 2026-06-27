namespace BillingSystem
{
    partial class frmChangePassword
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
            lblcurrentpassword = new Label();
            txtCurrentPassword = new TextBox();
            lblnewpassword = new Label();
            txtNewPassword = new TextBox();
            txtRetypePassword = new TextBox();
            btnsave = new Button();
            btnCancel = new Button();
            lblconfirmpassword = new Label();
            SuspendLayout();
            // 
            // lblcurrentpassword
            // 
            lblcurrentpassword.AutoSize = true;
            lblcurrentpassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblcurrentpassword.Location = new Point(44, 26);
            lblcurrentpassword.Name = "lblcurrentpassword";
            lblcurrentpassword.Size = new Size(133, 20);
            lblcurrentpassword.TabIndex = 0;
            lblcurrentpassword.Text = "Current Password";
            // 
            // txtCurrentPassword
            // 
            txtCurrentPassword.Location = new Point(202, 26);
            txtCurrentPassword.Name = "txtCurrentPassword";
            txtCurrentPassword.Size = new Size(262, 27);
            txtCurrentPassword.TabIndex = 1;
            txtCurrentPassword.UseSystemPasswordChar = true;
            // 
            // lblnewpassword
            // 
            lblnewpassword.AutoSize = true;
            lblnewpassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblnewpassword.Location = new Point(65, 73);
            lblnewpassword.Name = "lblnewpassword";
            lblnewpassword.Size = new Size(112, 20);
            lblnewpassword.TabIndex = 2;
            lblnewpassword.Text = "New Password";
            // 
            // txtNewPassword
            // 
            txtNewPassword.Location = new Point(202, 73);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.Size = new Size(262, 27);
            txtNewPassword.TabIndex = 3;
            txtNewPassword.UseSystemPasswordChar = true;
            // 
            // txtRetypePassword
            // 
            txtRetypePassword.Location = new Point(202, 120);
            txtRetypePassword.Name = "txtRetypePassword";
            txtRetypePassword.Size = new Size(262, 27);
            txtRetypePassword.TabIndex = 4;
            txtRetypePassword.UseSystemPasswordChar = true;
            // 
            // btnsave
            // 
            btnsave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnsave.Location = new Point(202, 180);
            btnsave.Name = "btnsave";
            btnsave.Size = new Size(117, 29);
            btnsave.TabIndex = 5;
            btnsave.Text = "Save";
            btnsave.UseVisualStyleBackColor = true;
            btnsave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.Location = new Point(343, 180);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(121, 29);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblconfirmpassword
            // 
            lblconfirmpassword.AutoSize = true;
            lblconfirmpassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblconfirmpassword.Location = new Point(40, 123);
            lblconfirmpassword.Name = "lblconfirmpassword";
            lblconfirmpassword.Size = new Size(137, 20);
            lblconfirmpassword.TabIndex = 7;
            lblconfirmpassword.Text = "Confirm Password";
            // 
            // frmChangePassword
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(503, 353);
            Controls.Add(lblconfirmpassword);
            Controls.Add(btnCancel);
            Controls.Add(btnsave);
            Controls.Add(txtRetypePassword);
            Controls.Add(txtNewPassword);
            Controls.Add(lblnewpassword);
            Controls.Add(txtCurrentPassword);
            Controls.Add(lblcurrentpassword);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmChangePassword";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Change Password";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblcurrentpassword;
        private TextBox txtCurrentPassword;
        private Label lblnewpassword;
        private TextBox txtNewPassword;
        private TextBox txtRetypePassword;
        private Button btnsave;
        private Button btnCancel;
        private Label lblconfirmpassword;
    }
}