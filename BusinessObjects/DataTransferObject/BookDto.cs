using BusinessObjects.Enum;

namespace BusinessObjects.DataTransferObject
{
    /// <summary>
    /// DTO for Book information.
    /// Exposes only necessary data to API clients:
    /// - Hides internal Rate and AuthorId properties
    /// - Includes author information through AuthorDto
    /// - Provides a clean interface for API responses
    /// </summary>
    public class BookDto
    {
        /// <summary>
        /// Unique identifier for the book.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the book.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The ISBN of the book.
        /// </summary>
        public string Isbn { get; set; } = string.Empty;

        /// <summary>
        /// The publication year of the book.
        /// </summary>
        public int PublicationYear { get; set; }

        /// <summary>
        /// The genre/type of the book.
        /// </summary>
        public TypeBook Type { get; set; }

        /// <summary>
        /// Information about the book's author.
        /// </summary>
        public AuthorDto? Author { get; set; }
    }
}
