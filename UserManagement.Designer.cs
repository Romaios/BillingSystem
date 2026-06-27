namespace BillingSystem
{
    partial class UserListForm
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
            lblusermanagement = new Label();
            dgvUsers = new DataGridView();
            UserId = new DataGridViewTextBoxColumn();
            UserName = new DataGridViewTextBoxColumn();
            FullName = new DataGridViewTextBoxColumn();
            Role = new DataGridViewTextBoxColumn();
            colCreated = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            btnEdit = new Button();
            btndelete = new Button();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // lblusermanagement
            // 
            lblusermanagement.AutoSize = true;
            lblusermanagement.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblusermanagement.Location = new Point(168, 23);
            lblusermanagement.Name = "lblusermanagement";
            lblusermanagement.Size = new Size(156, 23);
            lblusermanagement.TabIndex = 0;
            lblusermanagement.Text = "User Management";
            // 
            // dgvUsers
            // 
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Columns.AddRange(new DataGridViewColumn[] { UserId, UserName, FullName, Role, colCreated });
            dgvUsers.Location = new Point(168, 63);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.Size = new Size(680, 255);
            dgvUsers.TabIndex = 1;
            dgvUsers.SelectionChanged += dgvUsers_SelectionChanged;
            // 
            // UserId
            // 
            UserId.HeaderText = "User ID";
            UserId.MinimumWidth = 6;
            UserId.Name = "UserId";
            UserId.Width = 125;
            // 
            // UserName
            // 
            UserName.HeaderText = "User Name";
            UserName.MinimumWidth = 6;
            UserName.Name = "UserName";
            UserName.Width = 125;
            // 
            // FullName
            // 
            FullName.HeaderText = "Full Name";
            FullName.MinimumWidth = 6;
            FullName.Name = "FullName";
            FullName.Width = 125;
            // 
            // Role
            // 
            Role.HeaderText = "Role";
            Role.MinimumWidth = 6;
            Role.Name = "Role";
            Role.Width = 125;
            // 
            // colCreated
            // 
            colCreated.HeaderText = "Created";
            colCreated.MinimumWidth = 6;
            colCreated.Name = "colCreated";
            colCreated.Width = 125;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.Location = new Point(21, 63);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(122, 29);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add User";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAddUser_Click;
            // 
            // btnEdit
            // 
            btnEdit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEdit.Location = new Point(21, 110);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(120, 29);
            btnEdit.TabIndex = 3;
            btnEdit.Text = "Edit User";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEditUser_Click;
            // 
            // btndelete
            // 
            btndelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btndelete.Location = new Point(21, 167);
            btndelete.Name = "btndelete";
            btndelete.Size = new Size(120, 29);
            btndelete.TabIndex = 4;
            btndelete.Text = "Delete User";
            btndelete.UseVisualStyleBackColor = true;
            btndelete.Click += btnDeleteUser_Click;
            // 
            // btnClose
            // 
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.Location = new Point(21, 220);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(120, 29);
            btnClose.TabIndex = 5;
            btnClose.Text = "C L O S E";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // UserListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 393);
            Controls.Add(btnClose);
            Controls.Add(btndelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(dgvUsers);
            Controls.Add(lblusermanagement);
            Name = "UserListForm";
            Text = "User List Form";
            Load += FrmUserListForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblusermanagement;
        private DataGridView dgvUsers;
        private DataGridViewTextBoxColumn UserId;
        private DataGridViewTextBoxColumn UserName;
        private DataGridViewTextBoxColumn FullName;
        private DataGridViewTextBoxColumn Role;
        private DataGridViewTextBoxColumn colCreated;
        private Button btnAdd;
        private Button btnEdit;
        private Button btndelete;
        private Button btnClose;
    }
}
