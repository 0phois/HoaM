using HoaM.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaM.Infrastructure.Data
{
    internal sealed class LotConfiguration : IEntityTypeConfiguration<Lot>
    {
        public void Configure(EntityTypeBuilder<Lot> builder)
        {
            builder.ToTable("Lots");

            builder.HasKey(lot => lot.Id);

            builder.Property(lot => lot.Number).IsRequired();
        }
    }
}
