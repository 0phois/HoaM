using HoaM.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaM.Infrastructure.Data.Configurations
{
    internal class NotificationTemplateConfiguration : IEntityTypeConfiguration<NotificationTemplate>
    {
        public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
        {
            builder.ToTable("NotificationTemplates");

            builder.HasKey(nt => nt.Id);

            builder.Property(nt => nt.Type).IsRequired();
            builder.Property(nt => nt.Title).IsRequired();
            builder.Property(nt => nt.Content).IsRequired();
        }
    }
}
