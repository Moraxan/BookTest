namespace BookTest.Data.Entities;
public class AuthorBook
{
    public int AuthorId { get; set; }
    public virtual Author? Author { get; set; } // Made virtual for lazy loading

    public int BookId { get; set; }
    public virtual Book? Book { get; set; } // Made virtual for lazy loading
}

