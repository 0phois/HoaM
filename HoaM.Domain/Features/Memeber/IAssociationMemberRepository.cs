namespace HoaM.Domain
{
    /// <summary>
    /// Represents a repository interface for managing association members.
    /// </summary>
    public interface IAssociationMemberRepository : IBaseRepository<AssociationMember, AssociationMemberId>
    {
        /// <summary>
        /// Checks whether the specified email is unique within the association members.
        /// </summary>
        /// <param name="email">The email address to check for uniqueness.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a boolean indicating whether the email is unique (true) or not (false).
        /// </returns>
        Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
    }

}
