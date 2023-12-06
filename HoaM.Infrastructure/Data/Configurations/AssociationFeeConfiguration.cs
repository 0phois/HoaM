using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace HoaM.Infrastructure.Data
{
    internal sealed class AssociationFeeConfiguration : IEntityTypeConfiguration<AssociationFee>
    {
        public void Configure(EntityTypeBuilder<AssociationFee> builder)
        {
            var jsonOptions = new JsonSerializerOptions();

            builder.ToTable("AssociationFees");

            builder.HasKey(@event => @event.Id);

            builder.Property(@event => @event.Title).IsRequired();

            builder.Property(@event => @event.Data).IsRequired()
                   .HasConversion(TValue => JsonSerializer.Serialize(TValue, jsonOptions),
                                  jsonString => JsonSerializer.Deserialize<Expense>(jsonString, jsonOptions)!);

            builder.OwnsOne(@event => @event.Occurrence, occurrence =>
            {
                occurrence.Property(o => o.Start).IsRequired();
                occurrence.Property(o => o.Stop).IsRequired();
            });

            builder.OwnsOne(@event => @event.Schedule, schedule =>
            {
                schedule.Property(s => s.Interval);
                schedule.Property(s => s.EndsAt);
            });
        }
    }
}
