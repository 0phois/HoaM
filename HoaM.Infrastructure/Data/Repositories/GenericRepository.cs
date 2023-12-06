using HoaM.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data
{
    internal abstract class GenericRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        protected GenericRepository(DbContext dbContext) => DbContext = dbContext;

        /// <summary>
        /// Gets the database context.
        /// </summary>
        protected DbContext DbContext { get; }

        /// <summary>
        /// Gets the entity with the specified identifier.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        /// <returns>The maybe instance that may contain the entity with the specified identifier.</returns>
        public async Task<TEntity?> GetByIdAsync(TId id) => await DbContext.Set<TEntity>().FindAsync(id);

        /// <summary>
        /// Inserts the specified entity into the database.
        /// </summary>
        /// <param name="entity">The entity to be inserted into the database.</param>
        public virtual void Insert(TEntity entity) => DbContext.Set<TEntity>().Add(entity);

        /// <summary>
        /// Inserts the specified entities to the database.
        /// </summary>
        /// <param name="entities">The entities to be inserted into the database.</param>
        public virtual void InsertRange(IReadOnlyCollection<TEntity> entities) => DbContext.Set<TEntity>().AddRange(entities);

        /// <summary>
        /// Updates the specified entity in the database.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        public virtual void Update(TEntity entity) => DbContext.Set<TEntity>().Update(entity);

        /// <summary>
        /// Removes the specified entity from the database.
        /// </summary>
        /// <param name="entity">The entity to be removed from the database.</param>
        public virtual void Remove(TEntity entity) => DbContext.Set<TEntity>().Remove(entity);
    }
}
