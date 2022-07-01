using System;
using System.ComponentModel.DataAnnotations.Schema; 
using System.Collections.Generic;

namespace WebApi.Entities
{

    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BornDate {get; set;}
    }

}