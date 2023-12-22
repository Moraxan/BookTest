namespace BookTest.Common.DTOs
{
    // Base class for common properties of a book
    public class BookBaseDTO
    {
        public string? Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public List<int>? AuthorIds { get; set; } // List of author IDs
    }

    // Derived class for creating a new book
    public class CreateBookDTO : BookBaseDTO
    {
        // Additional properties for creating a new book can be added here
    }

    // Derived class for updating an existing book
    public class UpdateBookDTO : BookBaseDTO
    {
        public int Id { get; set; }
    }
}
