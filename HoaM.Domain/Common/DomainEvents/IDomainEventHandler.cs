namespace HoaM.Domain.Common
{
    public interface IDomainEventHandler<in TDomainEvent>
    {
        Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}
