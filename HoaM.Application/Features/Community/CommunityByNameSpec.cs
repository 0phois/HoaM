using Ardalis.Specification;
using HoaM.Domain;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    internal sealed class CommunityByNameSpec : Specification<Community>, ISingleResultSpecification<Community>
    {
        public CommunityByNameSpec(CommunityName name)
        {
            Query.Where(c => c.Name == name).AsNoTracking();
        }
    }
}
