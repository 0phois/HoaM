using HoaM.Application.Common;
using HoaM.Domain;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public interface ICommitteeBinder : ICommandBinder<Committee, CommitteeId>
    {
    }
}
