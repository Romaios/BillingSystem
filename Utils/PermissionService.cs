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
    }
}
