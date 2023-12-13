using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents an abstract class for financial transactions.
    /// </summary>
    public abstract class Transaction : AuditableSoftDeleteEntity<TransactionId>, ITransaction
    {
        /// <summary>
        /// Gets or sets the unique identifier of the transaction.
        /// </summary>
        public override TransactionId Id { get; protected set; } = TransactionId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Gets or sets the member who submitted the transaction.
        /// </summary>
        public AssociationMember? SubmittedBy { get; set; }

        /// <summary>
        /// Gets or initializes the title of the transaction.
        /// </summary>
        public required TransactionTitle Title { get; init; }

        /// <summary>
        /// Gets or initializes the amount of money involved in the transaction.
        /// </summary>
        public required Money Amount { get; init; }

        /// <summary>
        /// Gets or sets the effective date of the transaction.
        /// </summary>
        public DateOnly EffectiveDate { get; private protected set; }

        /// <summary>
        /// Gets the type of the transaction.
        /// </summary>
        public abstract TransactionType Type { get; }

        /// <summary>
        /// Gets or sets the optional memo associated with the transaction.
        /// </summary>
        public Note? Memo { get; set; }

        /// <summary>
        /// Sets the memo for the transaction.
        /// </summary>
        /// <param name="memo">The memo to set.</param>
        public void WithMemo(Note memo)
        {
            if (memo is null) throw new DomainException(DomainErrors.Transaction.MemoNullOrEmpty);

            Memo = memo;
        }
    }

}
