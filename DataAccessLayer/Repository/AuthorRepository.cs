using BusinessObjects.Entity;
using DataAccessLayer.Repository;

namespace DataAccessLayer.Repository
{
    /// <summary>
    /// Repository for managing Author entities.
    /// Currently uses in-memory data storage.
    /// </summary>
    public class AuthorRepository : IRepository<Author>
    {
        private readonly List<Author> _authors;

        public AuthorRepository()
        {
            // Initialize with sample data
            _authors = new List<Author>
            {
                new Author 
                { 
                    Id = 1, 
                    FirstName = "Mark", 
                    LastName = "Twain"
                },
                new Author 
                { 
                    Id = 2, 
                    FirstName = "Robert", 
                    LastName = "Martin"
                },
                new Author 
                { 
                    Id = 3, 
                    FirstName = "Antoine", 
                    LastName = "de Saint-Exupéry"
                }
            };
        }

        /// <summary>
        /// Retrieves all authors from the repository.
        /// </summary>
        public IEnumerable<Author> GetAll()
        {
            return _authors;
        }

        /// <summary>
        /// Retrieves an author by their identifier.
        /// </summary>
        public Author? Get(int id)
        {
            return _authors.FirstOrDefault(a => a.Id == id);
        }
    }
}
