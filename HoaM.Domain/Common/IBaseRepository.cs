using HoaM.Domain.Common;

namespace HoaM.Domain
{
    public interface IBaseRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        Task<TEntity?> GetByIdAsync(TId id);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);
    }
}
