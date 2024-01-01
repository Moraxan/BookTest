namespace BookTest.Data.Entities
{
    public class Quotation : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateAdded { get; set; }

        // Foreign Key - User
        public int UserId { get; set; }
        public virtual User User { get; set; } // Make navigation property virtual


    }
}
