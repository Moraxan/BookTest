namespace BookTest.Data.Entities
{
    public class Author : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int Id { get; set; }

        [MaxLength(80), Required] //Could make this a longer string but 80 should be enough for most names
        public string? Name { get; set; }

        public virtual ICollection<AuthorBook>? AuthorBooks { get; set; }
    }
}
