using HoaM.Application.Common;
using HoaM.Domain;

namespace HoaM.Application
{
    public interface ICommunityBinder : ICommandBinder<Community, CommunityId>
    {
    }
}
