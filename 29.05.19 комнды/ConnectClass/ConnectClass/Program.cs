using ConnectClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectClass
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentsDB studentsDB = new StudentsDB();
            //Console.WriteLine(studentsDB.ConnectionString);
            try
            {
                //studentsDB.SelectDB("SELECT * FROM Persons;");
                Person person = new Person
                {
                    LastName = "Miller",
                    FirstName = "Glenn",
                    Birthday = new DateTime(1997, 6, 18),
                    GroupId = 2
                };

                //добавили нашего студента
                //studentsDB.PersonInsert(person);

                //посмотрели нашего студента
                //studentsDB.SelectDB("SELECT * FROM Persons;");
                studentsDB.PersonDelete(7);
                studentsDB.SelectDB("SELECT * FROM Persons;");
                Console.WriteLine(studentsDB.RecordCount("Persons"));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
