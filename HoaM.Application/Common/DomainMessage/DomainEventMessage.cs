using HoaM.Domain.Common;
using MediatR;

namespace HoaM.Application.Common.MediatR
{
    internal sealed class DomainEventMessage<TDomainEvent> : INotification where TDomainEvent : IDomainEvent
    {
        public TDomainEvent Event { get; }

        public DomainEventMessage(TDomainEvent domainEvent)
        {
            Event = domainEvent;
        }
    }
}
