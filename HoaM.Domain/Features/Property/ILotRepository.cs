namespace HoaM.Domain
{
    public interface ILotRepository : IBaseRepository<Lot, LotId>
    {
        Task<bool> IsLotNumberUnique(LotNumber number, CancellationToken cancellationToken = default);
    }
}
