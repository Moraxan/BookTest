namespace BookTest.Common.DTOs
{
    // Base class for common properties of a user
    public class UserBaseDTO
    {
        public string Username { get; set; }
    }

    // Derived class for creating a new user
    public class CreateUserDTO : UserBaseDTO
    {
        // Additional properties for creating a new user can be added here
        // For example, password, email, etc.
    }

    // Derived class for updating an existing user
    public class UpdateUserDTO : UserBaseDTO
    {
        public int Id { get; set; }
    }
}
