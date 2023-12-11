using HoaM.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaM.Infrastructure.Data
{
    internal sealed class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder.ToTable("Meetings");

            builder.HasKey(meeting => meeting.Id);

            builder.Property(meeting => meeting.Title).IsRequired();
            builder.Property(meeting => meeting.Description).IsRequired(false);

            builder.OwnsMany(meeting => meeting.Agenda, navigationBuilder =>
            {
                navigationBuilder.Property(note => note.Id);
                navigationBuilder.Property(note => note.Content);
                navigationBuilder.Property(note => note.CreatedBy);
                navigationBuilder.Property(note => note.CreatedDate);
                navigationBuilder.Property(note => note.LastModifiedBy);
                navigationBuilder.Property(note => note.LastModifiedDate);
            });

            builder.Property(meeting => meeting.ScheduledDate).IsRequired();

            builder.HasOne(meeting => meeting.Minutes)
                   .WithOne(x => x.Meeting)
                   .HasPrincipalKey<Meeting>(x => x.Id)
                   .HasForeignKey<MeetingMinutes>("MeetingId")
                   .IsRequired(false);

            builder.HasOne(meeting => meeting.Committee)
                   .WithMany(x => x.Meetings)
                   .HasForeignKey("CommitteeId")
                   .IsRequired();

            builder.HasQueryFilter(meeting => meeting.DeletionDate == null);
        }
    }
}
