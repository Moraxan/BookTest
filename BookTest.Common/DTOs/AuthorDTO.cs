namespace BookTest.Common.DTOs
{

    public class AuthorDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<int>? BookIds { get; set; } // List of book IDs instead of BookDTOs
    }
}
