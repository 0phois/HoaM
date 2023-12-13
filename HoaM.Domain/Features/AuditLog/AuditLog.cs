using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents an audit log entry as an entity with an identifier of type <see cref="AuditId"/>.
    /// Inherits from <see cref="Entity{AuditId}"/> and implements the <see cref="IAuditTrail"/> interface.
    /// </summary>
    public class AuditLog : Entity<AuditId>, IAuditTrail
    {
        /// <summary>
        /// Gets or sets the unique identifier for the audit log entry.
        /// </summary>
        public override AuditId Id { get; protected set; } = AuditId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Gets or sets the name of the database table being modified.
        /// </summary>
        public string TableName { get; init; }

        /// <summary>
        /// Gets or sets the primary identifier for the record from its originating table.
        /// </summary>
        public Guid RecordId { get; init; }

        /// <summary>
        /// Gets or sets the audit action that resulted in the record being created/changed.
        /// </summary>
        public AuditAction Action { get; init; }

        /// <summary>
        /// Gets or sets the names of the columns that were changed in the record.
        /// </summary>
        public string? ChangedColumns { get; set; }

        /// <summary>
        /// Gets or sets the old values of the changed record in JSON format.
        /// </summary>
        public string? OldValues { get; set; }

        /// <summary>
        /// Gets or sets the new values of the changed record in JSON format.
        /// </summary>
        public string? NewValues { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the association member that performed the action.
        /// </summary>
        public AssociationMemberId Who { get; set; }

        /// <summary>
        /// Gets or sets the date and time the action was performed.
        /// </summary>
        public DateTimeOffset When { get; set; }

        /// <summary>
        /// Private constructor to prevent direct instantiation of the audit log without using the creation methods.
        /// </summary>
        private AuditLog() { }

        /// <summary>
        /// Creates an audit log entry for an insert action.
        /// </summary>
        /// <param name="table">The name of the database table being modified.</param>
        /// <param name="recordId">The primary identifier for the record from its originating table.</param>
        /// <returns>A new instance of the <see cref="AuditLog"/> class for an insert action.</returns>
        public static AuditLog CreateInsertLog(string table, Guid recordId)
        {
            return new AuditLog { TableName = table, RecordId = recordId, Action = AuditAction.Insert };
        }

        /// <summary>
        /// Creates an audit log entry for an update action.
        /// </summary>
        /// <param name="table">The name of the database table being modified.</param>
        /// <param name="recordId">The primary identifier for the record from its originating table.</param>
        /// <param name="columns">The names of the columns that were changed in the record.</param>
        /// <param name="oldValues">The old values of the changed record in JSON format.</param>
        /// <param name="newValues">The new values of the changed record in JSON format.</param>
        /// <returns>A new instance of the <see cref="AuditLog"/> class for an update action.</returns>
        public static AuditLog CreateUpdateLog(string table, Guid recordId, string columns, string oldValues, string newValues)
        {
            return new AuditLog { TableName = table, RecordId = recordId, Action = AuditAction.Update, ChangedColumns = columns, OldValues = oldValues, NewValues = newValues };
        }

        /// <summary>
        /// Creates an audit log entry for a delete action.
        /// </summary>
        /// <param name="table">The name of the database table being modified.</param>
        /// <param name="recordId">The primary identifier for the record from its originating table.</param>
        /// <param name="oldValues">The old values of the changed record in JSON format.</param>
        /// <returns>A new instance of the <see cref="AuditLog"/> class for a delete action.</returns>
        public static AuditLog CreateDeleteLog(string table, Guid recordId, string oldValues)
        {
            return new AuditLog { TableName = table, RecordId = recordId, Action = AuditAction.Delete, OldValues = oldValues };
        }
    }

}
