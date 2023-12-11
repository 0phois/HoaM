using HoaM.Domain.Common;

namespace HoaM.Domain
{
    public interface ITransaction : IEntity<TransactionId>, IAuditable, ISoftDelete
    {
        /// <summary>
        /// <see cref="AssociationMember"/> that submitted the <see cref="ITransaction"/>
        /// </summary>
        AssociationMember? SubmittedBy { get; set; }

        /// <summary>
        /// Short description of this <see cref="ITransaction"/>
        /// </summary>
        TransactionTitle Title { get; init; }

        /// <summary>
        /// Monetary value of this <see cref="ITransaction"/>
        /// </summary>
        Money Amount { get; init; }

        /// <summary>
        /// Date Due when the <see cref="ITransaction"/> is a <see cref="TransactionType.Credit"/> <br></br>
        /// Date Paid when the <see cref="ITransaction"/> is a <see cref="TransactionType.Debit"/>
        /// </summary>
        DateOnly EffectiveDate { get; }

        /// <summary>
        /// The <see cref="TransactionType">type</see> of <see cref="ITransaction"/>
        /// </summary>
        TransactionType Type { get; }

        /// <summary>
        /// Additional details/notes on this <see cref="ITransaction"/> 
        /// </summary>
        public Note? Memo { get; }
    }
}
