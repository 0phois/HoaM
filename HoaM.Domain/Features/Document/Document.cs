using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;
using System.IO.Compression;

namespace HoaM.Domain
{
    public sealed class Document : Entity<DocumentId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Document"/>
        /// </summary>
        public override DocumentId Id { get; protected set; } = DocumentId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Name of the <see cref="Document"/>
        /// </summary>
        public FileName Title { get; private set; } = null!;

        /// <summary>
        /// Compressed <see cref="Document"/> data
        /// </summary>
        public byte[] Content { get; private set; } = [];

        private Document() { }

        public static Document Create(FileName title, byte[] data)
        {
            if (title is null) throw new DomainException(DomainErrors.Document.TitleNullOrEmpty);

            if (data is null || data.Length == 0) throw new DomainException(DomainErrors.Document.DataNullOrEmpty);

            var compressedData = Compress(data);

            return new() { Title = title, Content = compressedData };
        }

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
