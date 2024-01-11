namespace BookTest.Common.DTOs
{
    public class UserDTO
    {

        public string Username { get; set; }
        public string Password { get; set; } // Add this line
        public string RefreshToken { get; set; }
    }

    public class UserReadDTO : UserDTO
    {
        public int Id { get; set; }
    }
}
