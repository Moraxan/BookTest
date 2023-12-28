namespace BookTest.Common.DTOs
{
    // Base class for common properties of a book
    public class BookDTO

    {
        
        public string? Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public List<int>? AuthorIds { get; set; } // List of author IDs
    }

    // DTO for creating a new book (without Id)
    public class BookReadDTO : BookDTO
    {
        public int Id { get; set; }
    }

   
}
