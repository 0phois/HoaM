using HoaM.Domain.Common;
using MediatR;

namespace HoaM.Extensions.MediatR
{
    internal sealed class MediatrDomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
    {
        private readonly IMediator _mediator = mediator;

        public Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            return _mediator.Publish(new MediatrNotificationAdapter<IDomainEvent>(domainEvent), cancellationToken);
        }
    }
}
