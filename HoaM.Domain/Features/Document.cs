using HoaM.Domain.Common;
using MassTransit;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IO.Compression;

namespace HoaM.Domain.Features
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
        public FileName Title { get; private set; } = null!;

        /// <summary>
        /// Compressed <see cref="Document"/> data
        /// </summary>
        public byte[] Content { get; private set; } = Array.Empty<byte>();

        private Document() { }

        public static Document Create(FileName title, byte[] data)
        {
            if (data is null || data.Length == 0) throw new ArgumentNullException(nameof(data), "Value cannot be null or empty.");

            var memoryStream = new MemoryStream();

            using var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress);
            gzipStream.Write(data, 0, data.Length);

            return new() { Title = title, Content = memoryStream.ToArray() };
        }

        public void EditTitle(FileName title)
        {
            if (title == Title) return;

            Title = title;
        }

        public byte[] ExtractUncompressedData()
        {
            using var gzipStream = new GZipStream(new MemoryStream(Content), CompressionMode.Decompress);
            using var outputStream = new MemoryStream();
            gzipStream.CopyTo(outputStream);

            return outputStream.ToArray();
        }
    }
}
