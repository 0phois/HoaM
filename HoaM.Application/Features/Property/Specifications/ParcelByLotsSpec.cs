using Ardalis.Specification;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    internal sealed class ParcelByLotsSpec : Specification<Parcel>, ISingleResultSpecification<Parcel>
    {
        public ParcelByLotsSpec(Lot[] lots)
        {
            Query.Where(parcel => parcel.Lots.Any(lot => lots.Contains(lot))).AsNoTracking();
        }
    }
}
