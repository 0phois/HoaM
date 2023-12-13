using HoaM.Domain.Common;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents an interface for a financial transaction.
    /// </summary>
    public interface ITransaction : IEntity<TransactionId>, IAuditable, ISoftDelete
    {
        /// <summary>
        /// Gets or sets the member who submitted the transaction.
        /// </summary>
        AssociationMember? SubmittedBy { get; set; }

        /// <summary>
        /// Gets the title of the transaction.
        /// </summary>
        TransactionTitle Title { get; init; }

        /// <summary>
        /// Gets the amount of the transaction.
        /// </summary>
        Money Amount { get; init; }

        /// <summary>
        /// Gets the effective date of the transaction. 
        /// For <see cref="TransactionType.Credit"/> transactions, it is the due date.
        /// For <see cref="TransactionType.Debit"/> transactions, it is the paid date.
        /// </summary>
        DateOnly EffectiveDate { get; }

        /// <summary>
        /// Gets the type of the transaction.
        /// </summary>
        TransactionType Type { get; }

        /// <summary>
        /// Gets the optional memo associated with the transaction.
        /// </summary>
        Note? Memo { get; }
    }

}
