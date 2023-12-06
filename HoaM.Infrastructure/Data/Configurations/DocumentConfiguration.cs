using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaM.Infrastructure.Data
{
    internal sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");

            builder.HasKey(document => document.Id);

            builder.Property(document => document.Title).IsRequired();
            builder.Property(document => document.Content).IsRequired();
        }
    }
}
