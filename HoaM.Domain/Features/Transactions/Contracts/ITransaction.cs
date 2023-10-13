using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Features
{
    public interface ITransaction : IAuditable, ISoftDelete
    {
        /// <summary>
        /// Unique Id of this <see cref="ITransaction"/>
        /// </summary>
        TransactionId Id => TransactionId.From(NewId.Next().ToGuid());

        /// <summary>
        /// <see cref="AssociationMember"/> that submitted the <see cref="ITransaction"/>
        /// </summary>
        AssociationMember SubmittedBy { get; init; }

        /// <summary>
        /// Short description of this <see cref="ITransaction"/>
        /// </summary>
        TransactionTitle Title { get; init; }

        /// <summary>
        /// Monetary value of this <see cref="ITransaction"/>
        /// </summary>
        decimal Amount { get; init; }

        /// <summary>
        /// Date Due when the <see cref="ITransaction"/> is a <see cref="TransactionType.Credit"/> <br></br>
        /// Date Paid when the <see cref="ITransaction"/> is a <see cref="TransactionType.Debit"/>
        /// </summary>
        DateTimeOffset EffectiveDate { get; set; }

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
