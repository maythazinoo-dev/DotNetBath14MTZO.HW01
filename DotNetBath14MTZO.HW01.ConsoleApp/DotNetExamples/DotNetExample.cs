using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBath14MTZO.HW01.ConsoleApp.DotNetExamples
{
    public class DotNetExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "WalletDB",
            UserID = "sa",
            Password = "mtzoo@123",
            TrustServerCertificate = true
        };

        public void Read()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = "select * from Tbl_Blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("BlogId..." + dr["BlogId"]);
                Console.WriteLine("BlogTitle..." + dr["BlogTitle"]);
                Console.WriteLine("BlogAuthor..." + dr["BlogAuthor"]);
                Console.WriteLine("BlogContent..." + dr["BlogContent"]);
            }

        }
        public void Edit(string Id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = $"select * from Tbl_Blog where [BlogId]='{Id}'";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("Data Not Found!!!");
                return;
            }
            DataRow row = dt.Rows[0];
            Console.WriteLine("BlogId..." + row["BlogId"]);
            Console.WriteLine("BlogTitle..." + row["BlogTitle"]);
            Console.WriteLine("BlogAuthor..." + row["BlogAuthor"]);
            Console.WriteLine("BlogContent..." + row["BlogContent"]);

        }
        public void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ( [BlogTitle]
           , [BlogAuthor]
           , [BlogContent])
     VALUES
           ( '{title}'
           ,'{author}'
           ,'{content}') ";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Update Successful..." : "Update Failed...";
            Console.WriteLine(message);
        }

        public void Update(string id, String title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogId] = '{id}'
      ,[BlogTitle] = '{title}'
      ,[BlogAuthor] = '{author}'
      ,[BlogContent] = '{content}'
 WHERE [blogId]='{id}'";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Updating Successful...." : "Updating Failed ....";
            Console.WriteLine(message);
        }

        public void Delete(string Id) {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = $"DELETE FROM [dbo].[Tbl_Blog]  WHERE [BlogId]='{Id}'";
            SqlCommand cmd = new SqlCommand(query, connection);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Deleting Successful...." : "Deleting Failed";
            Console.WriteLine(message); 

        }
    }
}
