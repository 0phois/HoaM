namespace HoaM.Domain
{
    /// <summary>
    /// Represents an interface for a member within an association or community.
    /// </summary>
    public interface IMember
    {
        /// <summary>
        /// Gets the unique identifier of the association member.
        /// </summary>
        AssociationMemberId Id { get; }

        /// <summary>
        /// Gets the display name or username of the association member.
        /// </summary>
        Username DisplayName { get; }
    }
}
