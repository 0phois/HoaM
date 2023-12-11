namespace HoaM.Domain
{
    public abstract class RecurringMeeting<T> : Event<T> where T : Meeting
    {
        private protected RecurringMeeting() { }

        protected RecurringMeeting(T activity, DateTimeOffset start, DateTimeOffset stop, Schedule? schedule = null)
            : base(activity, EventTitle.From(activity.Title.Value), new Occurrence(start, stop), schedule) { }
    }
}
