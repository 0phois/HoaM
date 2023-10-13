using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Features
{
    public class AuditLog : Entity<AuditId>, IAuditTrail
    {
        /// <summary>
        /// Unique ID of the <see cref="AuditLog"/> entry
        /// </summary>
        public override AuditId Id => AuditId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Name of the database table being modified
        /// </summary>
        public required string TableName { get; init; }

        /// <summary>
        /// The primary identifier for the record from its originating table
        /// </summary>
        public required Guid RecordId { get; init; }

        /// <summary>
        /// <see cref="AuditAction"/> that resulted in the record being created/changed
        /// </summary>
        public required AuditAction Action { get; init; }

        /// <summary>
        /// Names of the columns that were changed in the record
        /// </summary>
        public string? ChangedColumns { get; set; }

        /// <summary>
        /// Old values of the changed record in JSON format
        /// </summary>
        public string? OldValues { get; set; }

        /// <summary>
        /// New values of the changed record in JSON format
        /// </summary>
        public string? NewValues { get; set; }

        /// <summary>
        /// <see cref="AssociationMember"/> that performed the action
        /// </summary>
        public AssociationMemberId Who { get; set; }

        /// <summary>
        /// Date and time the action was performed
        /// </summary>
        public DateTimeOffset When { get; set; }

        private AuditLog() { }

        public static AuditLog CreateInsertLog(string table, Guid recordId)
        {
            return new() { TableName = table, RecordId = recordId, Action = AuditAction.Insert };
        }

        public static AuditLog CreateUpdateLog(string table, Guid recordId, string columns, string oldValues, string newValues) 
        {
            return new() { TableName = table, RecordId = recordId, Action = AuditAction.Update, ChangedColumns = columns, OldValues = oldValues, NewValues = newValues };
        }

        public static AuditLog CreateDeleteLog(string table, Guid recordId, string oldValues)
        { 
            return new() { TableName = table, RecordId = recordId, Action = AuditAction.Delete, OldValues = oldValues };
        }
    }
}
