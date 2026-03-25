using BusinessObjects.Entity;
using BusinessObjects.Enum;

namespace Services.Services
{
    /// <summary>
    /// Interface for catalog management operations.
    /// Defines the contract for accessing and filtering books in the catalog.
    /// </summary>
    public interface ICatalogManager
    {
        /// <summary>
        /// Retrieves all books in the catalog.
        /// </summary>
        IEnumerable<Book> GetCatalog();

        /// <summary>
        /// Retrieves books filtered by their type.
        /// </summary>
        /// <param name="type">The type of book to filter by.</param>
        IEnumerable<Book> GetCatalog(TypeBook type);

        /// <summary>
        /// Finds a book by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        Book? FindBook(int id);
    }
}
