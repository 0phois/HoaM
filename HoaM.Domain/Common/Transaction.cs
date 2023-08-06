using HoaM.Domain.Contracts;
using HoaM.Domain.Entities;

namespace HoaM.Domain.Common
{
    public abstract class Transaction : Entity<TransactionId>, ITransaction
    {
        //inherit doc
        public TransactionTitle Title { get; set; }
        
        //inherit doc
        public decimal Amount { get; set; }

        //inherit doc
        public abstract TransactionType Type { get; }

        //inherit doc
        public AssociationMember Submitter { get; init; } = null!;

        //inherit doc
        public Note? Memo { get; set; }
    }
}
