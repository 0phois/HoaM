using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Entities
{
    public sealed class Note : Entity<NoteId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Note"/>
        /// </summary>
        public override NoteId Id => NoteId.From(NewId.Next().ToGuid());

        /// <summary>
        /// <see cref="AssociationMember"/> that authored the <seealso cref="Note"/>
        /// </summary>
        public required AssociationMember Author { get; init; }

        /// <summary>
        /// Date and time the <see cref="Note"/> was created
        /// </summary>
        public DateTimeOffset CreatedDate { get; init; }

        /// <summary>
        /// Contents of the <see cref="Note"/>
        /// </summary>
        public required Text Text { get; init; }
    }
}
