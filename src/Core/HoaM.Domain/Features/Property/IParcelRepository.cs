namespace HoaM.Domain
{
    /// <summary>
    /// Represents a repository interface for managing operations related to <see cref="Parcel"/> entities.
    /// </summary>
    public interface IParcelRepository : IBaseRepository<Parcel, ParcelId>
    {
        /// <summary>
        /// Checks if an address (combination of street number and name) is unique within the repository.
        /// </summary>
        /// <param name="streetNumber">The street number of the address to check for uniqueness.</param>
        /// <param name="streetName">The street name of the address to check for uniqueness.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the operation asynchronously.</param>
        /// <returns>True if the address is unique, otherwise, false.</returns>
        Task<bool> IsAddressUniqueAsync(StreetNumber streetNumber, StreetName streetName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a collection of lots is unique within the repository.
        /// </summary>
        /// <param name="lots">The lots to check for uniqueness.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the operation asynchronously.</param>
        /// <returns>True if the lots are unique, otherwise, false.</returns>
        Task<bool> HasUniqueLotsAsync(Lot[] lots, CancellationToken cancellationToken = default);
    }

}
