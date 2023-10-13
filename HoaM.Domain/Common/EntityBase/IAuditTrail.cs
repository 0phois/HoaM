namespace HoaM.Domain.Common
{
    public interface IAuditTrail : IEntity<AuditId>
    {
        /// <summary>
        /// Name of the table associated with the <see cref="AuditLog"/> entry
        /// </summary>
        string TableName { get; init; }

        /// <summary>
        /// Primary key of the record associated with the <see cref="AuditLog"/> entry
        /// </summary>
        Guid RecordId { get; init; }

        /// <summary>
        /// <see cref="AuditAction"/> performed on the record
        /// </summary>
        AuditAction Action { get; init; }

        /// <summary>
        /// Names of the columns that were changed in the record
        /// </summary>
        string? ChangedColumns { get; set; }

        /// <summary>
        /// Old values of the changed columns in JSON format
        /// </summary>
        string? OldValues { get; set; }

        /// <summary>
        /// New values of the changed columns in JSON format
        /// </summary>
        string? NewValues { get; set; }

        /// <summary>
        /// Id of the <see cref="AssociationMember"/> who made the change
        /// </summary>
        AssociationMemberId Who { get; set; }

        /// <summary>
        /// Date and time when the change was made
        /// </summary>
        DateTimeOffset When { get; set; }

    }

    public enum AuditAction
    {
        Insert,
        Update, 
        Delete
    }
}
