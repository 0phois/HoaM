using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain.Features
{
    public sealed class Note : AuditableEntity<NoteId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Note"/>
        /// </summary>
        public override NoteId Id => NoteId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Contents of the <see cref="Note"/>
        /// </summary>
        public Text Content { get; private set; } = null!;

        private Note() { }

        public static Note Create(Text content)
        {
            if (content is null) throw new DomainException(DomainErrors.Note.ContentNullOrEmpty);

            return new() { Content = content };
        }

        public void EditContent(Text content)
        {
            if (content is null) throw new DomainException(DomainErrors.Note.ContentNullOrEmpty);

            if (content == Content) return;

            Content = content;
        }
    }
}
