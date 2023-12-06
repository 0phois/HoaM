using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public interface ILotRepository : IBaseRepository<Lot, LotId>
    {
        Task<bool> IsLotNumberUnique(LotNumber number, CancellationToken cancellationToken = default);
    }
}
