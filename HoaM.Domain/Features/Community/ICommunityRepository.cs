namespace HoaM.Domain
{
    /// <summary>
    /// Represents a repository interface for managing communities.
    /// Inherits from <see cref="IBaseRepository{TEntity, TId}"/>.
    /// </summary>
    public interface ICommunityRepository : IBaseRepository<Community, CommunityId>
    {
        /// <summary>
        /// Asynchronously checks if a community name is unique.
        /// </summary>
        /// <param name="name">The community name to check for uniqueness.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation (optional).</param>
        /// <returns>True if the community name is unique; otherwise, false.</returns>
        Task<bool> IsNameUniqueAsync(CommunityName name, CancellationToken cancellationToken = default);
    }

}
