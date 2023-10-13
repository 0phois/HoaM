using HoaM.Domain.Common;
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
            return new() { Content = content };
        }

        public void EditContent(Text content)
        {
            if (content == Content) return;

            Content = content;
        }
    }
}
