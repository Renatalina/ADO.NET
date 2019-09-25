using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_LINQU
{
    interface ISome
    {
        void M();
    }

    class Student : ISome
    {
        public enum Professions { Doctor, Programmer, Tester,
            Engineer, Teacher }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsMarried { get; set; }
        byte[] marks = new byte[10];
        public Professions Profession { get; set; }
        public byte[] Marks { get { return marks; }
            set { marks = value; } }

        public decimal GetAverage()
        {
            double aver = 0;
            foreach (var v in Marks)
                aver += v;
            return (decimal)aver / Marks.Length;
        }

        public override string ToString()
        {
            return String.Format("Name: {0}, age: {1}," +
                " profession: {2}, isMarried: {3}," +
                " marksAverage: {4}", Name, Age, Profession,
                IsMarried, Math.Round(GetAverage(), 2).ToString());
        }

        public void M()
        {
            Console.WriteLine("Hello");
        }
    }
    class StudentComparer : IEqualityComparer<Student>
    {
        public  bool Equals(Student x, Student y)
        {
            return x.ToString()==y.ToString();
        }

        public int GetHashCode(Student obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>
            {
                new Student{ Name = "Nastya", Age = 30, IsMarried = true, Profession = Student.Professions.Teacher, Marks = new Byte[]{10,8,7,11,3,8,11,12}},
                new Student{ Name = "Nastya", Age = 30, IsMarried = true, Profession = Student.Professions.Teacher, Marks = new Byte[]{10,8,7,11,3,8,11,12}},
                new Student{ Name = "Nastya", Age = 30, IsMarried = true, Profession = Student.Professions.Teacher, Marks = new Byte[]{10,8,7,11,3,8,11,12}},
                new Student {Name = "Ira", Age = 18, IsMarried = false, Profession = Student.Professions.Doctor, Marks = new Byte[]{6,8,7,2,3,8,6,3}},
                new Student { Name = "Dima", Age = 34, IsMarried = true, Profession = Student.Professions.Programmer, Marks = new Byte[]{10,12,12,11,12,12,11,12}},
                new Student { Name = "Timur", Age = 25, IsMarried = false, Profession = Student.Professions.Tester, Marks = new Byte[]{7,8,7,7,7,8,7,7}},
                new Student { Name = "Kolya", Age = 23, IsMarried = true, Profession = Student.Professions.Doctor, Marks = new Byte[]{5,5,5,3,3,5,5,5}},
                new Student { Name = "Oleg", Age = 31, IsMarried = true, Profession = Student.Professions.Tester, Marks = new Byte[]{9,9,9,7,9,8,9,9}},
                new Student { Name = "Sergey", Age = 29, IsMarried = true, Profession = Student.Professions.Programmer, Marks = new Byte[]{11,11,11,11,11,8,11,11}}
            };

            
            List<Student> students1 = new List<Student>
            {
                new Student{ Name = "Roma", Age = 30, IsMarried = true, Profession = Student.Professions.Teacher, Marks = new Byte[]{10,8,7,11,3,8,11,12}},
                new Student {Name = "Garic", Age = 18, IsMarried = false, Profession = Student.Professions.Doctor, Marks = new Byte[]{6,8,7,2,3,8,6,3}},
                new Student { Name = "Lyosha", Age = 34, IsMarried = true, Profession = Student.Professions.Programmer, Marks = new Byte[]{10,12,12,11,12,12,11,12}},
                 new Student { Name = "Timur", Age = 25, IsMarried = false, Profession = Student.Professions.Tester, Marks = new Byte[]{7,8,7,7,7,8,7,7}},
                  new Student { Name = "Tom", Age = 23, IsMarried = true, Profession = Student.Professions.Doctor, Marks = new Byte[]{5,5,5,3,3,5,5,5}},
                  new Student { Name = "Oleg", Age = 31, IsMarried = true, Profession = Student.Professions.Tester, Marks = new Byte[]{9,9,9,7,9,8,9,9}},
                  new Student { Name = "Sergey", Age = 29, IsMarried = true, Profession = Student.Professions.Programmer, Marks = new Byte[]{11,11,11,11,11,8,11,11}}

            };

             List<Student> students2 = new List<Student>
            {
                new Student{ Name = "Roma", Age = 30, IsMarried = true, Profession = Student.Professions.Teacher, Marks = new Byte[]{10,8,7,11,3,8,11,12}},
                new Student {Name = "Garic1", Age = 18, IsMarried = false, Profession = Student.Professions.Doctor, Marks = new Byte[]{6,8,7,2,3,8,6,3}},
                new Student { Name = "Lyosha1", Age = 34, IsMarried = true, Profession = Student.Professions.Programmer, Marks = new Byte[]{10,12,12,11,12,12,11,12}},
                 new Student { Name = "Timur1", Age = 25, IsMarried = false, Profession = Student.Professions.Tester, Marks = new Byte[]{7,8,7,7,7,8,7,7}},
                  new Student { Name = "tom1", Age = 23, IsMarried = true, Profession = Student.Professions.Doctor, Marks = new Byte[]{5,5,5,3,3,5,5,5}},
                  new Student { Name = "Oleg1", Age = 31, IsMarried = true, Profession = Student.Professions.Tester, Marks = new Byte[]{9,9,9,7,9,8,9,9}},
                  new Student { Name = "Sergey1", Age = 29, IsMarried = true, Profession = Student.Professions.Programmer, Marks = new Byte[]{11, 11,11,11,11,8,11,11}}

            };


            //1. Выбрать всех по имени Dima.
            var result = students.Where(s => s.Name == "Dima");
            foreach(var item in result)
            {
                Console.WriteLine(item);
            }

            //2. Выбрать всех в возрасте от 24 до 30.
            var result2 = students.Where(s => s.Age > 24 && s.Age < 30);
            foreach (var item in result2)
            {
                Console.WriteLine(item);
            }

            ////3. Выбрать всех, у кого оценки не ниже 7 баллов по 
            //профессии тестировщик.

            var result3 = students.Where
                (s => s.Profession == Student.Professions.Tester && s.Marks.Min() >= 7).OrderBy(p => p.Name).OrderByDescending(d => d.Age).
                Select(s => $"{s.Name}{s.Age}{s.Profession}");
            //Linque только он организует коллекцию IEnumerable
            Console.WriteLine(students.All(s=>s is IDisposable));
            Console.WriteLine(students.Any(s => s.Age==35 ));

            //Cast приводит к IEnumerable
            IEnumerable<ISome> somes = students.Cast<ISome>();

            foreach (var item in result3)
            {
                Console.WriteLine(item);
            }

            //4. Объединить две последовательности.
            //объединит две коллекции студентов в одну коллекцию
            var concat = students.Concat(students1);
            var union = students.Union(students1);

            //5. Определить, содержит ли последовательность указанный элемент.
            concat.Contains(new Student
            {
                Name = "Sergey",
                Age = 29,
                IsMarried = true,
                Profession = Student.Professions.Programmer,
                Marks = new byte[] { 11, 10, 12, 11, 10, 11, 10, 11 }
            });

            Student st = new Student
            {
                Name = "Rammir",
                Age = 40,
                IsMarried = false,
                Profession = Student.Professions.Engineer,
                Marks = new byte[] { 6, 8, 9, 7, 6, 5, 8, 9, 12 }
            };
            //вот здесь он присоединит все в большую коллекции
            List<Student> contains = concat.ToList();
            contains.Add(st);


            Console.WriteLine(contains.Contains(st));

            //7.Получить число элементов, удовлетворяющих заданному условию.
            Console.WriteLine(concat.Count(s => s.Name == "Nastya"));

            //8. Убрать из последовательности все повторяющиеся элементы.
            Console.WriteLine($"Убрать из последовательности все повторяющиеся элементы.");
           var distinct = concat.Distinct(new StudentComparer());

            foreach(var item in distinct)
            {
                Console.WriteLine(item);
            }

            //9. Вернуть элемент по указанному индексу в последовательности.
            Console.WriteLine(distinct.ElementAt(1));

            //10. Вернуть элемент по указанному индексу в последовательности или значение по умолчанию, если индекс вне допустимого диапазона.

            Student student23 = distinct.ElementAtOrDefault(1);
            Console.WriteLine(student23!=null ? student23.ToString():"Student null");

            //11. Получить все элементы первой последовательности, которых нет во второй.
            Console.WriteLine($"Получить все элементы первой последовательности, которых нет во второй.");
            var except = distinct.Except(students2);
            foreach(var item in except)
            {
                Console.WriteLine(item);
            }

            //12. Получить первый элемент последовательности, который удовлетворяет указанному условию.
            //здесь опасно!!! здесь если не находит, то выбрасывает исключение
            //поэтому для них нужно делать трай кетч
            Console.WriteLine(distinct.First(s => s.Name == "Tom"));
            try
            {
                Console.WriteLine(distinct.First(s => s.Name == "fds"));
            }
            catch { }

            //13.Получить первый элемент последовательности или значение по умолчанию, если последовательность не содержит элементов.
            Console.WriteLine($"13. Получить первый элемент последовательности или значение по умолчанию, если последовательность не содержит элементов.");
            Student student = distinct.FirstOrDefault(s => s.Name=="Oleg");
            if(student!=null)
            {
                Console.WriteLine(student);
            }

            //14. Получить группу студентов, которые женаты.
            Console.WriteLine($"14. Получить группу студентов, которые женаты.");
            IEnumerable<IGrouping<bool,Student>> groupBy = distinct.GroupBy(s => s.IsMarried);

            foreach(IGrouping<bool,Student> item in groupBy)
            {
                Console.WriteLine(item.Key);
                foreach (Student value in item)
                {
                     //Console.WriteLine($"{value.Name}");
                    //или
                    Console.WriteLine(value.ToString());
                }
            }
            //15. Получить элементы - общие для двух последовательностей (пересечение множеств, представленных двумя последовательностями).
            var intersect = concat.Intersect(students1);
            Console.WriteLine($"15. Получить элементы - общие для двух последовательностей" +
                $" (пересечение множеств, представленных двумя последовательностями).");
            foreach (var item in intersect)
            {
                Console.WriteLine(item);
            }


            Console.ReadLine();       
           

        }
    }
}
