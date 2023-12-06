using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaM.Infrastructure.Data
{
    internal sealed class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");

            builder.HasKey(article => article.Id);

            builder.Property(article => article.Type).IsRequired();
            builder.Property(article => article.Title).IsRequired();
            builder.Property(article => article.Body).IsRequired();
            builder.Property(article => article.IsPinned).HasDefaultValue(false);
            builder.Property(article => article.PublishedDate).IsRequired(false);
        }
    }
}
