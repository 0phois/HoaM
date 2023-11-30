using HoaM.Domain;
using HoaM.Domain.Features;
using TanvirArjel.EFCore.GenericRepository;

namespace HoaM.Application.Features
{
    internal sealed class CommitteeByNameSpec : Specification<Committee>
    {
        public CommitteeByNameSpec(CommitteeName name) => Conditions.Add(c => c.Name == name && c.DeletionDate == null);
    }
}
