using BillingSystem.Database;
using BillingSystem.Utils;
using MySql.Data.MySqlClient;

namespace BillingSystem
{
    public partial class AddUserForm : Form
    {
        private readonly int _editUserId;
        private string _loadedUsername = string.Empty;

        public AddUserForm()
        {
            InitializeComponent();
            FormResizer.Enable(this);
        }

        public AddUserForm(int userId) : this()
        {
            _editUserId = userId;
        }

        private bool IsEditMode => _editUserId > 0;

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            cmbRole.Items.Clear();
            cmbRole.Items.AddRange(new object[] { "Admin", "Cashier" });
            cmbRole.SelectedIndex = 0;

            ApplyModeState();

            if (IsEditMode)
            {
                LoadUserData();
            }
        }

        private void ApplyModeState()
        {
            if (IsEditMode)
            {
                lblTitle.Text = "Edit User";
                Text = "Edit User";
                txtUsername.ReadOnly = true;
                txtUsername.BackColor = SystemColors.Control;
                txtPassword.Visible = true;
                txtPassword.ReadOnly = true;
                txtPassword.TabStop = false;
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = "Use Change Password form";
                txtPassword.BackColor = SystemColors.Control;
                lblEditHint.Visible = true;
            }
            else
            {
                lblTitle.Text = "Add User";
                Text = "Add User";
                txtUsername.ReadOnly = false;
                txtUsername.BackColor = Color.White;
                lblPassword.Visible = true;
                txtPassword.Visible = true;
                txtPassword.ReadOnly = false;
                txtPassword.TabStop = true;
                txtPassword.UseSystemPasswordChar = true;
                txtPassword.Text = string.Empty;
                txtPassword.BackColor = Color.White;
                lblEditHint.Visible = false;
            }
        }

        private void LoadUserData()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string sql = @"SELECT Username, FullName, Role
                                   FROM   Users
                                   WHERE  UserID = @UserID;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", _editUserId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                AuditLogger.Log("EDIT_USER_RECORD_NOT_FOUND",
                                    $"UserID {_editUserId} was missing when {AppSession.CurrentUsername} opened Edit User.");
                                AppMessageBox.Show("The selected user could not be found.",
                                    "Edit User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                DialogResult = DialogResult.Cancel;
                                Close();
                                return;
                            }

                            _loadedUsername = reader.GetString("Username");
                            txtUsername.Text = _loadedUsername;
                            txtFullName.Text = reader.GetString("FullName");
                            cmbRole.SelectedItem = reader.GetString("Role");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppMessageBox.Show($"Error loading user data:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            if (IsEditMode)
            {
                UpdateUser();
            }
            else
            {
                InsertUser();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                AuditLogger.Log(IsEditMode ? "EDIT_USER_VALIDATION_FAILED" : "ADD_USER_VALIDATION_FAILED",
                    $"{AppSession.CurrentUsername} submitted the user form without a username.");
                AppMessageBox.Show("Username is required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (!IsEditMode && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                AuditLogger.Log("ADD_USER_VALIDATION_FAILED",
                    $"{AppSession.CurrentUsername} submitted the Add User form without a password.");
                AppMessageBox.Show("Password is required for new accounts.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                AuditLogger.Log(IsEditMode ? "EDIT_USER_VALIDATION_FAILED" : "ADD_USER_VALIDATION_FAILED",
                    $"{AppSession.CurrentUsername} submitted the user form without a full name.");
                AppMessageBox.Show("Full Name is required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (cmbRole.SelectedItem == null)
            {
                AuditLogger.Log(IsEditMode ? "EDIT_USER_VALIDATION_FAILED" : "ADD_USER_VALIDATION_FAILED",
                    $"{AppSession.CurrentUsername} submitted the user form without selecting a role.");
                AppMessageBox.Show("Please select a role.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRole.Focus();
                return false;
            }

            if (!IsEditMode && UsernameExists(txtUsername.Text.Trim()))
            {
                AuditLogger.Log("ADD_USER_DUPLICATE_USERNAME",
                    $"{AppSession.CurrentUsername} attempted to create duplicate username '{txtUsername.Text.Trim()}'.");
                AppMessageBox.Show("That username already exists. Please choose another one.",
                    "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            return true;
        }

        private bool UsernameExists(string username)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                string sql = @"SELECT COUNT(*)
                               FROM   Users
                               WHERE  Username = @Username;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    long matches = Convert.ToInt64(cmd.ExecuteScalar());
                    return matches > 0;
                }
            }
        }

        private void InsertUser()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string sql = @"INSERT INTO Users
                                      (Username, Password, FullName, Role)
                                   VALUES
                                      (@Username, @Password, @FullName, @Role);";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Role", cmbRole.SelectedItem!.ToString());

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            AuditLogger.Log("ADD_USER",
                                $"User '{txtUsername.Text.Trim()}' created by {AppSession.CurrentUsername}.");

                            AppMessageBox.Show("User created successfully.",
                                "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AuditLogger.Log("ADD_USER_ERROR",
                    $"{AppSession.CurrentUsername} encountered an error while creating user '{txtUsername.Text.Trim()}': {ex.Message}");
                AppMessageBox.Show($"Error creating user:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateUser()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string sql = @"UPDATE Users
                                   SET    FullName = @FullName,
                                          Role = @Role
                                   WHERE  UserID = @UserID;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Role", cmbRole.SelectedItem!.ToString());
                        cmd.Parameters.AddWithValue("@UserID", _editUserId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            AuditLogger.Log("EDIT_USER",
                                $"User '{txtUsername.Text}' updated by {AppSession.CurrentUsername}.");

                            AppMessageBox.Show("User updated successfully.",
                                "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        else
                        {
                            AuditLogger.Log("EDIT_USER_SAVE_FAILED",
                                $"Edit User did not update any rows for UserID {_editUserId} ({txtUsername.Text}).");
                            AppMessageBox.Show("Update failed. The record may no longer exist.",
                                "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AuditLogger.Log("EDIT_USER_ERROR",
                    $"{AppSession.CurrentUsername} encountered an error while updating user '{txtUsername.Text}': {ex.Message}");
                AppMessageBox.Show($"Error updating user:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string action = IsEditMode ? "CANCEL_EDIT_USER" : "CANCEL_ADD_USER";
            string target = IsEditMode ? _loadedUsername : txtUsername.Text.Trim();
            AuditLogger.Log(action,
                $"{AppSession.CurrentUsername} cancelled {(IsEditMode ? "Edit User" : "Add User")}" +
                $"{(string.IsNullOrWhiteSpace(target) ? "." : $" for '{target}'.")}");
            Close();
        }
    }
}

