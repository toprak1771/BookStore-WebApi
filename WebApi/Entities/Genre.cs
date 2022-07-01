using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{

    public class Genre
    {
        public Genre()
        {
            Books=new List<Book>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; } = true;

        public List<Book> Books {get; set;}
    }
}