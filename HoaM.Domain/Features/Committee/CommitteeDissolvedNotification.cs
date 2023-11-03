using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public sealed class CommitteeDissolvedNotification : DomainNotification
    {
        public Committee Committee { get; }

        public CommitteeDissolvedNotification(Committee committee) => Committee = committee;
    }
}
