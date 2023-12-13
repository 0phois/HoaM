using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed class SystemUser : Entity<AssociationMemberId>, IMember
    {
        public Username DisplayName => Username.From("SystemUser");
    }
}
