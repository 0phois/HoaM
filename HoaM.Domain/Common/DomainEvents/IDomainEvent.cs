namespace HoaM.Domain.Common
{
    public interface IDomainEvent
    {
        Guid Id { get; }
    }

    public interface INotifyBefore : IDomainEvent { }

    public interface INotifyAfter : IDomainEvent { }
}
