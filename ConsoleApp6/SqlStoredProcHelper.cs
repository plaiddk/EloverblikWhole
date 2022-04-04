using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ConsoleApp6
{
    class SqlStoredProcHelper
    {
        public static string ExecuteProc(string conn, string query)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return "succes";


        }
    }
}
