namespace HoaM.Domain.Features
{
    public interface ILotRepository
    {
        Task<Lot> GetByIdAsync(LotId parcelId, CancellationToken cancellationToken = default);
     
        Task<bool> IsLotNumberUnique(LotNumber number, CancellationToken cancellationToken = default);

        void Insert(Lot parcel);

        void Remove(Lot parcel);
    }
}
