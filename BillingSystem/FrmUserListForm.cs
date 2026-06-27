using BillingSystem.Database;
using BillingSystem.Utils;
using MySql.Data.MySqlClient;
using System.Data;

namespace BillingSystem
{
    public partial class FrmUserListForm : Form
    {
        private int _selectedUserId;
        private bool _ignoreSelection = true;

        public FrmUserListForm()
        {
            InitializeComponent();
            ConfigureDataGridView();
            FormResizer.Enable(this);
            Shown += FrmUserListForm_Shown;
        }

        private void FrmUserListForm_Load(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission(AppSession.CurrentRole, "ManageUsers"))
            {
                AuditLogger.Log("ACCESS_DENIED_USER_MANAGEMENT",
                    $"{AppSession.CurrentUsername} was denied access to User Management.");
                AppMessageBox.Show("You do not have permission to access User Management.",
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
            colUserId.DataPropertyName = "UserID";
            colUsername.DataPropertyName = "Username";
            colFullName.DataPropertyName = "FullName";
            colRole.DataPropertyName = "Role";
            colCreated.DataPropertyName = "Created";
            colCreated.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
        }

        private void LoadUsers()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string? createdColumnName = FindCreatedColumnName(conn);
                    string sql = GetUserListSql(createdColumnName);

                    using (var adapter = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvUsers.DataSource = dt;
                        lblTitle.Text = $"User Management ({dt.Rows.Count} user(s))";
                    }
                }

                ClearUserSelection();
            }
            catch (Exception ex)
            {
                AppMessageBox.Show($"Error loading users:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearUserSelection()
        {
            dgvUsers.ClearSelection();
            dgvUsers.CurrentCell = null;
            _selectedUserId = 0;
        }

        private static string? FindCreatedColumnName(MySqlConnection conn)
        {
            string[] allowedColumnNames =
            {
                "Created",
                "CreatedAt",
                "DateCreated",
                "CreatedOn",
                "Created_At",
                "Date_Created"
            };

            string sql = @"SELECT COLUMN_NAME
                           FROM   INFORMATION_SCHEMA.COLUMNS
                           WHERE  TABLE_SCHEMA = @TableSchema
                             AND  TABLE_NAME = 'Users';";

            var existingColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@TableSchema", conn.Database);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        existingColumns.Add(reader.GetString("COLUMN_NAME"));
                    }
                }
            }

            foreach (string candidate in allowedColumnNames)
            {
                if (existingColumns.Contains(candidate))
                    return candidate;
            }

            return null;
        }

        private static string GetUserListSql(string? createdColumnName)
        {
            return createdColumnName switch
            {
                "Created" => @"SELECT UserID,
                                      Username,
                                      FullName,
                                      Role,
                                      Created AS Created
                               FROM   Users
                               ORDER  BY Username ASC;",
                "CreatedAt" => @"SELECT UserID,
                                        Username,
                                        FullName,
                                        Role,
                                        CreatedAt AS Created
                                 FROM   Users
                                 ORDER  BY Username ASC;",
                "DateCreated" => @"SELECT UserID,
                                          Username,
                                          FullName,
                                          Role,
                                          DateCreated AS Created
                                   FROM   Users
                                   ORDER  BY Username ASC;",
                "CreatedOn" => @"SELECT UserID,
                                        Username,
                                        FullName,
                                        Role,
                                        CreatedOn AS Created
                                 FROM   Users
                                 ORDER  BY Username ASC;",
                "Created_At" => @"SELECT UserID,
                                         Username,
                                         FullName,
                                         Role,
                                         Created_At AS Created
                                  FROM   Users
                                  ORDER  BY Username ASC;",
                "Date_Created" => @"SELECT UserID,
                                           Username,
                                           FullName,
                                           Role,
                                           Date_Created AS Created
                                    FROM   Users
                                    ORDER  BY Username ASC;",
                _ => @"SELECT UserID,
                              Username,
                              FullName,
                              Role,
                              NULL AS Created
                       FROM   Users
                       ORDER  BY Username ASC;"
            };
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (_ignoreSelection) return;
            if (dgvUsers.CurrentRow == null) return;

            object? idValue = dgvUsers.CurrentRow.Cells["colUserId"].Value;
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
            AuditLogger.Log("OPEN_ADD_USER",
                $"{AppSession.CurrentUsername} opened the Add User form.");
            using (var form = new AddUserForm())
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
                AuditLogger.Log("EDIT_USER_NO_SELECTION",
                    $"{AppSession.CurrentUsername} clicked Edit User without selecting a record.");
                AppMessageBox.Show("Please select a user to edit.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AuditLogger.Log("OPEN_EDIT_USER",
                $"{AppSession.CurrentUsername} opened the Edit User form for UserID {_selectedUserId}.");
            using (var form = new AddUserForm(_selectedUserId))
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
                AuditLogger.Log("DELETE_USER_NO_SELECTION",
                    $"{AppSession.CurrentUsername} clicked Delete User without selecting a record.");
                AppMessageBox.Show("Please select a user to delete.",
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
                                AuditLogger.Log("DELETE_USER_RECORD_NOT_FOUND",
                                    $"UserID {_selectedUserId} was missing when {AppSession.CurrentUsername} attempted deletion.");
                                AppMessageBox.Show("The selected user no longer exists.",
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
                        AuditLogger.Log("DELETE_USER_BLOCKED_SELF",
                            $"{AppSession.CurrentUsername} was blocked from deleting their own account.");
                        AppMessageBox.Show("You cannot delete your own account while logged in.",
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
                                AuditLogger.Log("DELETE_USER_BLOCKED_LAST_ADMIN",
                                    $"{AppSession.CurrentUsername} was blocked from deleting the last Admin account '{selectedUsername}'.");
                                AppMessageBox.Show("You cannot delete the last remaining Admin account.",
                                    "Delete Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    DialogResult confirm = AppMessageBox.Show(
                        $"Are you sure you want to delete user '{selectedUsername}'?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirm != DialogResult.Yes)
                    {
                        AuditLogger.Log("DELETE_USER_CANCELLED",
                            $"{AppSession.CurrentUsername} cancelled deletion of user '{selectedUsername}'.");
                        return;
                    }

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

                            AppMessageBox.Show("User deleted successfully.",
                                "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadUsers();
                        }
                        else
                        {
                            AppMessageBox.Show("Delete failed. The record may no longer exist.",
                                "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppMessageBox.Show($"Error deleting user:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            AuditLogger.Log("CLOSE_USER_MANAGEMENT",
                $"{AppSession.CurrentUsername} closed User Management.");
            Close();
        }

        private void ApplyTheme()
        {
            BackColor = AppTheme.BackgroundColor;
            pnlTop.BackColor = AppTheme.PrimaryColor;
            pnlBottom.BackColor = Color.FromArgb(242, 242, 242);

            btnAddUser.BackColor = AppTheme.SuccessColor;
            btnAddUser.ForeColor = Color.White;
            btnEditUser.BackColor = AppTheme.PrimaryColor;
            btnEditUser.ForeColor = Color.White;
            btnDeleteUser.BackColor = AppTheme.DangerColor;
            btnDeleteUser.ForeColor = Color.White;
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

