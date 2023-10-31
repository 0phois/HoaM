using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Common
{
    internal class SystemUser : Entity<AssociationMemberId>, IMember
    {
        public Username Username => Username.From("SystemUser");
    }
}
