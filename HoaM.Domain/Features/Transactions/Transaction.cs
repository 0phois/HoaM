using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public abstract class Transaction : AuditableSoftDeleteEntity<TransactionId>, ITransaction
    {
        public required AssociationMember SubmittedBy { get; init; }

        public required TransactionTitle Title { get; init; }

        public required decimal Amount { get; init; }

        public DateTimeOffset EffectiveDate { get; set; }

        public abstract TransactionType Type { get; }

        public Note? Memo { get; set; }
    }
}
