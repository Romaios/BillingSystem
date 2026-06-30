using BillingSystem.Database;
using MySql.Data.MySqlClient;

namespace BillingSystem.Utils
{
    public static class CustomerArchiveService
    {
        public static void EnsureArchiveColumnExists()
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                string countSql = @"SELECT COUNT(*)
                                    FROM   INFORMATION_SCHEMA.COLUMNS
                                    WHERE  TABLE_SCHEMA = @TableSchema
                                      AND  TABLE_NAME = 'Customers'
                                      AND  COLUMN_NAME = 'IsArchived';";

                using (var countCmd = new MySqlCommand(countSql, conn))
                {
                    countCmd.Parameters.AddWithValue("@TableSchema", conn.Database);

                    long existingCount = Convert.ToInt64(countCmd.ExecuteScalar());
                    if (existingCount > 0)
                        return;
                }

                string alterSql = @"ALTER TABLE Customers
                                    ADD COLUMN IsArchived TINYINT(1) NOT NULL DEFAULT 0;";

                using (var alterCmd = new MySqlCommand(alterSql, conn))
                {
                    alterCmd.ExecuteNonQuery();
                }
            }
        }

        public static bool SetArchivedState(int customerId, bool isArchived)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                string sql = @"UPDATE Customers
                               SET    IsArchived = @IsArchived
                               WHERE  CustomerID = @CustomerID;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IsArchived", isArchived ? 1 : 0);
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
