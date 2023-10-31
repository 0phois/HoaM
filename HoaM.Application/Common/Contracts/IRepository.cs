using Ardalis.Specification;
using HoaM.Domain.Common;

namespace HoaM.Application.Common
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IEntity
    {

    }

    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IEntity
    {

    }

    public interface IRepositoryWithEvents<T> : IRepository<T> where T : class, IEntity
    {

    }
}
