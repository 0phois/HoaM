using Ardalis.Specification;
using HoaM.Domain;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    internal sealed class LotByNumberSpec : Specification<Lot>, ISingleResultSpecification<Lot>
    {
        public LotByNumberSpec(LotNumber number)
        {
            Query.Where(lot => lot.Number == number).AsNoTracking();
        }
    }
}
