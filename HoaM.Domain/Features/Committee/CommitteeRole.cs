namespace HoaM.Domain
{
    /// <summary>
    /// Partial class containing predefined committee roles as static properties.
    /// </summary>
    public partial class CommitteeRole
    {
        /// <summary>
        /// Gets a committee role representing the Chairman.
        /// </summary>
        public static CommitteeRole Chairman => From("Chairman");

        /// <summary>
        /// Gets a committee role representing the President.
        /// </summary>
        public static CommitteeRole President => From("President");

        /// <summary>
        /// Gets a committee role representing the Vice President.
        /// </summary>
        public static CommitteeRole VicePresident => From("Vice President");

        /// <summary>
        /// Gets a committee role representing the Secretary.
        /// </summary>
        public static CommitteeRole Secretary => From("Secretary");

        /// <summary>
        /// Gets a committee role representing the Treasurer.
        /// </summary>
        public static CommitteeRole Treasurer => From("Treasurer");

        /// <summary>
        /// Gets a committee role representing the Liaison.
        /// </summary>
        public static CommitteeRole Liaison => From("Liaison");

        /// <summary>
        /// Gets a committee role representing the Member.
        /// </summary>
        public static CommitteeRole Member => From("Member");
    }

    /// <summary>
    /// Represents an assignment of a role within a committee.
    /// </summary>
    public sealed record CommitteeAssignment(CommitteeId CommitteeId, CommitteeRole CommitteeRole);

}
