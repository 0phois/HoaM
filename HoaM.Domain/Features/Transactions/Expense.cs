using HoaM.Domain.Exceptions;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents an expense transaction.
    /// </summary>
    public class Expense : Transaction
    {
        /// <summary>
        /// Gets the type of the transaction, which is always <see cref="TransactionType.Debit"/> for expenses.
        /// </summary>
        public override TransactionType Type => TransactionType.Debit;

        /// <summary>
        /// Creates a new instance of the <see cref="Expense"/> class.
        /// </summary>
        /// <param name="title">The title of the expense.</param>
        /// <param name="amount">The amount of money involved in the expense.</param>
        /// <returns>A new <see cref="Expense"/> instance.</returns>
        public static Expense Create(TransactionTitle title, Money amount)
        {
            if (title is null) throw new DomainException(DomainErrors.Transaction.TitleNullOrEmpty);

            if (amount == default) throw new DomainException(DomainErrors.Transaction.AmountNullOrEmpty);

            return new Expense() { Title = title, Amount = amount };
        }

        /// <summary>
        /// Sets the due date for the expense.
        /// </summary>
        /// <param name="dueDate">The due date to set.</param>
        /// <returns>The updated <see cref="Expense"/> instance.</returns>
        public Expense WithDueDate(DateOnly dueDate)
        {
            if (dueDate == default) throw new DomainException(DomainErrors.Transaction.DateNullOrEmpty);

            EffectiveDate = dueDate;

            return this;
        }
    }
}

