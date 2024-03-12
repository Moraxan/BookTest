using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[ApiController]
[Route("api/DataSeed")]
public class DataSeedController : ControllerBase
{
    private readonly BookContext _context;

    public DataSeedController(BookContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("SeedDatabase")]
    public IActionResult SeedDatabase()
    {
       
        if (!_context.Authors.Any() || !_context.Books.Any() || !_context.AuthorBooks.Any() || !_context.Users.Any())
        {
            SeedData(_context);
            _context.SaveChanges();
            return Ok("Database has been seeded successfully.");
        }

        return Ok("Database seeding is not required or has already been completed.");
    }


    private static void SeedData(BookContext context)
    {
        if (!context.Authors.Any())
        {
            var authors = new List<Author>
        {
            new Author { Name = "J.K. Rowling" },
            new Author { Name = "J.R.R. Tolkien" },
            new Author { Name = "George R.R. Martin" },
            new Author { Name = "Stephen King" },
            new Author { Name = "J.D. Salinger" }
        };
            context.Authors.AddRange(authors);
            context.SaveChanges();
        }

        if (!context.Books.Any())
        {
            var books = new List<Book>
        {
            new Book { Title = "Harry Potter and the Philosopher's Stone", PublicationDate = new DateTime(1997, 6, 26) },
            new Book { Title = "Harry Potter and the Chamber of Secrets", PublicationDate = new DateTime(1998, 7, 2) },
            new Book { Title = "The Lord of the Rings: The Fellowship of the Ring", PublicationDate = new DateTime(1954, 7, 29) },
            new Book { Title = "The Lord of the Rings: The Two Towers", PublicationDate = new DateTime(1954, 11, 11) },
            new Book { Title = "A Game of Thrones", PublicationDate = new DateTime(1996, 8, 1) },
            new Book { Title = "A Clash of Kings", PublicationDate = new DateTime(1998, 11, 16) },
            new Book { Title = "The Shining", PublicationDate = new DateTime(1977, 1, 28) },
            new Book { Title = "The Stand", PublicationDate = new DateTime(1978, 10, 3) },
            new Book { Title = "The Catcher in the Rye", PublicationDate = new DateTime(1951, 7, 16) },
            new Book { Title = "Nine Stories", PublicationDate = new DateTime(1953, 5, 1) }
        };
            context.Books.AddRange(books);
            context.SaveChanges();
        }

        if (!context.AuthorBooks.Any())
        {
            var authorBooks = new List<AuthorBook>
        {
            new AuthorBook { AuthorId = context.Authors.FirstOrDefault(a => a.Name == "J.K. Rowling").Id, BookId = context.Books.FirstOrDefault(b => b.Title == "Harry Potter and the Philosopher's Stone").Id },
            new AuthorBook { AuthorId = context.Authors.FirstOrDefault(a => a.Name == "J.K. Rowling").Id, BookId = context.Books.FirstOrDefault(b => b.Title == "Harry Potter and the Chamber of Secrets").Id },
            new AuthorBook { AuthorId = context.Authors.FirstOrDefault(a => a.Name == "J.R.R. Tolkien").Id, BookId = context.Books.FirstOrDefault(b => b.Title == "The Lord of the Rings: The Fellowship of the Ring").Id },
            new AuthorBook { AuthorId = context.Authors.FirstOrDefault(a => a.Name == "J.R.R. Tolkien").Id, BookId = context.Books.FirstOrDefault(b => b.Title == "The Lord of the Rings: The Two Towers").Id },
            // Repeat for other author-book relationships
            new AuthorBook { AuthorId = context.Authors.FirstOrDefault(a => a.Name == "George R.R. Martin").Id, BookId = context.Books.FirstOrDefault(b => b.Title == "A Game of Thrones").Id },
            new AuthorBook { AuthorId = context.Authors.FirstOrDefault(a => a.Name == "George R.R. Martin").Id, BookId = context.Books.FirstOrDefault(b => b.Title == "A Clash of Kings").Id },
            new AuthorBook { AuthorId = context.Authors.FirstOrDefault(a => a.Name == "Stephen King").Id, BookId = context.Books.FirstOrDefault(b => b.Title == "The Shining").Id },
            new AuthorBook { AuthorId = context.Authors.FirstOrDefault(a => a.Name == "Stephen King").Id, BookId = context.Books.FirstOrDefault(b => b.Title == "The Stand").Id },
            new AuthorBook { AuthorId = context.Authors.FirstOrDefault(a => a.Name == "J.D. Salinger").Id, BookId = context.Books.FirstOrDefault(b => b.Title == "The Catcher in the Rye").Id },
            new AuthorBook { AuthorId = context.Authors.FirstOrDefault(a => a.Name == "J.D. Salinger").Id, BookId = context.Books.FirstOrDefault(b => b.Title == "Nine Stories").Id },
        };
            context.AuthorBooks.AddRange(authorBooks);
            context.SaveChanges();
        }

    }

}
