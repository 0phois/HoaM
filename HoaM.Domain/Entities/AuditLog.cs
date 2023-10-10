using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Entities
{
    public class AuditLog : Entity<AuditId>, IAuditTrail
    {
        /// <summary>
        /// Unique ID of the <see cref="AuditLog"/> entry
        /// </summary>
        public override AuditId Id => AuditId.From(NewId.Next().ToGuid());

        public required string TableName { get; set; }

        public required Guid RecordId { get; set; }

        public AuditAction Action { get; set; }

        public string? ChangedColumns { get; set; }

        public string? OldValues { get; set; }

        public string? NewValues { get; set; }

        public AssociationMemberId Who { get; set; }

        public DateTimeOffset When { get; set; }
    }
}
