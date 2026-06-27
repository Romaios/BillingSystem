using BillingSystem.Database;
using BillingSystem.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BillingSystem
{
    public partial class UserListForm : Form
    {
        private int _selectedUserId;
        private bool _ignoreSelection = true;

        public UserListForm()
        {
            InitializeComponent();
            ConfigureDataGridView();
            Shown += FrmUserListForm_Shown;
        }

        private void FrmUserListForm_Load(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission(AppSession.CurrentRole, "ManageUsers"))
            {
                MessageBox.Show("You do not have permission to access User Management.",
                    "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            ApplyTheme();
            LoadUsers();
            AuditLogger.Log("VIEW_USER_MANAGEMENT",
                $"{AppSession.CurrentUsername} opened User Management.");
        }

        private void FrmUserListForm_Shown(object? sender, EventArgs e)
        {
            ClearUserSelection();
            _ignoreSelection = false;
        }

        private void ConfigureDataGridView()
        {
            dgvUsers.AutoGenerateColumns = false;
            UserId.DataPropertyName = "UserID";
            UserName.DataPropertyName = "Username";
            FullName.DataPropertyName = "FullName";
            Role.DataPropertyName = "Role";
            colCreated.DataPropertyName = "CreatedAt";
        }

        private void LoadUsers()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string sql = @"SELECT UserID,
                                          Username,
                                          FullName,
                                          Role,
                                          CreatedAt
                                   FROM   Users
                                   ORDER  BY Username ASC;";

                    using (var adapter = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvUsers.DataSource = dt;
                        lblusermanagement.Text = $"User Management ({dt.Rows.Count} user(s))";
                    }
                }

                ClearUserSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearUserSelection()
        {
            dgvUsers.ClearSelection();
            dgvUsers.CurrentCell = null;
            _selectedUserId = 0;
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (_ignoreSelection) return;
            if (dgvUsers.CurrentRow == null) return;

            object? idValue = dgvUsers.CurrentRow.Cells["UserId"].Value;
            if (idValue != null && int.TryParse(idValue.ToString(), out int userId))
            {
                _selectedUserId = userId;
            }
            else
            {
                _selectedUserId = 0;
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            using (var form = new AddUser())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadUsers();
                }
            }
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == 0)
            {
                MessageBox.Show("Please select a user to edit.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new AddUser(_selectedUserId))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadUsers();
                }
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == 0)
            {
                MessageBox.Show("Please select a user to delete.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TryDeleteSelectedUser();
        }

        private void TryDeleteSelectedUser()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string userSql = @"SELECT UserID, Username, Role
                                       FROM   Users
                                       WHERE  UserID = @UserID;";

                    string selectedUsername = string.Empty;
                    string selectedRole = string.Empty;

                    using (var userCmd = new MySqlCommand(userSql, conn))
                    {
                        userCmd.Parameters.AddWithValue("@UserID", _selectedUserId);

                        using (var reader = userCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("The selected user no longer exists.",
                                    "Delete User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                LoadUsers();
                                return;
                            }

                            selectedUsername = reader.GetString("Username");
                            selectedRole = reader.GetString("Role");
                        }
                    }

                    if (_selectedUserId == AppSession.CurrentUserID)
                    {
                        MessageBox.Show("You cannot delete your own account while logged in.",
                            "Delete Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.Equals(selectedRole, "Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        string countSql = @"SELECT COUNT(*)
                                            FROM   Users
                                            WHERE  Role = 'Admin';";

                        using (var countCmd = new MySqlCommand(countSql, conn))
                        {
                            long adminCount = Convert.ToInt64(countCmd.ExecuteScalar());
                            if (adminCount <= 1)
                            {
                                MessageBox.Show("You cannot delete the last remaining Admin account.",
                                    "Delete Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    DialogResult confirm = MessageBox.Show(
                        $"Are you sure you want to delete user '{selectedUsername}'?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirm != DialogResult.Yes)
                        return;

                    string deleteSql = @"DELETE FROM Users
                                         WHERE  UserID = @UserID;";

                    using (var deleteCmd = new MySqlCommand(deleteSql, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@UserID", _selectedUserId);

                        int rowsAffected = deleteCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            AuditLogger.Log("DELETE_USER",
                                $"User '{selectedUsername}' deleted by {AppSession.CurrentUsername}.");

                            MessageBox.Show("User deleted successfully.",
                                "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadUsers();
                        }
                        else
                        {
                            MessageBox.Show("Delete failed. The record may no longer exist.",
                                "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ApplyTheme()
        {
            BackColor = AppTheme.BackgroundColor;
            btnAdd.BackColor = AppTheme.SuccessColor;
            btnAdd.ForeColor = Color.White;
            btnEdit.BackColor = AppTheme.PrimaryColor;
            btnEdit.ForeColor = Color.White;
            btndelete.BackColor = AppTheme.DangerColor;
            btndelete.ForeColor = Color.White;
            btnClose.BackColor = AppTheme.SecondaryColor;
            btnClose.ForeColor = Color.White;

            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryColor;
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsers.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = AppTheme.GridRowAlt;
        }
    }
}
