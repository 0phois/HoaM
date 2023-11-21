namespace HoaM.Domain.Features
{
    public abstract class RecurringMeeting<T> : Event<T> where T : Meeting
    {
        private protected RecurringMeeting() { }

        protected RecurringMeeting(T activity, EventTitle title, DateTimeOffset start, DateTimeOffset stop, Schedule? schedule = null)
            : base(activity, title, new Occurrence(start, stop), schedule) { }
    }
}
