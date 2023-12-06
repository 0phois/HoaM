using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaM.Infrastructure.Data
{
    internal sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");

            builder.HasKey(auditLog => auditLog.Id);

            builder.Property(auditLog => auditLog.TableName).IsRequired();
            builder.Property(auditLog => auditLog.RecordId).IsRequired();
            builder.Property(auditLog => auditLog.Action).IsRequired();
            builder.Property(auditLog => auditLog.ChangedColumns).HasMaxLength(255);
            builder.Property(auditLog => auditLog.OldValues).HasColumnType("nvarchar(max)");
            builder.Property(auditLog => auditLog.NewValues).HasColumnType("nvarchar(max)");
            builder.Property(auditLog => auditLog.Who).IsRequired();
            builder.Property(auditLog => auditLog.When).IsRequired();

            builder.HasOne<AssociationMember>()
                   .WithMany()
                   .HasForeignKey(auditLog => auditLog.Who)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
