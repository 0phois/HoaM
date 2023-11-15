using Ardalis.Specification;
using HoaM.Domain;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    internal sealed class ParcelByAddressSpec : Specification<Parcel>, ISingleResultSpecification<Parcel>
    {
        public ParcelByAddressSpec(StreetName streetName, StreetNumber streetNumber)
        {
            Query.Where(parcel => parcel.StreetName == streetName &&  parcel.StreetNumber == streetNumber).AsNoTracking();
        }
    }
}
