using BookTest.Data.Interfaces;
using System.ComponentModel.DataAnnotations;


namespace BookTest.Data.Entities
{
    public class Book : IEntity
    {
        public int Id { get; set; }

        [MaxLength(80), Required] //Could make this a longer string but 80 should be enough for most titles
        public string? Title { get; set; }
                
        public DateTime? PublicationDate { get; set; }

        public ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
