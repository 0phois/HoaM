using HoaM.Application.Common;
using HoaM.Domain;

namespace HoaM.Application
{
    public interface IMemberBinder : ICommandBinder<AssociationMember, AssociationMemberId>
    {
    }
}
