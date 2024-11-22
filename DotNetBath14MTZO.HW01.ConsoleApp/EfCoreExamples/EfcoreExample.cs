using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static DotNetBath14MTZO.HW01.ConsoleApp.EfCoreExamples.AppDbContext;

namespace DotNetBath14MTZO.HW01.ConsoleApp.EfCoreExamples
{
    public class EfcoreExample
    {
        private readonly AppDbContext _appDbContext = new AppDbContext();
        public void Read()
        {
            var list = _appDbContext.BlogTable.ToList();
            foreach ( var item in list)

            {
                Console.WriteLine("Blog Id" + item.Id);
                Console.WriteLine("Blog Title" + item.Titel);
                Console.WriteLine("Blog Author"+ item.Author);
                Console.WriteLine("Blog Content" + item.Content);
            }
        }

        public void Edit(string id)
        {
            var item = _appDbContext.BlogTable.FirstOrDefault(x => x.Id == id);
            if ( item is null)
            {
                Console.WriteLine("Data not found");
                return;
            }
            Console.WriteLine("Blog Id" + item.Id);
            Console.WriteLine("Blog Title" + item.Titel);
            Console.WriteLine("Blog Author" + item.Author);
            Console.WriteLine("Blog Content" + item.Content);

        }

        public void Create(string title, string author, string content)
        {
            var blog = new TblBlog()
            {
                Id = Guid.NewGuid().ToString(),
                Titel = title,
                Author = author,
                Content = content
            };
            _appDbContext.BlogTable.Add(blog);
            int result = _appDbContext.SaveChanges();
            string messge = result > 0 ? "Create is successful" : "Create is Failed";
            Console.WriteLine(messge);
        }
        public void Update(string id, string title, string author, string content)
        {
            var item = _appDbContext.BlogTable.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }
            item.Id = id;
            item.Titel = title;
            item.Author = author;
            item.Content= content;
            _appDbContext.Entry(item).State = EntityState.Modified;
            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Update is successful" : "Update is Failed";
            Console.WriteLine(message);
        }

        public void Delete(string id)
        {
            var item = _appDbContext.BlogTable.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                Console.WriteLine("No data not found");
                return;
            }
            
            _appDbContext.Entry(item).State = EntityState.Deleted;
            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Delete is successful" : "Delete is failed";
            Console.WriteLine(message);
        }
    }
}
