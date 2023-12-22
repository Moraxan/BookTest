namespace BookTest.Common.DTOs
{
    // Base class for common properties without Id
    public class AuthorBaseDTO
    {
        public string? Name { get; set; }
        public List<int>? BookIds { get; set; } // List of book IDs
    }

    // DTO for operations that require an Id (Read, Update, Delete)
    public class AuthorDTO : AuthorBaseDTO
    {
        public int Id { get; set; } // Include Id here
    }

    // Derived class for creating a new author
    public class CreateAuthorDTO : AuthorBaseDTO
    {
        // No Id needed here as it's for creation
        // Additional properties for creating a new author can be added here
    }

    // Derived class for updating an existing author
    public class UpdateAuthorDTO : AuthorBaseDTO
    {
        public int Id { get; set; } // Id needed for specifying the author to update
        // Additional properties for updating an author can be added here
    }
}
