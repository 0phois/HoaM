namespace HoaM.Domain
{
    /// <summary>
    /// Represents a repository interface for managing operations related to <see cref="Lot"/> entities.
    /// </summary>
    public interface ILotRepository : IBaseRepository<Lot, LotId>
    {
        /// <summary>
        /// Checks if a lot number is unique within the repository.
        /// </summary>
        /// <param name="number">The lot number to check for uniqueness.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the operation asynchronously.</param>
        /// <returns>True if the lot number is unique, otherwise, false.</returns>
        Task<bool> IsLotNumberUnique(LotNumber number, CancellationToken cancellationToken = default);
    }

}
