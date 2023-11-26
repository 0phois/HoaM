using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain.Features
{
    public abstract class Transaction : AuditableSoftDeleteEntity<TransactionId>, ITransaction
    {
        public override TransactionId Id { get; protected set; } = TransactionId.From(NewId.Next().ToGuid());

        public AssociationMember? SubmittedBy { get; set; }

        public required TransactionTitle Title { get; init; }

        public required Money Amount { get; init; }

        public DateOnly EffectiveDate { get; private protected set; }

        public abstract TransactionType Type { get; }

        public Note? Memo { get; set; }

        public void WithMemo(Note memo)
        {
            if (memo is null) throw new DomainException(DomainErrors.Transaction.MemoNullOrEmpty);

            Memo = memo;
        }
    }
}
