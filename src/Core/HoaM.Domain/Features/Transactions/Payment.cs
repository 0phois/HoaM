using HoaM.Domain.Exceptions;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a payment transaction.
    /// </summary>
    public sealed class Payment : Transaction
    {
        /// <summary>
        /// Gets the type of the transaction, which is always <see cref="TransactionType.Credit"/> for payments.
        /// </summary>
        public override TransactionType Type => TransactionType.Credit;

        /// <summary>
        /// Creates a new instance of the <see cref="Payment"/> class.
        /// </summary>
        /// <param name="title">The title of the payment.</param>
        /// <param name="amount">The amount of money involved in the payment.</param>
        /// <returns>A new <see cref="Payment"/> instance.</returns>
        public static Payment Create(TransactionTitle title, Money amount)
        {
            if (title is null) throw new DomainException(DomainErrors.Transaction.TitleNullOrEmpty);

            if (amount == default) throw new DomainException(DomainErrors.Transaction.AmountNullOrEmpty);

            return new Payment() { Title = title, Amount = amount };
        }

        /// <summary>
        /// Sets the deposit date for the payment.
        /// </summary>
        /// <param name="dueDate">The deposit date to set.</param>
        /// <returns>The updated <see cref="Payment"/> instance.</returns>
        public Payment WithDepositDate(DateOnly dueDate)
        {
            if (dueDate == default) throw new DomainException(DomainErrors.Transaction.DateNullOrEmpty);

            EffectiveDate = dueDate;

            return this;
        }
    }

}
