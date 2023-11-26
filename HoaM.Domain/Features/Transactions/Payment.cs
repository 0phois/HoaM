using HoaM.Domain.Exceptions;

namespace HoaM.Domain.Features
{
    public sealed class Payment : Transaction
    {
        public override TransactionType Type => TransactionType.Credit;

        public static Payment Create(TransactionTitle title, Money amount)
        {
            if (title is null) throw new DomainException(DomainErrors.Transaction.TitleNullOrEmpty);

            if (amount == default) throw new DomainException(DomainErrors.Transaction.AmountNullOrEmpty);

            return new Payment() { Title = title, Amount = amount };
        }

        public Payment WithDepositDate(DateOnly dueDate)
        {
            if (dueDate == default) throw new DomainException(DomainErrors.Transaction.DateNullOrEmpty);

            EffectiveDate = dueDate;

            return this;
        }
    }
}
