using HoaM.Domain.Common;
using MediatR;

namespace HoaM.Extensions.MediatR
{
    internal sealed class MediatrNotificationHandlerAdapter<TNotification>(IEnumerable<IDomainEventHandler<TNotification>> domainEventHandlers) : INotificationHandler<MediatrNotificationAdapter<TNotification>>
        where TNotification : IDomainEvent
    {
        private readonly IEnumerable<IDomainEventHandler<TNotification>> _handlers = domainEventHandlers ?? [];

        public Task Handle(MediatrNotificationAdapter<TNotification> notification, CancellationToken cancellationToken)
        {
            var tasks = _handlers.Select(x => x.Handle(notification.Value, cancellationToken));

            return Task.WhenAll(tasks);
        }
    }
}
