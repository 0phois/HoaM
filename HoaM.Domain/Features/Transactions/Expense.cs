using HoaM.Domain.Exceptions;

namespace HoaM.Domain
{
    public class Expense : Transaction
    {
        public override TransactionType Type => TransactionType.Debit;

        private Expense() { }

        public static Expense Create(TransactionTitle title, Money amount)
        {
            if (title is null) throw new DomainException(DomainErrors.Transaction.TitleNullOrEmpty);

            if (amount == default) throw new DomainException(DomainErrors.Transaction.AmountNullOrEmpty);

            return new Expense() { Title = title, Amount = amount };
        }

        public Expense WithDueDate(DateOnly dueDate)
        {
            if (dueDate == default) throw new DomainException(DomainErrors.Transaction.DateNullOrEmpty);

            EffectiveDate = dueDate;

            return this;
        }
    }
}
