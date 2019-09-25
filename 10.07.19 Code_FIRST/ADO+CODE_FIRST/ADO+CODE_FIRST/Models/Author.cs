using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_CODE_FIRST.Models
{
    class Author
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        //здесь будет наш Foreign Key
        public int CountryId { get; set; }

        //это все для ленивой загрузки
        public virtual Country Country { get; set; }



        //связь многие ко многим с Книгами
        public virtual ICollection<Book> Books {get; set; }

        public virtual Acount Acount { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }

        //нужно переопределить то стринг, что бы красиво выводить на экран, а не обращаться к каждому по отдельности
        public override string ToString()
        {
            return  $"{LastName} {FirstName}";
        }



    }
}
