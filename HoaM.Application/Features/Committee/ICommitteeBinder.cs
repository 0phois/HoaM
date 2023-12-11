using HoaM.Application.Common;
using HoaM.Domain;

namespace HoaM.Application
{
    public interface ICommitteeBinder : ICommandBinder<Committee, CommitteeId>
    {
    }
}
