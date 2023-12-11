using HoaM.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaM.Infrastructure.Data
{
    internal sealed class MeetingMinutesConfiguration : IEntityTypeConfiguration<MeetingMinutes>
    {
        public void Configure(EntityTypeBuilder<MeetingMinutes> builder)
        {
            builder.ToTable("MeetingMinutes");

            builder.HasKey(minutes => minutes.Id);

            builder.OwnsMany(minutes => minutes.AgendaNotes, navigationBuilder =>
            {
                navigationBuilder.Property(note => note.Id);
                navigationBuilder.Property(note => note.Content);
                navigationBuilder.Property(note => note.CreatedBy);
                navigationBuilder.Property(note => note.CreatedDate);
                navigationBuilder.Property(note => note.LastModifiedBy);
                navigationBuilder.Property(note => note.LastModifiedDate);
            });

            builder.OwnsMany(minutes => minutes.ActionItems, navigationBuilder =>
            {
                navigationBuilder.ToTable("ActionItems");
                navigationBuilder.Property(note => note.Id);
                navigationBuilder.Property(note => note.Content);
                navigationBuilder.Property(note => note.CreatedBy);
                navigationBuilder.Property(note => note.CreatedDate);
                navigationBuilder.Property(note => note.LastModifiedBy);
                navigationBuilder.Property(note => note.LastModifiedDate);
                navigationBuilder.Property(note => note.AssignedTo);
                navigationBuilder.Property(note => note.IsCompleted);
            });

            builder.OwnsMany(minutes => minutes.Notes, navigationBuilder =>
            {
                navigationBuilder.Property(note => note.Id);
                navigationBuilder.Property(note => note.Content);
                navigationBuilder.Property(note => note.CreatedBy);
                navigationBuilder.Property(note => note.CreatedDate);
                navigationBuilder.Property(note => note.LastModifiedBy);
                navigationBuilder.Property(note => note.LastModifiedDate);
            });

            builder.HasMany(minutes => minutes.Attendees).WithMany();

            builder.Property(minutes => minutes.Publisher).IsRequired(false);
            builder.Property(minutes => minutes.PublishedDate).IsRequired(false);

            builder.HasQueryFilter(minutes => minutes.DeletionDate == null);
        }
    }
}
