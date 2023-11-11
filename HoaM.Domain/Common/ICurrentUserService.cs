using HoaM.Domain.Features;

namespace HoaM.Domain.Common
{
    public interface ICurrentUserService
    {
        IMember CurrentUser { get; }

        ValueTask<CommitteeMember> GetCommitteeMember();
    }
}
