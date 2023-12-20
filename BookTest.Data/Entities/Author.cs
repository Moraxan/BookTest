using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTest.Data.Entities
{
    public class Author : IEntity
    {
        public int Id { get; set; }

        [MaxLength(80), Required] //Could make this a longer string but 80 should be enough for most names
        public string? Name { get; set; }

        public ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
