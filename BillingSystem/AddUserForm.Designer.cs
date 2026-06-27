namespace BillingSystem
{
    partial class AddUserForm
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
            lblUsername = new Label();
            lblPassword = new Label();
            lblFullName = new Label();
            lblRole = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            txtFullName = new TextBox();
            cmbRole = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            lblEditHint = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(35, 22);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(112, 31);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Add User";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(35, 87);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(78, 20);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Username:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(35, 132);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(73, 20);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Password:";
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(35, 177);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(79, 20);
            lblFullName.TabIndex = 3;
            lblFullName.Text = "Full Name:";
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Location = new Point(35, 222);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(42, 20);
            lblRole.TabIndex = 4;
            lblRole.Text = "Role:";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(140, 84);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(258, 27);
            txtUsername.TabIndex = 5;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(140, 129);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(258, 27);
            txtPassword.TabIndex = 6;
            txtPassword.Text = "**********";
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(140, 174);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(258, 27);
            txtFullName.TabIndex = 7;
            // 
            // cmbRole
            // 
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(140, 219);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(258, 28);
            cmbRole.TabIndex = 8;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(140, 287);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(112, 42);
            btnSave.TabIndex = 9;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(286, 287);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 42);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblEditHint
            // 
            lblEditHint.Anchor = AnchorStyles.None;
            lblEditHint.ForeColor = Color.Firebrick;
            lblEditHint.Location = new Point(21, 129);
            lblEditHint.Name = "lblEditHint";
            lblEditHint.Size = new Size(400, 27);
            lblEditHint.TabIndex = 11;
            lblEditHint.Text = "Password changes use the separate form.";
            lblEditHint.TextAlign = ContentAlignment.MiddleCenter;
            lblEditHint.Visible = false;
            // 
            // AddUserForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(446, 363);
            Controls.Add(lblEditHint);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(cmbRole);
            Controls.Add(txtFullName);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblRole);
            Controls.Add(lblFullName);
            Controls.Add(lblPassword);
            Controls.Add(lblUsername);
            Controls.Add(lblTitle);
            Name = "AddUserForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add User";
            Load += AddUserForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblFullName;
        private Label lblRole;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtFullName;
        private ComboBox cmbRole;
        private Button btnSave;
        private Button btnCancel;
        private Label lblEditHint;
    }
}
