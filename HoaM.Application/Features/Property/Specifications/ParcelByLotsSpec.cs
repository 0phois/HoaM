using HoaM.Domain.Features;
using TanvirArjel.EFCore.GenericRepository;

namespace HoaM.Application.Features
{
    internal sealed class ParcelByLotsSpec : Specification<Parcel>
    {
        public ParcelByLotsSpec(Lot[] lots)
        {
            Conditions.Add(parcel => parcel.Lots.Any(lot => lots.Contains(lot)));
        }
    }
}
