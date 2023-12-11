namespace HoaM.Domain
{
    public abstract class RecurringTransaction<T> : Event<T> where T : ITransaction
    {
        private protected RecurringTransaction() { }

        protected RecurringTransaction(T transaction, Schedule? schedule = null)
            : base(transaction,
                  EventTitle.From(transaction.Title.Value),
                   new Occurrence(transaction.EffectiveDate.ToDateTime(TimeOnly.MinValue), transaction.EffectiveDate.ToDateTime(TimeOnly.MaxValue)),
                   schedule)
        { }
    }
}
