namespace HoaM.Domain.Features
{
    public interface IParcelRepository
    {
        Task<Parcel> GetByIdAsync(ParcelId parcelId, CancellationToken cancellationToken = default);

        Task<bool> IsAddressUniqueAsync(StreetNumber streetNumber, StreetName streetName, CancellationToken cancellationToken = default);

        Task<bool> HasUniqueLotsAsync(Lot[] lots, CancellationToken cancellationToken = default);

        void Insert(Parcel parcel); 

        void Remove(Parcel parcel);
    }
}
