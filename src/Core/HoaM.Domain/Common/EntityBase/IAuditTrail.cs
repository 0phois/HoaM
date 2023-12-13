namespace HoaM.Domain.Common
{
    /// <summary>
    /// Interface representing an audit trail record for tracking changes to entities in the system.
    /// </summary>
    public interface IAuditTrail : IEntity<AuditId>
    {
        /// <summary>
        /// Gets or sets the name of the table associated with the audited entity.
        /// </summary>
        string TableName { get; init; }

        /// <summary>
        /// Gets or sets the unique identifier of the audited record.
        /// </summary>
        Guid RecordId { get; init; }

        /// <summary>
        /// Gets or sets the action performed on the audited record (Insert, Update, Delete).
        /// </summary>
        AuditAction Action { get; init; }

        /// <summary>
        /// Gets or sets a comma-separated list of column names that were changed during the action.
        /// </summary>
        string? ChangedColumns { get; set; }

        /// <summary>
        /// Gets or sets a string representation of the old values of the changed columns.
        /// </summary>
        string? OldValues { get; set; }

        /// <summary>
        /// Gets or sets a string representation of the new values of the changed columns.
        /// </summary>
        string? NewValues { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who performed the action.
        /// </summary>
        AssociationMemberId Who { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the change was made.
        /// </summary>
        DateTimeOffset When { get; set; }
    }

    /// <summary>
    /// Enumeration representing the possible actions for auditing purposes (Insert, Update, Delete).
    /// </summary>
    public enum AuditAction
    {
        Insert,
        Update,
        Delete
    }

}
