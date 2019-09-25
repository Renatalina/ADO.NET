using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSet2
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryDB libraryDB = new LibraryDB();

            libraryDB.PrintDataSet();
            
            Console.WriteLine();

           //libraryDB.AuthorAdd("John", "Doe");

           // libraryDB.AuthorDelete("John", "Doe");
            libraryDB.PrintRelation("Authors", "Books", "AuthBook");

            libraryDB.AuthorUpdate("Doe", "Smith");

            libraryDB.PrintDataSet();

            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
