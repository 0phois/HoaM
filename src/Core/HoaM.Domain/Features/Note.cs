using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a note entity with an identifier of type <see cref="NoteId"/>.
    /// Inherits from <see cref="AuditableEntity{NoteId}"/> and implements methods for creating and editing notes.
    /// </summary>
    public sealed class Note : AuditableEntity<NoteId>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the note.
        /// </summary>
        public override NoteId Id { get; protected set; } = NoteId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Gets or sets the content of the note.
        /// </summary>
        public Text Content { get; private set; } = null!;

        /// <summary>
        /// Private constructor to prevent direct instantiation of the note without using the creation method.
        /// </summary>
        private Note() { }

        /// <summary>
        /// Creates a new note with the specified content.
        /// </summary>
        /// <param name="content">The content of the note.</param>
        /// <returns>A new instance of the <see cref="Note"/> class.</returns>
        public static Note Create(Text content)
        {
            if (content is null) throw new DomainException(DomainErrors.Note.ContentNullOrEmpty);

            return new Note { Content = content };
        }

        /// <summary>
        /// Edits the content of the note with the specified content.
        /// </summary>
        /// <param name="content">The new content of the note.</param>
        public void EditContent(Text content)
        {
            if (content is null) throw new DomainException(DomainErrors.Note.ContentNullOrEmpty);

            if (content == Content) return;

            Content = content;
        }
    }

}
