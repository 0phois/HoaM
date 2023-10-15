namespace HoaM.Domain.Common
{
    public interface IDomainEvent
    {
        Guid Id { get; }
    }

    public interface INotifyBefore { }

    public interface INotifyAfter { }
}
