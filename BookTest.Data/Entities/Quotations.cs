public class Quotation : IEntity
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime DateAdded { get; set; }

    // Foreign Key - User
    public int UserId { get; set; }
    public User User { get; set; } // Navigation property

    // Foreign Key - Author
    public int AuthorId { get; set; }
    public Author Author { get; set; } // Navigation property
}
