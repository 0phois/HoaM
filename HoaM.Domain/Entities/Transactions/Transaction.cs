using HoaM.Domain.Common;

namespace HoaM.Domain.Entities
{
    public abstract class Transaction : AuditableSoftDeleteEntity<TransactionId>, ITransaction
    {
        public required AssociationMember SubmittedBy { get; init; }

        //inherit doc
        public required TransactionTitle Title { get; init; }

        //inherit doc
        public required decimal Amount { get; init; }

        //inherit doc
        public abstract TransactionType Type { get; }

        //inherit doc
        public Note? Memo { get; set; }
    }
}
