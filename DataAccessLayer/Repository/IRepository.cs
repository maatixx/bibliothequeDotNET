using BusinessObjects;

namespace DataAccessLayer.Repository
{
    /// <summary>
    /// Interface générique de repository pour les opérations CRUD.
    /// Fonctionne avec tout type d'entité qui implémente IEntity.
    /// </summary>
    /// <typeparam name="T">Le type d'entité, doit implémenter IEntity.</typeparam>
    public interface IGenericRepository<T> where T : IEntity
    {
        /// <summary>
        /// Récupère toutes les entités.
        /// </summary>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Récupère une entité par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant unique.</param>
        T? Get(int id);

        /// <summary>
        /// Ajoute une nouvelle entité à la base de données.
        /// </summary>
        /// <param name="entity">L'entité à ajouter.</param>
        /// <returns>L'entité ajoutée.</returns>
        T Add(T entity);

        /// <summary>
        /// Récupère plusieurs entités avec filtrage optionnel et chargement des relations.
        /// </summary>
        /// <param name="filter">Prédicat optionnel pour filtrer les entités.</param>
        /// <param name="includes">Propriétés de navigation à inclure (eager load).</param>
        /// <returns>Une énumération d'entités correspondant aux critères.</returns>
        IEnumerable<T> GetMultiple(Func<T, bool>? filter = null, params string[] includes);
    }
}

