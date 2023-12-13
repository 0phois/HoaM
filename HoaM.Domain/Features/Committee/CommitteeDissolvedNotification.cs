using HoaM.Domain.Common;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a domain notification indicating the dissolution of a committee.
    /// Inherits from <see cref="DomainNotification"/>.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CommitteeDissolvedNotification"/> class.
    /// </remarks>
    /// <param name="committee">The committee that has been dissolved.</param>
    public sealed class CommitteeDissolvedNotification(Committee committee) : DomainNotification
    {
        /// <summary>
        /// Gets the committee that has been dissolved.
        /// </summary>
        public Committee Committee { get; } = committee;
    }

}
