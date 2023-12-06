using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public interface IParcelRepository : IBaseRepository<Parcel, ParcelId>
    {
        Task<bool> IsAddressUniqueAsync(StreetNumber streetNumber, StreetName streetName, CancellationToken cancellationToken = default);

        Task<bool> HasUniqueLotsAsync(Lot[] lots, CancellationToken cancellationToken = default);
    }
}
