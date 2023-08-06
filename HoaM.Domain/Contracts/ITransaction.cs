using HoaM.Domain.Common;
using HoaM.Domain.Entities;
using MassTransit;

namespace HoaM.Domain.Contracts
{
    public interface ITransaction
    {
        /// <summary>
        /// Unique Id of this <see cref="ITransaction"/>
        /// </summary>
        TransactionId Id => TransactionId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Short description of this <see cref="ITransaction"/>
        /// </summary>
        TransactionTitle Title { get; set; }

        /// <summary>
        /// Monetary value of this <see cref="ITransaction"/>
        /// </summary>
        decimal Amount { get; set; }

        /// <summary>
        /// The <see cref="TransactionType">type</see> of <see cref="ITransaction"/>
        /// </summary>
        TransactionType Type { get; }

        /// <summary>
        /// Additional details/notes on this <see cref="ITransaction"/> 
        /// </summary>
        public Note? Memo { get; set; }

        /// <summary>
        /// <see cref="AssociationMember"/> that submitted this <seealso cref="ITransaction"/>
        /// </summary>
        AssociationMember Submitter { get; init; }
    }
}
