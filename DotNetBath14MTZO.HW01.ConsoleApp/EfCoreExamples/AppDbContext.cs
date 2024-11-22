using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBath14MTZO.HW01.ConsoleApp.EfCoreExamples
{
    public class AppDbContext : DbContext
    {


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AppSettings.SqlConnectionStringBuilder.ConnectionString);
            }

        }

        public DbSet<TblBlog>BlogTable { get; set; }

        [Table("Tbl_Blog")]
        public class TblBlog
        {
            [Key]
            [Column("BlogId")]
            public string Id { get; set; }
            [Column("BlogTitle")]
            public string Titel { get; set; }
            [Column("BlogAuthor")]
            public string Author { get; set; }
            [Column("BlogContent")]
            public string Content { get; set; }

        }

    }
}
