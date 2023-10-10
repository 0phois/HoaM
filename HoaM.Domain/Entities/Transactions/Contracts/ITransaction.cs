using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Entities
{
    public interface ITransaction : IAuditable, ISoftDelete
    {
        /// <summary>
        /// Unique Id of this <see cref="ITransaction"/>
        /// </summary>
        TransactionId Id => TransactionId.From(NewId.Next().ToGuid());

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
