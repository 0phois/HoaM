using MassTransit;

namespace HoaM.Domain.Entities
{
    public interface ITransaction
    {
        /// <summary>
        /// Unique Id of this <see cref="ITransaction"/>
        /// </summary>
        TransactionId Id => TransactionId.From(NewId.Next().ToGuid());

        /// <summary>
        /// <see cref="AssociationMember"/> that submitted this <seealso cref="ITransaction"/>
        /// </summary>
        AssociationMember Submitter { get; init; }

        /// <summary>
        /// Short description of this <see cref="ITransaction"/>
        /// </summary>
        TransactionTitle Title { get; init; }

        /// <summary>
        /// Monetary value of this <see cref="ITransaction"/>
        /// </summary>
        decimal Amount { get; init; }

        /// <summary>
        /// The <see cref="TransactionType">type</see> of <see cref="ITransaction"/>
        /// </summary>
        TransactionType Type { get; }

        /// <summary>
        /// Additional details/notes on this <see cref="ITransaction"/> 
        /// </summary>
        public Note? Memo { get; set; }
    }
}
