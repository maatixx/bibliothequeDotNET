using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessLayer.Contexts;

namespace DataAccessLayer.Repository
{
    /// <summary>
    /// Implémentation générique de repository pour tous les types d'entités.
    /// Remplace les classes de repository spécifiques en fournissant des opérations CRUD génériques.
    /// </summary>
    /// <typeparam name="T">Le type d'entité, doit implémenter IEntity.</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly LibraryContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(LibraryContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Récupère toutes les entités de la base de données.
        /// </summary>
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        /// <summary>
        /// Récupère une seule entité par son ID.
        /// </summary>
        public T? Get(int id)
        {
            return _dbSet.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Ajoute une nouvelle entité à la base de données.
        /// </summary>
        public T Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Récupère plusieurs entités avec filtrage optionnel et chargement des relations.
        /// </summary>
        /// <param name="filter">Prédicat optionnel pour filtrer les entités.</param>
        /// <param name="includes">Propriétés de navigation à inclure (ex: "Author", "Books").</param>
        public IEnumerable<T> GetMultiple(Func<T, bool>? filter = null, params string[] includes)
        {
            IQueryable<T> query = _dbSet;

            // Charger les entités liées (eager loading)
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Appliquer le filtre s'il est fourni
            if (filter != null)
            {
                return query.AsEnumerable().Where(filter).ToList();
            }

            return query.ToList();
        }
    }
}
