using BusinessObjects.Enum;

namespace BusinessObjects.Entity
{
    /// <summary>
    /// Represents an Author entity in the library system.
    /// </summary>
    public class Author : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Navigation property: One author has many books
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}
