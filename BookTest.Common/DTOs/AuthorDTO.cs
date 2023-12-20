
namespace BookTest.Common.DTOs;

public class Author
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public ICollection<BookDTO>? Books { get; set; }
}
