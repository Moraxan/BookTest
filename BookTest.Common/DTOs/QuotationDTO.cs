namespace BookTest.Common.DTOs
{
    // Base class for common properties of a quotation
    public class QuotationBaseDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateAdded { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } // Optional, based on your needs
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } // Include only the necessary details
    }

   
}
