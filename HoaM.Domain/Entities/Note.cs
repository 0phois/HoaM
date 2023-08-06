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
        /// Date and time the <see cref="Note"/> was created
        /// </summary>
        public DateTimeOffset CreatedDate { get; init; }

        /// <summary>
        /// Contents of the <see cref="Note"/>
        /// </summary>
        public Text Text { get; init; }
    }
}
