using BusinessObjects.Entity;
using DataAccessLayer.Repository;

namespace DataAccessLayer.Repository
{
    /// <summary>
    /// Repository for managing Library entities.
    /// Currently uses in-memory data storage.
    /// </summary>
    public class LibraryRepository : IRepository<Library>
    {
        private readonly List<Library> _libraries;

        public LibraryRepository()
        {
            // Initialize with sample data
            _libraries = new List<Library>
            {
                new Library 
                { 
                    Id = 1, 
                    Name = "Central Library", 
                    Address = "123 Main Street",
                    City = "Paris"
                },
                new Library 
                { 
                    Id = 2, 
                    Name = "Downtown Library", 
                    Address = "456 Oak Avenue",
                    City = "Lyon"
                }
            };
        }

        /// <summary>
        /// Retrieves all libraries from the repository.
        /// </summary>
        public IEnumerable<Library> GetAll()
        {
            return _libraries;
        }

        /// <summary>
        /// Retrieves a library by its identifier.
        /// </summary>
        public Library? Get(int id)
        {
            return _libraries.FirstOrDefault(l => l.Id == id);
        }
    }
}
