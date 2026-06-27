using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BillingSystem.Database;
using BillingSystem.Utils;

namespace BillingSystem
{
    public partial class frmChangePassword : Form
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }

        // Cancel — close the form without making any changes.
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string current = txtCurrentPassword.Text;
            string newPass = txtNewPassword.Text;
            string retype = txtRetypePassword.Text;

            // --- 0. Make sure nothing was left blank ---
            if (string.IsNullOrWhiteSpace(current) ||
                string.IsNullOrWhiteSpace(newPass) ||
                string.IsNullOrWhiteSpace(retype))
            {
                MessageBox.Show("Please fill in all three password fields.",
                    "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // --- 1. Current Password check ---
                // Verify the typed current password matches what is stored for THIS user,
                // identified by the logged-in user's unique ID (never a typed username).
                if (!CurrentPasswordIsValid(current))
                {
                    MessageBox.Show("Your current password is incorrect.",
                        "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCurrentPassword.Clear();
                    txtCurrentPassword.Focus();
                    return;
                }

                // --- 2. Password Match check ---
                if (newPass != retype)
                {
                    MessageBox.Show("New Password and Retype Password do not match.",
                        "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRetypePassword.Clear();
                    txtRetypePassword.Focus();
                    return;
                }

                // --- 3. Password Complexity check ---
                // All unmet rules are reported together in a single message.
                List<string> unmet = GetUnmetComplexityRules(newPass);
                if (unmet.Count > 0)
                {
                    string message = "Your new password does not meet the following requirement(s):\n\n"
                        + "  • " + string.Join("\n  • ", unmet);
                    MessageBox.Show(message,
                        "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // --- 4. Final Save — only reached when every validation passed ---
                SavePassword(newPass);

                AuditLogger.Log("CHANGE_PASSWORD",
                    $"{AppSession.CurrentUsername} changed their own password.");

                MessageBox.Show("Password changed successfully.",
                    "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing password:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Returns true if 'current' matches the stored password for the logged-in user.
        private bool CurrentPasswordIsValid(string current)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                // Parameterized — match on the unique UserID AND the typed current password.
                string sql = @"SELECT COUNT(*)
                               FROM   Users
                               WHERE  UserID   = @UserID
                                 AND  Password = @Password;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", AppSession.CurrentUserID);
                    cmd.Parameters.AddWithValue("@Password", current);

                    long matches = Convert.ToInt64(cmd.ExecuteScalar());
                    return matches > 0;
                }
            }
        }

        // Updates ONLY the logged-in user's row, identified by their unique UserID.
        private void SavePassword(string newPass)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                string sql = @"UPDATE Users
                               SET    Password = @Password
                               WHERE  UserID   = @UserID;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Password", newPass);
                    cmd.Parameters.AddWithValue("@UserID", AppSession.CurrentUserID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Checks the 5 complexity rules and returns a list of the ones NOT satisfied.
        private List<string> GetUnmetComplexityRules(string password)
        {
            var unmet = new List<string>();

            if (password.Length < 8)
                unmet.Add("At least 8 characters long");
            if (!Regex.IsMatch(password, "[A-Z]"))
                unmet.Add("At least one uppercase letter (A-Z)");
            if (!Regex.IsMatch(password, "[a-z]"))
                unmet.Add("At least one lowercase letter (a-z)");
            if (!Regex.IsMatch(password, "[0-9]"))
                unmet.Add("At least one numeric digit (0-9)");
            if (!Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
                unmet.Add("At least one special character (e.g. !@#$%^&*)");

            return unmet;
        }
    }
}
