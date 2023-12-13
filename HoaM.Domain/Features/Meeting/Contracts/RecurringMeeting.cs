namespace HoaM.Domain
{
    /// <summary>
    /// Represents an abstract class for recurring meetings.
    /// </summary>
    /// <typeparam name="T">The type of meeting associated with the recurring meeting.</typeparam>
    public abstract class RecurringMeeting<T> : Event<T> where T : Meeting
    {
        /// <summary>
        /// Protected constructor to prevent direct instantiation.
        /// </summary>
        private protected RecurringMeeting() { }

        /// <summary>
        /// Constructor to create a recurring meeting with specified parameters.
        /// </summary>
        /// <param name="activity">The meeting associated with the recurring meeting.</param>
        /// <param name="start">The start date and time of the recurring meeting.</param>
        /// <param name="stop">The end date and time of the recurring meeting.</param>
        /// <param name="schedule">The schedule for the recurring meeting (optional).</param>
        protected RecurringMeeting(T activity, DateTimeOffset start, DateTimeOffset stop, Schedule? schedule = null)
            : base(activity, EventTitle.From(activity.Title.Value), new Occurrence(start, stop), schedule) { }
    }

}
