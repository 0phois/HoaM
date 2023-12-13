namespace HoaM.Domain
{
    /// <summary>
    /// Represents a repository interface for managing committees.
    /// Inherits from <see cref="IBaseRepository{TEntity,TId}"/>.
    /// </summary>
    public interface ICommitteeRepository : IBaseRepository<Committee, CommitteeId>
    {
        /// <summary>
        /// Asynchronously checks if a committee name is unique.
        /// </summary>
        /// <param name="name">The committee name to check for uniqueness.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation (optional).</param>
        /// <returns>True if the committee name is unique; otherwise, false.</returns>
        Task<bool> IsNameUniqueAsync(CommitteeName name, CancellationToken cancellationToken = default);
    }

}
