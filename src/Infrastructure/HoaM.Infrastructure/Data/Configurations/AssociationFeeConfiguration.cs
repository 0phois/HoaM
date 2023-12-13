using HoaM.Domain;
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

            builder.HasKey(fee => fee.Id);

            builder.Property(fee => fee.Title).IsRequired();

            builder.Property(fee => fee.Data).IsRequired()
                   .HasConversion(TValue => JsonSerializer.Serialize(TValue, jsonOptions),
                                  jsonString => JsonSerializer.Deserialize<Expense>(jsonString, jsonOptions)!);

            builder.OwnsOne(fee => fee.Occurrence, occurrence =>
            {
                occurrence.Property(o => o.Start).IsRequired();
                occurrence.Property(o => o.Stop).IsRequired();
            });

            builder.OwnsOne(fee => fee.Schedule, schedule =>
            {
                schedule.Property(s => s.Interval);
                schedule.Property(s => s.EndsAt);
            });

            builder.HasQueryFilter(fee => fee.DeletionDate == null);
        }
    }
}
