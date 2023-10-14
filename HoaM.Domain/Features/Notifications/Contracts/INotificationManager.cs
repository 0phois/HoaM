using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public interface INotificationManager
    {
        ISystemClock SystemClock { get; }

        IResult DeliverTo(AssociationMember member);
    }
}