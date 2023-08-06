namespace HoaM.Domain.Contracts
{
    public interface IDomainEvent : IDomainNotification
    {
        DateTimeOffset TriggeredOn { get; }
    }
}
