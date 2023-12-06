using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public interface IEventRepository<T> : IBaseRepository<Event<T>, EventId>
    {
    }
}
