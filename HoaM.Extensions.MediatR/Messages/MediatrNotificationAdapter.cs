using HoaM.Domain.Common;
using MediatR;

namespace HoaM.Extensions.MediatR
{
    internal sealed record MediatrNotificationAdapter<TNotification>(TNotification Value) : INotification where TNotification : IDomainEvent
    {
    }
}
