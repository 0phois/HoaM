using HoaM.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaM.Infrastructure.Data.Configurations
{
    internal sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(n => n.Id);

            builder.HasOne(n => n.Template)
                   .WithMany()
                   .HasForeignKey("TemplateId")
                   .IsRequired();

            builder.HasOne(n => n.Recipient)
                   .WithMany(am => am.Notifications)
                   .HasForeignKey("AssociationMemberId")
                   .IsRequired(false);

            builder.Property(n => n.ReceivedDate).IsRequired(false);
            builder.Property(n => n.ReadDate).IsRequired();

        }
    }
}
