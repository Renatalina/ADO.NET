using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_CODE_FIRST.Models
{
    class Publisher
    {
        public int Id { get; set; }
        public string PublisherName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public Publisher()
        {
            Books = new List<Book>();
        }
    }
}
