namespace HoaM.Domain.Common
{
    public interface IDomainEventDispatcher
    {
        Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}
