namespace HoaM.Domain.Contracts
{
    public interface IDomainNotification
    {
        DateTimeOffset TriggeredOn { get; }
    }
}
