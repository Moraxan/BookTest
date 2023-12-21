namespace BookTest.Common.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public List<int>? AuthorIds { get; set; } // List of author IDs instead of AuthorDTOs
    }

}