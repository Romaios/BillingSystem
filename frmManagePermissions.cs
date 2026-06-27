using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BillingSystem.Database;
using BillingSystem.Utils;

namespace BillingSystem
{
    public partial class frmManagePermissions : Form
    {
        // Maps each PermissionName to its checkbox control
        private Dictionary<string, CheckBox> _permCheckboxes;

        public frmManagePermissions()
        {
            InitializeComponent();
        }

        private void frmManagePermissions_Load(object sender, EventArgs e)
        {
            // Map PermissionName strings to their checkbox controls
            _permCheckboxes = new Dictionary<string, CheckBox>
            {
                { "AddCustomer",       chkAddCustomer    },
                { "EditCustomer",      chkEditCustomer   },
                { "DeleteCustomer",    chkDeleteCustomer },
                { "Analytics",         chkAnalytics      },
                { "ExportExcel",       chkExportExcel    },
                { "ExportPdf",         chkExportPdf      },
                { "AuditLogs",         chkAuditLogs      },
                { "ManageUsers",       chkManageUsers    },
                { "ChangePassword",    chkChangePassword },
            };

            EnsurePermissionRows();

            // Populate dropdown and load first role
            cmbRole.Items.AddRange(new object[] { "Admin", "Cashier" });
            cmbRole.SelectedIndex = 0;
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRole.SelectedItem != null)
                LoadPermissions(cmbRole.SelectedItem.ToString());
        }

        private void LoadPermissions(string role)
        {
            try
            {
                foreach (var checkbox in _permCheckboxes.Values)
                {
                    checkbox.Checked = false;
                }

                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT PermissionName, IsAllowed
                                   FROM   UserPermissions
                                   WHERE  Role = @Role;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Role", role);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string permName = reader.GetString("PermissionName");
                                bool isAllowed = reader.GetBoolean("IsAllowed");

                                if (_permCheckboxes.ContainsKey(permName))
                                    _permCheckboxes[permName].Checked = isAllowed;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading permissions:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbRole.SelectedItem == null) return;
            string role = cmbRole.SelectedItem.ToString();

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    foreach (var kvp in _permCheckboxes)
                    {
                        SavePermission(conn, role, kvp.Key, kvp.Value.Checked);
                    }
                }

                AuditLogger.Log("MANAGE_PERMISSIONS",
                    $"Permissions updated for role '{role}' by {AppSession.CurrentUsername}.");

                MessageBox.Show("Permissions saved successfully.",
                    "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving permissions:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EnsurePermissionRows()
        {
            string[] roles = { "Admin", "Cashier" };

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    foreach (string role in roles)
                    {
                        foreach (var permissionName in _permCheckboxes.Keys)
                        {
                            bool defaultAllowed =
                                role == "Admin" && permissionName == "ManageUsers";

                            EnsurePermissionRow(conn, role, permissionName, defaultAllowed);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error preparing permissions:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SavePermission(
            MySqlConnection conn,
            string role,
            string permissionName,
            bool isAllowed)
        {
            string updateSql = @"UPDATE UserPermissions
                                 SET    IsAllowed = @IsAllowed
                                 WHERE  Role = @Role
                                   AND  PermissionName = @PermName;";

            using (var updateCmd = new MySqlCommand(updateSql, conn))
            {
                updateCmd.Parameters.AddWithValue("@IsAllowed", isAllowed ? 1 : 0);
                updateCmd.Parameters.AddWithValue("@Role", role);
                updateCmd.Parameters.AddWithValue("@PermName", permissionName);

                int rowsAffected = updateCmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    return;
            }

            EnsurePermissionRow(conn, role, permissionName, isAllowed);
        }

        private void EnsurePermissionRow(
            MySqlConnection conn,
            string role,
            string permissionName,
            bool isAllowed)
        {
            string existsSql = @"SELECT COUNT(*)
                                 FROM   UserPermissions
                                 WHERE  Role = @Role
                                   AND  PermissionName = @PermName;";

            using (var existsCmd = new MySqlCommand(existsSql, conn))
            {
                existsCmd.Parameters.AddWithValue("@Role", role);
                existsCmd.Parameters.AddWithValue("@PermName", permissionName);

                long matches = Convert.ToInt64(existsCmd.ExecuteScalar());
                if (matches > 0)
                    return;
            }

            string insertSql = @"INSERT INTO UserPermissions
                                    (Role, PermissionName, IsAllowed)
                                 VALUES
                                    (@Role, @PermName, @IsAllowed);";

            using (var insertCmd = new MySqlCommand(insertSql, conn))
            {
                insertCmd.Parameters.AddWithValue("@Role", role);
                insertCmd.Parameters.AddWithValue("@PermName", permissionName);
                insertCmd.Parameters.AddWithValue("@IsAllowed", isAllowed ? 1 : 0);
                insertCmd.ExecuteNonQuery();
            }
        }
    }
}
