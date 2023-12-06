using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaM.Infrastructure.Data
{
    internal sealed class CommitteeConfiguration : IEntityTypeConfiguration<Committee>
    {
        public void Configure(EntityTypeBuilder<Committee> builder)
        {
            builder.ToTable("Committees");

            builder.HasKey(committee => committee.Id);

            builder.Property(committee => committee.Name).IsRequired();
            builder.Property(committee => committee.MissionStatement);
            builder.Property(committee => committee.EstablishedDate);
            builder.Property(committee => committee.DissolvedDate);


            builder.OwnsMany(committee => committee.AdditionalDetails, navigationBuilder =>
            {
                navigationBuilder.Property(note => note.Id);
                navigationBuilder.Property(note => note.Content);
                navigationBuilder.Property(note => note.CreatedBy);
                navigationBuilder.Property(note => note.CreatedDate);
                navigationBuilder.Property(note => note.LastModifiedBy);
                navigationBuilder.Property(note => note.LastModifiedDate);
            });

            builder.HasMany(committee => committee.Members)
                   .WithMany(x => x.Committees);

            builder.HasMany(committee => committee.Meetings)
                   .WithOne()
                   .HasForeignKey("CommitteeId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(committee => committee.DeletedBy);
            builder.Property(committee => committee.DeletionDate);

            builder.Ignore(committee => committee.IsDeleted);
            builder.Ignore(committee => committee.IsDissolved);
            builder.Ignore(committee => committee.IsActive);
        }
    }
}