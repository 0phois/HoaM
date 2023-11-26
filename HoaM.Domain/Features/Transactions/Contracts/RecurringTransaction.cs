namespace HoaM.Domain.Features
{
    public abstract class RecurringTransaction<T> : Event<T> where T : ITransaction
    {
        private protected RecurringTransaction() { }

        protected RecurringTransaction(T transaction, EventTitle title, Schedule? schedule = null)
            : base(transaction,
                   title,
                   new Occurrence(transaction.EffectiveDate.ToDateTime(TimeOnly.MinValue), transaction.EffectiveDate.ToDateTime(TimeOnly.MaxValue)),
                   schedule) { }
    }
}
