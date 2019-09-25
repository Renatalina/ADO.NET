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
                    LastName = "Lansakaya",
                    FirstName = "Natalina",
                    Birthday = new DateTime(1986, 2, 14),
                    GroupId = 1
                };

                //добавили нашего студента
                studentsDB.PersonInsert(person);

                //посмотрели нашего студента
                //studentsDB.SelectDB("SELECT * FROM Persons;");
                //studentsDB.PersonDelete(7);
                studentsDB.PersonUpdate(8, "Beam");
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
