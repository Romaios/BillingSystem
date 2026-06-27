using BillingSystem.Database;
using MySql.Data.MySqlClient;

namespace BillingSystem.Utils
{
    /// <summary>
    /// Ensures new permission entries added in later activities exist in the
    /// database, even for older BillingDB copies created before those rows did.
    /// </summary>
    public static class PermissionInitializer
    {
        public static void EnsureManageUsersPermissionExists()
        {
            EnsurePermissionExists("Admin", "ManageUsers", true);
            EnsurePermissionExists("Cashier", "ManageUsers", false);
        }

        private static void EnsurePermissionExists(string role, string permissionName, bool isAllowed)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                string countSql = @"SELECT COUNT(*)
                                    FROM   UserPermissions
                                    WHERE  Role = @Role
                                      AND  PermissionName = @PermissionName;";

                using (var countCmd = new MySqlCommand(countSql, conn))
                {
                    countCmd.Parameters.AddWithValue("@Role", role);
                    countCmd.Parameters.AddWithValue("@PermissionName", permissionName);

                    long existingCount = Convert.ToInt64(countCmd.ExecuteScalar());
                    if (existingCount > 0)
                        return;
                }

                string insertSql = @"INSERT INTO UserPermissions
                                        (Role, PermissionName, IsAllowed)
                                     VALUES
                                        (@Role, @PermissionName, @IsAllowed);";

                using (var insertCmd = new MySqlCommand(insertSql, conn))
                {
                    insertCmd.Parameters.AddWithValue("@Role", role);
                    insertCmd.Parameters.AddWithValue("@PermissionName", permissionName);
                    insertCmd.Parameters.AddWithValue("@IsAllowed", isAllowed ? 1 : 0);
                    insertCmd.ExecuteNonQuery();
                }
            }
        }
    }
}
