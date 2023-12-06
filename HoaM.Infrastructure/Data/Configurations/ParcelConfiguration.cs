using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaM.Infrastructure.Data
{
    internal sealed class ParcelConfiguration : IEntityTypeConfiguration<Parcel>
    {
        public void Configure(EntityTypeBuilder<Parcel> builder)
        {
            builder.ToTable("Parcels");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.StreetNumber).IsRequired(false);
            builder.Property(p => p.StreetName).IsRequired(false);
            builder.Property(p => p.Status).IsRequired();

            builder.HasMany(p => p.Transactions)
                   .WithOne()
                   .HasForeignKey("ParcelId")
                   .IsRequired(false);

            builder.HasMany(p => p.Lots)
                   .WithOne()
                   .HasForeignKey("ParcelId")
                   .IsRequired(false);

            builder.Property(p => p.DeletedBy).IsRequired(false);
            builder.Property(p => p.DeletionDate).IsRequired(false);

            builder.HasDiscriminator<string>("ParcelType")
                   .HasValue<GreenSpace>("GreenSpace")
                   .HasValue<Residence>("Residence");

            builder.HasQueryFilter(parcel => parcel.DeletionDate == null);
        }
    }

    internal sealed class ResidenceConfiguration : IEntityTypeConfiguration<Residence>
    {
        public void Configure(EntityTypeBuilder<Residence> builder)
        {
            builder.ToTable("Parcels");

            builder.HasMany(r => r.Residents)
                   .WithOne(x => x.Residence)
                   .HasForeignKey("ParcelId")
                   .IsRequired(false);
        }
    }

    internal sealed class GreenSpaceConfiguration : IEntityTypeConfiguration<GreenSpace>
    {
        public void Configure(EntityTypeBuilder<GreenSpace> builder)
        {
            builder.ToTable("Parcels");

            builder.Property(greenSpace => greenSpace.Title).IsRequired();

            builder.OwnsOne(greenSpace => greenSpace.Description, navigationBuilder =>
            {
                navigationBuilder.Property(note => note.Id);
                navigationBuilder.Property(note => note.Content);
                navigationBuilder.Property(note => note.CreatedBy);
                navigationBuilder.Property(note => note.CreatedDate);
                navigationBuilder.Property(note => note.LastModifiedBy);
                navigationBuilder.Property(note => note.LastModifiedDate);
            });

            builder.OwnsOne(greenSpace => greenSpace.OpeningHours, navigationBuilder =>
            {
                navigationBuilder.Property(openingHours => openingHours.OpeningTime);
                navigationBuilder.Property(openingHours => openingHours.ClosingTime);
            });

            builder.OwnsMany(greenSpace => greenSpace.Amenities, navigationBuilder =>
            {
                navigationBuilder.Property(text => text.Value);
                navigationBuilder.WithOwner().HasForeignKey("ParcelId");
            });

            builder.OwnsMany(greenSpace => greenSpace.Rules, navigationBuilder =>
            {
                navigationBuilder.Property(text => text.Value);
                navigationBuilder.WithOwner().HasForeignKey("ParcelId");
            });
        }
    }
}
