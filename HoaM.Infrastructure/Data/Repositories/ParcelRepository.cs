using HoaM.Domain;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data
{
    internal sealed class ParcelRepository(DbContext dbContext) : GenericRepository<Parcel, ParcelId>(dbContext), IParcelRepository
    {
        public Task<bool> HasUniqueLotsAsync(Lot[] lots, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAddressUniqueAsync(StreetNumber streetNumber, StreetName streetName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
