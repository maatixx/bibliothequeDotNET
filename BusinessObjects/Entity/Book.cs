using BusinessObjects.Enum;

namespace BusinessObjects.Entity
{
    /// <summary>
    /// Represents a Book entity in the library system.
    /// </summary>
    public class Book : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public TypeBook Type { get; set; }
        public string Isbn { get; set; } = string.Empty;
        public int PublicationYear { get; set; }

        // Foreign key
        public int AuthorId { get; set; }
        
        // Navigation property: Reference to the author
        public Author? Author { get; set; }

        // Navigation property: Many books can be in many libraries
        public IEnumerable<Library> Libraries { get; set; } = new List<Library>();
    }
}
