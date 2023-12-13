namespace HoaM.Domain
{
    /// <summary>
    /// Represents an abstract class for recurring financial transactions.
    /// </summary>
    /// <typeparam name="T">Type of the recurring transaction implementing <see cref="ITransaction"/>.</typeparam>
    public abstract class RecurringTransaction<T> : Event<T> where T : ITransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecurringTransaction{T}"/> class.
        /// </summary>
        private protected RecurringTransaction() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecurringTransaction{T}"/> class with specified transaction and schedule.
        /// </summary>
        /// <param name="transaction">The recurring transaction.</param>
        /// <param name="schedule">The schedule for the recurring transaction.</param>
        protected RecurringTransaction(T transaction, Schedule? schedule = null)
            : base(transaction,
                   EventTitle.From(transaction.Title.Value),
                   new Occurrence(transaction.EffectiveDate.ToDateTime(TimeOnly.MinValue), transaction.EffectiveDate.ToDateTime(TimeOnly.MaxValue)),
                   schedule)
        { }
    }

}
