namespace HoaM.Domain
{
    /// <summary>
    /// Represents an interface for a repository managing events with a specified activity type.
    /// Inherits from <see cref="IBaseRepository{TEntity, TId}"/>.
    /// </summary>
    /// <typeparam name="T">The type representing the basis of the event.</typeparam>
    public interface IEventRepository<T> : IBaseRepository<Event<T>, EventId>
    {
    }

}
