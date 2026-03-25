using BusinessObjects;

namespace DataAccessLayer.Repository
{
    /// <summary>
    /// Generic repository interface for basic CRUD operations.
    /// </summary>
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T? Get(int id);
    }
}
