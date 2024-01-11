namespace BookTest.Data.Entities
{
       public class User : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; } // Optional, for implementing refresh token logic
    }
}
