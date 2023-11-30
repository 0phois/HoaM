using HoaM.Domain.Common;
using MediatR;

namespace HoaM.Extensions.MediatR
{
    internal sealed record MediatrDomainEventMessage<TDomainEvent>(TDomainEvent DomainEvent) : INotification where TDomainEvent : IDomainEvent
    {
    }
}