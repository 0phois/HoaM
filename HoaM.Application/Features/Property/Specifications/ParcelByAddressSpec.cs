using HoaM.Domain;
using HoaM.Domain.Features;
using TanvirArjel.EFCore.GenericRepository;

namespace HoaM.Application.Features
{
    internal sealed class ParcelByAddressSpec : Specification<Parcel>
    {
        public ParcelByAddressSpec(StreetName streetName, StreetNumber streetNumber)
        {
            Conditions.Add(parcel => parcel.StreetName == streetName && parcel.StreetNumber == streetNumber);
        }
    }
}
