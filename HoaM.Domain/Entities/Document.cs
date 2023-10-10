using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Entities
{
    public sealed class Document : Entity<DocumentId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Document"/>
        /// </summary>
        public override DocumentId Id => DocumentId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Name of the <see cref="Document"/>
        /// </summary>
        public required FileName Title { get; set; }

        /// <summary>
        /// Compressed <see cref="Document"/> data
        /// </summary>
        public byte[] Content { get; set; } = Array.Empty<byte>();
    }
}
