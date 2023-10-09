using HoaM.Domain.Common;

namespace HoaM.Domain.Entities
{
    public abstract class Transaction : Entity<TransactionId>, ITransaction
    {
        //inherit doc
        public required AssociationMember Submitter { get; init; }

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
