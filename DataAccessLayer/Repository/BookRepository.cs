using BusinessObjects.Entity;
using DataAccessLayer.Repository;

namespace DataAccessLayer.Repository
{
    /// <summary>
    /// Repository for managing Book entities.
    /// Currently uses in-memory data storage.
    /// </summary>
    public class BookRepository : IRepository<Book>
    {
        private readonly List<Book> _books;

        public BookRepository()
        {
            // Initialize with sample data
            _books = new List<Book>
            {
                new Book 
                { 
                    Id = 1, 
                    Name = "Les Aventures de Tom Sawyer", 
                    Type = BusinessObjects.Enum.TypeBook.Aventure,
                    Isbn = "978-0-123456-78-9",
                    PublicationYear = 1876,
                    AuthorId = 1
                },
                new Book 
                { 
                    Id = 2, 
                    Name = "Clean Code", 
                    Type = BusinessObjects.Enum.TypeBook.Programming,
                    Isbn = "978-0-132350-88-6",
                    PublicationYear = 2008,
                    AuthorId = 2
                },
                new Book 
                { 
                    Id = 3, 
                    Name = "Le Petit Prince", 
                    Type = BusinessObjects.Enum.TypeBook.Fiction,
                    Isbn = "978-0-156012-95-9",
                    PublicationYear = 1943,
                    AuthorId = 3
                }
            };
        }

        /// <summary>
        /// Retrieves all books from the repository.
        /// </summary>
        public IEnumerable<Book> GetAll()
        {
            return _books;
        }

        /// <summary>
        /// Retrieves a book by its identifier.
        /// </summary>
        public Book? Get(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }
    }
}
