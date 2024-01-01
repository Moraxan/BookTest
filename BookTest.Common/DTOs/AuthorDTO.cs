namespace BookTest.Common.DTOs
{
    // DTO for creating a new author (without Id)
    public class AuthorDTO
    {
        public string? Name { get; set; }
        public List<int>? BookIds { get; set; } // List of book IDs
    }

    // DTO for reading author data (with Id)
    public class AuthorReadDTO : AuthorDTO
    {
        public int Id { get; set; }
    }
    
}