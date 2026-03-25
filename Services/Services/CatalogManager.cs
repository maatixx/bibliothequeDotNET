using BusinessObjects.Entity;
using BusinessObjects.Enum;
using DataAccessLayer.Repository;

namespace Services.Services
{
    /// <summary>
    /// CatalogManager provides business logic for managing the book catalog.
    /// It orchestrates the interactions with the BookRepository.
    /// </summary>
    public class CatalogManager
    {
        private readonly BookRepository _bookRepository;

        /// <summary>
        /// Initializes a new instance of the CatalogManager class.
        /// </summary>
        public CatalogManager()
        {
            _bookRepository = new BookRepository();
        }

        /// <summary>
        /// Retrieves the complete catalog of all books.
        /// </summary>
        /// <returns>An enumeration of all available books.</returns>
        public IEnumerable<Book> GetCatalog()
        {
            return _bookRepository.GetAll();
        }

        /// <summary>
        /// Retrieves books filtered by their type.
        /// Demonstrates business logic on top of repository operations.
        /// </summary>
        /// <param name="type">The type of book to filter by.</param>
        /// <returns>An enumeration of books matching the specified type.</returns>
        public IEnumerable<Book> GetCatalog(TypeBook type)
        {
            return _bookRepository.GetAll()
                .Where(book => book.Type == type)
                .ToList();
        }

        /// <summary>
        /// Finds a book by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <returns>The book if found; otherwise null.</returns>
        public Book? FindBook(int id)
        {
            return _bookRepository.Get(id);
        }
    }
}
