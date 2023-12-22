namespace BookTest.Common.DTOs
{
    // Base class for common properties of a quotation
    public class QuotationBaseDTO
    {
        public string Text { get; set; }
        public DateTime DateAdded { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } // Optional, based on your needs
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } // Include only the necessary details
    }

    // Derived class for creating a new quotation
    public class CreateQuotationDTO : QuotationBaseDTO
    {
        // Additional properties for creating a new quotation can be added here
    }

    // Derived class for updating an existing quotation
    public class UpdateQuotationDTO : QuotationBaseDTO
    {
        public int Id { get; set; }
    }
}
