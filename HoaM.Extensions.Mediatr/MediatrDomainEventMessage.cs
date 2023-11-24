using HoaM.Domain.Common;
using MediatR;

namespace HoaM.Extensions.Mediatr
{
    internal sealed record MediatrDomainEventMessage<TDomainEvent>(TDomainEvent DomainEvent) : INotification where TDomainEvent : IDomainEvent
    {
    }
}