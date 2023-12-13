using HoaM.Domain;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data
{
    internal sealed class LotRepository(DbContext dbContext) : GenericRepository<Lot, LotId>(dbContext), ILotRepository
    {
        public Task<bool> IsLotNumberUnique(LotNumber number, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
