using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DBF
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentDB studentDB = new StudentDB();
            studentDB.Show();

            //studentDB.PersonInsert(
            //    new Person
            //    {
            //        LastName = "Doe",
            //        FirstName = "John",
            //        BirthDate = new DateTime(1987, 4, 12)
            //    },
            //    "31ПР12");

            Console.ReadKey();
        }
    }
}
