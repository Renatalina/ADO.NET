using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_CODE_FIRST.Models
{
    class Country
    {
        //назвал Id и он САМ СДЕЛАЕТ из него ПЕРВИЧНЫЙ КЛЮЧ
        public int Id { get; set; }
        public string CountryName { get; set; }


        //в стране много авторов
        //virtual нужен для загрузки, данные будут загружаться в момент обращения к этому свойству(когда необходимо)
        //здесь память пока не выделена, надо сделать new ICollection или в констраукторе обязательно выделить память
        public virtual ICollection <Author> Authors { get; set; }
        public Country ()
        {
            //вот здесь выделили память под наш лист public virtual ICollection <Author> Authors
            Authors = new List<Author>();
        }

    }
}
