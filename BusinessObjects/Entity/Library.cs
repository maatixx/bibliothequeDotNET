namespace BusinessObjects.Entity
{
    /// <summary>
    /// Represents a Library entity in the library system.
    /// </summary>
    public class Library : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        // Navigation property: Many libraries can have many books
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}
