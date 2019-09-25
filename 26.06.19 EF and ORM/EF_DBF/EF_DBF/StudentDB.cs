using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DBF
{
    class StudentDB
    {
        StudentsEntities _studentsEntities;

        public StudentDB()
        {
            _studentsEntities = new StudentsEntities();
        }

        public void PersonInsert(Person person, string group)
        {
            try
            {
                Group gr = _studentsEntities.Groups.FirstOrDefault(g => g.GroupName == group);
                if (gr != null)
                {
                    person.GroupId = gr.Id;

                    _studentsEntities.Persons.Add(person);

                    _studentsEntities.SaveChanges();

                    Console.WriteLine("Insert OK!");
                }
                else
                {
                    Console.WriteLine("Group name incorrect!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Show()
        {
            foreach (Group item in _studentsEntities.Groups)
            {
                Console.WriteLine(item);
                foreach (Person person in item.Persons)
                {
                    Console.WriteLine(person);
                }
                Console.WriteLine("\n++++++++++++++++++\n");
            }
        }
    }
}
