using BookTest.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTest.Data.Contexts
{
    public class BookContext : DbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }

        DbSet<AuthorBook> AuthorBooks { get; set; }

        DbSet<Quotation> Quotations { get; set; }

        DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Use Lazy Loading
            optionsBuilder.UseLazyLoadingProxies();
            // Specify the database to use (e.g., SQL Server)
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=BookTest;Trusted_Connection=True");

        }
        public BookContext(DbContextOptions <BookContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Many to many relationship between books and authors
            modelBuilder.Entity<AuthorBook>()
                .HasKey(ab => new { ab.AuthorId, ab.BookId });

            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(ab => ab.BookId);

            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Author)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(ab => ab.AuthorId);

            //SeedData(modelBuilder); //Uncomment this line to seed the database with the data below    

        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            //Add Seedata for 5 authors and 5 books
            var authors = new List<Author>
            {
                new () { Id = 2, Name = "J.R.R. Tolkien" },
                new () { Id = 1, Name = "J.K. Rowling" },
                new () { Id = 3, Name = "George R.R. Martin" },
                new () { Id = 4, Name = "Stephen King" },
                new () { Id = 5, Name = "J.D. Salinger" }
            };
            modelBuilder.Entity<Author>()
                .HasData(authors);
            //Add Seedata for 2 books per author
            var books = new List<Book>
            {
                new () { Id = 1, Title = "Harry Potter and the Philosopher's Stone", PublicationDate = new DateTime(1997, 6, 26) },
                new () { Id = 2, Title = "Harry Potter and the Chamber of Secrets", PublicationDate = new DateTime(1998, 7, 2) },
                new () { Id = 3, Title = "The Lord of the Rings: The Fellowship of the Ring", PublicationDate = new DateTime(1954, 7, 29) },
                new () { Id = 4, Title = "The Lord of the Rings: The Two Towers", PublicationDate = new DateTime(1954, 11, 11) },
                new () { Id = 5, Title = "A Game of Thrones", PublicationDate = new DateTime(1996, 8, 1) },
                new () { Id = 6, Title = "A Clash of Kings", PublicationDate = new DateTime(1998, 11, 16) },
                new () { Id = 7, Title = "The Shining", PublicationDate = new DateTime(1977, 1, 28) },
                new () { Id = 8, Title = "The Stand", PublicationDate = new DateTime(1978, 10, 3) },
                new () { Id = 9, Title = "The Catcher in the Rye", PublicationDate = new DateTime(1951, 7, 16) },
                new () { Id = 10, Title = "Nine Stories", PublicationDate = new DateTime(1953, 5, 1) }
            };
            modelBuilder.Entity<Book>()
                .HasData(books);
            //Add connection between authors and books
            var authorBooks = new List<AuthorBook>
            {
                new () { AuthorId = 1, BookId = 1 },
                new () { AuthorId = 1, BookId = 2 },
                new () { AuthorId = 2, BookId = 3 },
                new () { AuthorId = 2, BookId = 4 },
                new () { AuthorId = 3, BookId = 5 },
                new () { AuthorId = 3, BookId = 6 },
                new () { AuthorId = 4, BookId = 7 },
                new () { AuthorId = 4, BookId = 8 },
                new () { AuthorId = 5, BookId = 9 },
                new () { AuthorId = 5, BookId = 10 }
            };
            modelBuilder.Entity<AuthorBook>()
                .HasData(authorBooks);
           
        }
    }
}
