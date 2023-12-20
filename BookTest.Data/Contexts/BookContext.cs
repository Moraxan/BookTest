using BookTest.Data.Entities;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Specify the database to use (e.g., SQL Server)
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BookTest;Trusted_Connection=True;");
        }
        public BookContext(DbContextOptions <BookContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Many to many relationship between books and authors
            modelBuilder.Entity<AuthorBook>()
                .HasKey(ab => new { ab.AuthorId, ab.BookId });
            modelBuilder.Entity<Author>()
                .HasMany(a => a.AuthorBooks)
                .WithOne(ab => ab.Author)
                .HasForeignKey(ab => ab.AuthorId);
            modelBuilder.Entity<Book>()
                .HasMany(b => b.AuthorBooks)
                .WithOne(ab => ab.Book)
                .HasForeignKey(ab => ab.BookId);

            //SeedData(modelBuilder); //Uncomment this line to seed the database with the data below    

        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            var authors = new List<Author>
            {
                new () { Id = 1, Name = "J.K. Rowling" },
                new () { Id = 2, Name = "J.R.R. Tolkien" },
                new () { Id = 3, Name = "George R.R. Martin" },
                new() { Id = 4, Name = "Stephen King" },
            };
            var books = new List<Book>
            {
                new () { Id = 1, Title = "Harry Potter and the Philosopher's Stone", PublicationDate = new DateTime(1997, 6, 26) },
                new () { Id = 2, Title = "Harry Potter and the Chamber of Secrets", PublicationDate = new DateTime(1998, 7, 2) },
                new () { Id = 3, Title = "Harry Potter and the Prisoner of Azkaban", PublicationDate = new DateTime(1999, 7, 8) },
                new () { Id = 5, Title = "Harry Potter and the Order of the Phoenix", PublicationDate = new DateTime(2003, 6, 21) },
                new () { Id = 6, Title = "Harry Potter and the Half-Blood Prince", PublicationDate = new DateTime(2005, 7, 16) },
                new () { Id = 8, Title = "The Hobbit", PublicationDate = new DateTime(1937, 9, 21) },
                new () { Id = 9, Title = "The Fellowship of the Ring", PublicationDate = new DateTime(1954, 7, 29) },
                new () { Id = 10, Title = "The Two Towers", PublicationDate = new DateTime(1954, 11, 11) },
                new () { Id = 11, Title = "The Return of the King", PublicationDate = new DateTime(1955, 10, 20) },
                new () { Id = 12, Title = "A Game of Thrones", PublicationDate = new DateTime(1996, 8, 1) },
                new () { Id = 13, Title = "A Clash of Kings", PublicationDate = new DateTime(1998, 11, 16) },
                new () { Id = 14, Title = "A Storm of Swords", PublicationDate = new DateTime(2000, 8, 8) },
                new () { Id = 15, Title = "A Feast for Crows", PublicationDate = new DateTime(2005, 11, 8) },
                new () { Id = 16, Title = "A Dance with Dragons", PublicationDate = new DateTime(2011, 7, 12) },
                new () { Id = 17, Title = "The Shining", PublicationDate = new DateTime(1977, 1, 28) },
                new () { Id = 18, Title = "It", PublicationDate = new DateTime(1986, 9, 15) },
                new () { Id = 7, Title = "Harry Potter and the Deathly Hallows", PublicationDate = new DateTime(2007, 7, 21) },
            };

            modelBuilder.Entity<Author>().HasData(authors);
            modelBuilder.Entity<Book>().HasData(books);



        }
    }
}
