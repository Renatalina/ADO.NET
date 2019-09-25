using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_CODE_FIRST.Models
{
    class Acount
    {
        [Key]
        [ForeignKey("Author")]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public virtual Author Author { get; set; }       

    }
}
