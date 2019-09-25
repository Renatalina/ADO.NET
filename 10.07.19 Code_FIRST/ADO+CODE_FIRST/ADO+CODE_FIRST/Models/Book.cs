using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_CODE_FIRST.Models
{
    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        //связь многие ко многим
        public int MyProperty { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        public virtual ICollection<Publisher> Publishers { get; set; }

        public Book()
        {
            Authors = new List<Author>();
            Publishers = new List<Publisher>();
        }

    }
}
