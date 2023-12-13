using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;
using System.IO.Compression;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a document entity.
    /// Inherits from <see cref="Entity{DocumentId}"/>.
    /// </summary>
    public sealed class Document : Entity<DocumentId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Document"/>.
        /// </summary>
        public override DocumentId Id { get; protected set; } = DocumentId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Name of the <see cref="Document"/>.
        /// </summary>
        public FileName Title { get; private set; } = null!;

        /// <summary>
        /// Compressed content data of the <see cref="Document"/>.
        /// </summary>
        public byte[] Content { get; private set; } = [];

        private Document() { }

        /// <summary>
        /// Creates a new instance of <see cref="Document"/> with the specified title and compressed data.
        /// </summary>
        /// <param name="title">The name of the document.</param>
        /// <param name="data">The compressed document data.</param>
        /// <returns>A new <see cref="Document"/> instance.</returns>
        public static Document Create(FileName title, byte[] data)
        {
            if (title is null) throw new DomainException(DomainErrors.Document.TitleNullOrEmpty);
            if (data is null || data.Length == 0) throw new DomainException(DomainErrors.Document.DataNullOrEmpty);

            var compressedData = Compress(data);

            return new() { Title = title, Content = compressedData };
        }

        /// <summary>
        /// Edits the title of the <see cref="Document"/>.
        /// </summary>
        /// <param name="title">The new title for the document.</param>
        public void EditTitle(FileName title)
        {
            if (title is null) throw new DomainException(DomainErrors.Document.TitleNullOrEmpty);

            if (title == Title) return;

            Title = title;
        }

        private static byte[] Compress(byte[] data)
        {
            using var compressedStream = new MemoryStream();
            using (var gzipStream = new GZipStream(compressedStream, CompressionLevel.Optimal, false))
            {
                gzipStream.Write(data);
            }

            return compressedStream.ToArray();
        }

        /// <summary>
        /// Extracts the uncompressed data of the <see cref="Document"/>.
        /// </summary>
        /// <returns>A task representing the asynchronous operation with the uncompressed data.</returns>
        public async Task<byte[]> ExtractUncompressedDataAsync()
        {
            await using var uncompressedStream = new MemoryStream();
            await using (var compressedStream = new MemoryStream(Content))
            {
                await using var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress);
                await gzipStream.CopyToAsync(uncompressedStream);
            }

            return uncompressedStream.ToArray();
        }
    }

}
