using Dapper;
using DotNetBath14MTZO.HW01.ConsoleApp.DapperExamples.BlogDtos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBath14MTZO.HW01.ConsoleApp.DapperExamples
{
    public class DapperExample
    {
       private readonly  string _connectionString = AppSettings.SqlConnectionStringBuilder.ConnectionString;

        public void Read()
        {
            using IDbConnection  connection = new SqlConnection ( _connectionString );
            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Tbl_Blog]";

           List<BlogDto>lst = connection.Query<BlogDto>(query).ToList();

            foreach (BlogDto item in lst) {
                Console.WriteLine("Blog Id "+ item.BlogId);
                Console.WriteLine("Blog Title " + item.BlogTitle);
                Console.WriteLine("Blog Author " + item.BlogAuthor);
                Console.WriteLine("Blog Content " + item.BlogContent);
            }
        }
        public void Edit(string id)
        {
            string query = $@"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Tbl_Blog] where [BlogId]='{id}'";
            using IDbConnection dbConnection = new SqlConnection ( _connectionString );
            var item = dbConnection.Query<BlogDto>(query).FirstOrDefault();
            if ( item is null)
            {
                Console.WriteLine("Data not found");
                return;
            }
            Console.WriteLine("Blog Id " + item.BlogId);
            Console.WriteLine("Blog Title " + item.BlogTitle);
            Console.WriteLine("Blog Author " + item.BlogAuthor);
            Console.WriteLine("Blog Content " + item.BlogContent);


        }

        public void Create (string title,string author,string content)
        {
            using IDbConnection dbConnection = new SqlConnection (_connectionString );
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
            ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           ('{title}'
            ,'{author}'
            ,'{content}')";
            int result = dbConnection.Execute(query);
            string message = result > 0 ? "Cteate is Successful! " : "Create is Failed !";
            Console.WriteLine(message);
        }

        public void Update(string id, string title, string author, string content)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogId] = '{id}'
      ,[BlogTitle] ='{title}'
      ,[BlogAuthor] = '{author}'
      ,[BlogContent] ='{content}'
 WHERE [BlogId]= '{id}'";

            int result = dbConnection.Execute(query);
            string message = result > 0 ? "Update is successfeul" : "Update is Failed";
            Console.WriteLine(message);
        }

        public void Delete(string id)
        {
            using IDbConnection connection= new SqlConnection(_connectionString);
            int result = connection.Execute($"delete from Tbl_Blog where [BlogId]= '{id}'");
            string message = result > 0 ? "Delete is successful " : "Delete is failed";
            Console.WriteLine(message);
        }
    }
}
