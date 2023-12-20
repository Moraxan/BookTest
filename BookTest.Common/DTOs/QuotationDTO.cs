using System.Security.Principal;

namespace BookTest.Common.DTOs
{
    public class QuotationDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateAdded { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; } // Optional, based on your needs

        // Author information
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } // Include only the necessary details
    }

}
