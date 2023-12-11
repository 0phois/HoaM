namespace HoaM.Domain
{
    public interface IEventRepository<T> : IBaseRepository<Event<T>, EventId>
    {
    }
}
