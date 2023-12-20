
namespace BookTest.Common.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public ICollection<Author>? AuthorBooks { get; set; }
    }
}
