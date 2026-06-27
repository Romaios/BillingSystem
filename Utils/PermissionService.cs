using BillingSystem.Database;
using MySql.Data.MySqlClient;
using System;

namespace BillingSystem.Utils
{
    public static class PermissionService
    {
        public static bool HasPermission(string role, string permissionName)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                EnsureManageUsersPermissionRows(conn);

                string sql = @"SELECT IsAllowed
                               FROM   UserPermissions
                               WHERE  Role = @Role
                                 AND  PermissionName = @PermissionName
                               LIMIT 1;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Role", role);
                    cmd.Parameters.AddWithValue("@PermissionName", permissionName);

                    object? result = cmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                        return false;

                    return Convert.ToBoolean(result);
                }
            }
        }

        private static void EnsureManageUsersPermissionRows(MySqlConnection conn)
        {
            EnsurePermissionRow(conn, "Admin", "ManageUsers", true);
            EnsurePermissionRow(conn, "Cashier", "ManageUsers", false);
        }

        private static void EnsurePermissionRow(
            MySqlConnection conn,
            string role,
            string permissionName,
            bool isAllowed)
        {
            string existsSql = @"SELECT COUNT(*)
                                 FROM   UserPermissions
                                 WHERE  Role = @Role
                                   AND  PermissionName = @PermissionName;";

            using (var existsCmd = new MySqlCommand(existsSql, conn))
            {
                existsCmd.Parameters.AddWithValue("@Role", role);
                existsCmd.Parameters.AddWithValue("@PermissionName", permissionName);

                long existingRows = Convert.ToInt64(existsCmd.ExecuteScalar());
                if (existingRows > 0)
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
                insertCmd.Parameters.AddWithValue("@IsAllowed", isAllowed);
                insertCmd.ExecuteNonQuery();
            }
        }
    }
}
