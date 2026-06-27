using BillingSystem.Database;
using BillingSystem.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BillingSystem
{
    public partial class AddUser : Form
    {
        private readonly int _editUserId;

        public AddUser()
        {
            InitializeComponent();
        }

        public AddUser(int userId) : this()
        {
            _editUserId = userId;
        }

        private bool IsEditMode => _editUserId > 0;

        private void AddUser_Load(object sender, EventArgs e)
        {
            cmbrole.Items.Clear();
            cmbrole.Items.AddRange(new object[] { "Admin", "Cashier" });
            cmbrole.SelectedIndex = 0;

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
                lbladduser.Text = "Edit User";
                Text = "Edit User";
                txtusername.ReadOnly = true;
                txtusername.BackColor = SystemColors.Control;
                lblpassword.Visible = false;
                txtpassword.Visible = false;
            }
            else
            {
                lbladduser.Text = "Add User";
                Text = "Add User";
                txtusername.ReadOnly = false;
                txtusername.BackColor = Color.White;
                lblpassword.Visible = true;
                txtpassword.Visible = true;
                txtpassword.UseSystemPasswordChar = true;
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
                                MessageBox.Show("The selected user could not be found.",
                                    "Edit User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                DialogResult = DialogResult.Cancel;
                                Close();
                                return;
                            }

                            txtusername.Text = reader.GetString("Username");
                            txtfullname.Text = reader.GetString("FullName");
                            cmbrole.SelectedItem = reader.GetString("Role");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data:\n{ex.Message}",
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
            if (string.IsNullOrWhiteSpace(txtusername.Text))
            {
                MessageBox.Show("Username is required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtusername.Focus();
                return false;
            }

            if (!IsEditMode && string.IsNullOrWhiteSpace(txtpassword.Text))
            {
                MessageBox.Show("Password is required for new accounts.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtpassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtfullname.Text))
            {
                MessageBox.Show("Full Name is required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtfullname.Focus();
                return false;
            }

            if (cmbrole.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbrole.Focus();
                return false;
            }

            if (!IsEditMode && UsernameExists(txtusername.Text.Trim()))
            {
                MessageBox.Show("That username already exists. Please choose another one.",
                    "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtusername.Focus();
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
                        cmd.Parameters.AddWithValue("@Username", txtusername.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", txtpassword.Text);
                        cmd.Parameters.AddWithValue("@FullName", txtfullname.Text.Trim());
                        cmd.Parameters.AddWithValue("@Role", cmbrole.SelectedItem!.ToString());

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            AuditLogger.Log("ADD_USER",
                                $"User '{txtusername.Text.Trim()}' created by {AppSession.CurrentUsername}.");

                            MessageBox.Show("User created successfully.",
                                "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating user:\n{ex.Message}",
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
                        cmd.Parameters.AddWithValue("@FullName", txtfullname.Text.Trim());
                        cmd.Parameters.AddWithValue("@Role", cmbrole.SelectedItem!.ToString());
                        cmd.Parameters.AddWithValue("@UserID", _editUserId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            AuditLogger.Log("EDIT_USER",
                                $"User '{txtusername.Text}' updated by {AppSession.CurrentUsername}.");

                            MessageBox.Show("User updated successfully.",
                                "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Update failed. The record may no longer exist.",
                                "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
