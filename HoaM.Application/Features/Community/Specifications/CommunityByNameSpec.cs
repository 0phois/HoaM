using HoaM.Domain;
using HoaM.Domain.Features;
using TanvirArjel.EFCore.GenericRepository;

namespace HoaM.Application.Features
{
    internal sealed class CommunityByNameSpec : Specification<Community>
    {
        public CommunityByNameSpec(CommunityName name) => Conditions.Add(c => c.Name == name);
    }
}
