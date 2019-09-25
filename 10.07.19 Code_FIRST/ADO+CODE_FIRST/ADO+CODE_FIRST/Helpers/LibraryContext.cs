using ADO_CODE_FIRST.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_CODE_FIRST.Helpers
{
    class LibraryContext:DbContext
    {        
        //это будет связующий класс 

        //это наша строка подключения
        public LibraryContext() : base("library")
        {

        }
        public DbSet<Acount> Acounts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Publisher> Publishers { get; set; }




    }
}
