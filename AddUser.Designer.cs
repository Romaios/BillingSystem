namespace BillingSystem
{
    partial class AddUser
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
            lbladduser = new Label();
            lblusername = new Label();
            lblpassword = new Label();
            lblfullname = new Label();
            lblrole = new Label();
            txtusername = new TextBox();
            txtpassword = new TextBox();
            txtfullname = new TextBox();
            cmbrole = new ComboBox();
            btnsave = new Button();
            btncancel = new Button();
            SuspendLayout();
            // 
            // lbladduser
            // 
            lbladduser.AutoSize = true;
            lbladduser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lbladduser.Location = new Point(152, 33);
            lbladduser.Name = "lbladduser";
            lbladduser.Size = new Size(84, 23);
            lbladduser.TabIndex = 0;
            lbladduser.Text = "Add User";
            // 
            // lblusername
            // 
            lblusername.AutoSize = true;
            lblusername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblusername.Location = new Point(49, 80);
            lblusername.Name = "lblusername";
            lblusername.Size = new Size(97, 23);
            lblusername.TabIndex = 1;
            lblusername.Text = "User Name";
            // 
            // lblpassword
            // 
            lblpassword.AutoSize = true;
            lblpassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblpassword.Location = new Point(49, 127);
            lblpassword.Name = "lblpassword";
            lblpassword.Size = new Size(85, 23);
            lblpassword.TabIndex = 2;
            lblpassword.Text = "Password";
            // 
            // lblfullname
            // 
            lblfullname.AutoSize = true;
            lblfullname.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblfullname.Location = new Point(49, 174);
            lblfullname.Name = "lblfullname";
            lblfullname.Size = new Size(91, 23);
            lblfullname.TabIndex = 3;
            lblfullname.Text = "Full Name";
            // 
            // lblrole
            // 
            lblrole.AutoSize = true;
            lblrole.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblrole.Location = new Point(49, 219);
            lblrole.Name = "lblrole";
            lblrole.Size = new Size(45, 23);
            lblrole.TabIndex = 4;
            lblrole.Text = "Role";
            // 
            // txtusername
            // 
            txtusername.Location = new Point(152, 76);
            txtusername.Name = "txtusername";
            txtusername.Size = new Size(265, 27);
            txtusername.TabIndex = 5;
            // 
            // txtpassword
            // 
            txtpassword.Location = new Point(154, 123);
            txtpassword.Name = "txtpassword";
            txtpassword.Size = new Size(263, 27);
            txtpassword.TabIndex = 6;
            txtpassword.Text = "********";
            txtpassword.UseSystemPasswordChar = true;
            // 
            // txtfullname
            // 
            txtfullname.Location = new Point(154, 173);
            txtfullname.Name = "txtfullname";
            txtfullname.Size = new Size(263, 27);
            txtfullname.TabIndex = 7;
            // 
            // cmbrole
            // 
            cmbrole.FormattingEnabled = true;
            cmbrole.Location = new Point(152, 219);
            cmbrole.Name = "cmbrole";
            cmbrole.Size = new Size(263, 28);
            cmbrole.TabIndex = 8;
            // 
            // btnsave
            // 
            btnsave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnsave.Location = new Point(152, 291);
            btnsave.Name = "btnsave";
            btnsave.Size = new Size(123, 29);
            btnsave.TabIndex = 9;
            btnsave.Text = "S A V E";
            btnsave.UseVisualStyleBackColor = true;
            btnsave.Click += btnSave_Click;
            // 
            // btncancel
            // 
            btncancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btncancel.Location = new Point(296, 291);
            btncancel.Name = "btncancel";
            btncancel.Size = new Size(119, 29);
            btncancel.TabIndex = 10;
            btncancel.Text = "C A N C E L";
            btncancel.UseVisualStyleBackColor = true;
            btncancel.Click += btnCancel_Click;
            // 
            // AddUser
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(502, 373);
            Controls.Add(btncancel);
            Controls.Add(btnsave);
            Controls.Add(cmbrole);
            Controls.Add(txtfullname);
            Controls.Add(txtpassword);
            Controls.Add(txtusername);
            Controls.Add(lblrole);
            Controls.Add(lblfullname);
            Controls.Add(lblpassword);
            Controls.Add(lblusername);
            Controls.Add(lbladduser);
            Name = "AddUser";
            StartPosition = FormStartPosition.CenterParent;
            Text = "AddUser";
            Load += AddUser_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbladduser;
        private Label lblusername;
        private Label lblpassword;
        private Label lblfullname;
        private Label lblrole;
        private TextBox txtusername;
        private TextBox txtpassword;
        private TextBox txtfullname;
        private ComboBox cmbrole;
        private Button btnsave;
        private Button btncancel;
    }
}
