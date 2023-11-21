using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Features
{
    public abstract class Transaction : AuditableSoftDeleteEntity<TransactionId>, ITransaction
    {
        public override TransactionId Id { get; protected set; } = TransactionId.From(NewId.Next().ToGuid());
        public required AssociationMember SubmittedBy { get; init; }

        public required TransactionTitle Title { get; init; }

        public required decimal Amount { get; init; }

        public DateTimeOffset EffectiveDate { get; set; }

        public abstract TransactionType Type { get; }

        public Note? Memo { get; set; }
    }
}
