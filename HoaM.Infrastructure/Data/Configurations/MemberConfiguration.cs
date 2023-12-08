using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaM.Infrastructure.Data
{
    internal sealed class AssociationMemberConfiguration : IEntityTypeConfiguration<AssociationMember>
    {
        public void Configure(EntityTypeBuilder<AssociationMember> builder)
        {
            builder.ToTable("AssociationMembers");

            builder.HasKey(member => member.Id);

            builder.Property(memeber => memeber.FirstName).IsRequired();
            builder.Property(member => member.LastName).IsRequired();

            builder.OwnsOne(member => member.Email, navigationBuilder =>
            {
                navigationBuilder.ToTable("Emails");
                navigationBuilder.HasKey(email => email.Id);
                navigationBuilder.Property(email => email.Address);
                navigationBuilder.Property(email => email.IsVerified);
                navigationBuilder.WithOwner().HasForeignKey("AssociationMemberId");
            });

            builder.OwnsMany(member => member.PhoneNumbers, navigationBuilder =>
            {
                navigationBuilder.ToTable("PhoneNumbers");
                navigationBuilder.HasKey(number => number.Id);
                navigationBuilder.Property(number => number.CountryCode);
                navigationBuilder.Property(number => number.AreaCode);
                navigationBuilder.Property(number => number.Prefix);
                navigationBuilder.Property(number => number.Number);
                navigationBuilder.Property(number => number.Type);
                navigationBuilder.WithOwner().HasForeignKey("AssociationMemberId");
            });

            builder.OwnsMany(member => member.Commitments, navigationBuilder =>
            {
                navigationBuilder.Property(x => x.CommitteeId).IsRequired();
                navigationBuilder.Property(x => x.CommitteeRole).IsRequired();
            });

            builder.Property(member => member.DeletedBy).IsRequired(false);
            builder.Property(member => member.DeletionDate).IsRequired(false);

            builder.HasQueryFilter(member => member.DeletionDate == null);
        }
    }
}
