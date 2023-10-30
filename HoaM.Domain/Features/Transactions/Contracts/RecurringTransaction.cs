namespace HoaM.Domain.Features
{
    public abstract class RecurringTransaction<T> : Event<T> where T : ITransaction
    {
        internal RecurringTransaction() { }

        protected RecurringTransaction(T activity, EventTitle title, DateTimeOffset start, DateTimeOffset stop, Schedule? schedule = null)
            : base(activity, title, new Occurance(start, stop), schedule) { }
    }
}
