using HoaM.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaM.Infrastructure.Data
{
    internal sealed class CommunityConfiguration : IEntityTypeConfiguration<Community>
    {
        public void Configure(EntityTypeBuilder<Community> builder)
        {
            builder.ToTable("Communities");

            builder.HasKey(community => community.Id);

            builder.Property(community => community.Name).IsRequired();

            builder.HasMany(community => community.AssociationMembers)
                   .WithOne()
                   .HasForeignKey("CommunityId")
                   .IsRequired(false);

            builder.HasMany(community => community.AssociationFees)
                   .WithOne()
                   .HasForeignKey("CommunityId")
                   .IsRequired(false);

            builder.HasMany(community => community.Transactions)
                   .WithOne()
                   .HasForeignKey("CommunityId")
                   .IsRequired(false);

            builder.HasMany(community => community.Committees)
                   .WithOne()
                   .HasForeignKey("CommunityId")
                   .IsRequired(false);

            builder.HasMany(community => community.Meeting)
                   .WithOne()
                   .HasForeignKey("CommunityId")
                   .IsRequired(false);

            builder.HasMany(community => community.Events)
                   .WithOne()
                   .HasForeignKey("CommunityId")
                   .IsRequired(false);

            builder.HasMany(community => community.Lots)
                   .WithOne()
                   .HasForeignKey("CommunityId")
                   .IsRequired(false);

            builder.HasMany(community => community.Parcels)
                   .WithOne()
                   .HasForeignKey("CommunityId")
                   .IsRequired(false);

            builder.HasMany(community => community.Articles)
                   .WithOne()
                   .HasForeignKey("CommunityId")
                   .IsRequired(false);

            builder.HasMany(community => community.Documents)
                   .WithOne()
                   .HasForeignKey("CommunityId")
                   .IsRequired(false);

            builder.HasMany(community => community.Notifications)
                   .WithOne()
                   .HasForeignKey("CommunityId")
                   .IsRequired(false);

        }
    }
}
