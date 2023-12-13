using HoaM.Domain.Common;

namespace HoaM.Domain
{
    /// <summary>
    /// Interface defining basic operations for a repository handling entities with a generic identifier.
    /// </summary>
    /// <typeparam name="TEntity">The type of entities managed by the repository.</typeparam>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    public interface IBaseRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        /// <summary>
        /// Asynchronously retrieves an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns the retrieved entity, or null if not found.</returns>
        Task<TEntity?> GetByIdAsync(TId id);

        /// <summary>
        /// Inserts a new entity into the repository.
        /// </summary>
        /// <param name="entity">The entity to be inserted.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Removes an entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to be removed.</param>
        void Remove(TEntity entity);
    }

}
