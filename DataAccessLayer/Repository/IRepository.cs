using BusinessObjects;

namespace DataAccessLayer.Repository
{
    /// <summary>
    /// Generic repository interface for basic CRUD operations.
    /// Works with any entity type that implements IEntity.
    /// </summary>
    /// <typeparam name="T">The entity type, must implement IEntity.</typeparam>
    public interface IGenericRepository<T> where T : IEntity
    {
        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Retrieves an entity by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        T? Get(int id);
    }
}
