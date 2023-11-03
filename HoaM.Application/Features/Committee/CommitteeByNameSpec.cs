using Ardalis.Specification;
using HoaM.Domain;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    internal sealed class CommitteeByNameSpec : Specification<Committee>, ISingleResultSpecification<Committee>
    {
        public CommitteeByNameSpec(CommitteeName name)
        {
            Query.Where(c => c.Name == name && c.DeletionDate == null).AsNoTracking();
        }
    }
}
