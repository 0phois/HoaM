using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaM.Infrastructure.Data
{
    internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(transaction => transaction.Id);

            builder.Property(transaction => transaction.Title).IsRequired();
            builder.Property(transaction => transaction.Amount).IsRequired();
            builder.Property(transaction => transaction.EffectiveDate).IsRequired();

            builder.HasOne(transaction => transaction.SubmittedBy)
                   .WithMany()
                   .HasForeignKey("AssociationMemberId")
                   .IsRequired(false);

            builder.OwnsOne(transaction => transaction.Memo, navigationBuilder =>
            {
                navigationBuilder.Property(note => note.Id);
                navigationBuilder.Property(note => note.Content);
                navigationBuilder.Property(note => note.CreatedBy);
                navigationBuilder.Property(note => note.CreatedDate);
                navigationBuilder.Property(note => note.LastModifiedBy);
                navigationBuilder.Property(note => note.LastModifiedDate);
            });

        }
    }

    internal sealed class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Transactions");
        }
    }

    internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Transactions");
        }
    }
}
