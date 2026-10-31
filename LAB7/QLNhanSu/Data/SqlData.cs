
using System;
using System.Data;
using System.Data.SqlClient;

namespace QLNhanSu.Data
{
    public class SqlData
    {
        // THAY TEN SERVER CHO DUNG VOI SSMS CUA BAN
        private readonly string connectionString =
            @"Data Source=localhost\SQLEXPRESS;
              Initial Catalog=QLNhanSu;
              Integrated Security=True;
              TrustServerCertificate=True;";

        // Doc du lieu cua mot bang
        public DataTable LayDuLieu(string tenBang)
        {
            using (SqlConnection conn =
                new SqlConnection(connectionString))
            {
                string sql =
                    "SELECT * FROM dbo.[" + tenBang + "]";

                using (SqlDataAdapter adapter =
                    new SqlDataAdapter(sql, conn))
                {
                    adapter.MissingSchemaAction =
                        MissingSchemaAction.AddWithKey;

                    DataTable dt = new DataTable(tenBang);
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }

        // Luu cac thay doi: them, sua, xoa
        public int LuuDuLieu(
            string tenBang, DataTable dt)
        {
            using (SqlConnection conn =
                new SqlConnection(connectionString))
            {
                string sql =
                    "SELECT * FROM dbo.[" + tenBang + "]";

                using (SqlDataAdapter adapter =
                    new SqlDataAdapter(sql, conn))
                {
                    adapter.MissingSchemaAction =
                        MissingSchemaAction.AddWithKey;

                    using (SqlCommandBuilder builder =
                        new SqlCommandBuilder(adapter))
                    {
                        adapter.InsertCommand =
                            builder.GetInsertCommand();

                        adapter.UpdateCommand =
                            builder.GetUpdateCommand();

                        adapter.DeleteCommand =
                            builder.GetDeleteCommand();

                        return adapter.Update(dt);
                    }
                }
            }
        }
    }
}
