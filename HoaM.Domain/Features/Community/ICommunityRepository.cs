using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public interface ICommunityRepository : IBaseRepository<Community, CommunityId>
    {
        Task<bool> IsNameUniqueAsync(CommunityName name, CancellationToken cancellationToken = default);
    }
}
