using HoaM.Domain.Exceptions;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a class for scheduling recurring events.
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// Gets the time interval between occurrences.
        /// </summary>
        public TimeSpan Interval { get; init; }

        /// <summary>
        /// Gets or sets the optional end date for the schedule.
        /// </summary>
        public DateTimeOffset? EndsAt { get; init; }

        /// <summary>
        /// Private constructor to enforce the use of factory methods.
        /// </summary>
        private Schedule() { }

        /// <summary>
        /// Creates a schedule with the specified interval and optional end date.
        /// </summary>
        /// <param name="interval">The time interval between occurrences.</param>
        /// <param name="until">The optional end date for the schedule.</param>
        /// <returns>An instance of the <see cref="Schedule"/> class.</returns>
        public static Schedule Create(TimeSpan interval, DateTimeOffset? until = null)
        {
            if (interval < TimeSpan.FromDays(1)) throw new DomainException(DomainErrors.Schedule.IntervalTooSmall);

            return new() { Interval = interval, EndsAt = until };
        }

        /// <summary>
        /// Creates a schedule with a weekly interval.
        /// </summary>
        /// <param name="until">Optional end date for the schedule.</param>
        /// <returns>The created schedule.</returns>
        public static Schedule CreateWeekly(DateTimeOffset? until = null) => new() { Interval = TimeSpan.FromDays(7), EndsAt = until };

        /// <summary>
        /// Creates a schedule with a bi-weekly interval.
        /// </summary>
        /// <param name="until">Optional end date for the schedule.</param>
        /// <returns>The created schedule.</returns>
        public static Schedule CreateBiWeekly(DateTimeOffset? until = null) => new() { Interval = TimeSpan.FromDays(14), EndsAt = until };

        /// <summary>
        /// Creates a schedule with a monthly interval.
        /// </summary>
        /// <param name="until">Optional end date for the schedule.</param>
        /// <returns>The created schedule.</returns>
        public static Schedule CreateMonthly(DateTimeOffset? until = null) => new() { Interval = TimeSpan.FromDays(30), EndsAt = until };

        /// <summary>
        /// Creates a schedule with a bi-monthly interval.
        /// </summary>
        /// <param name="until">Optional end date for the schedule.</param>
        /// <returns>The created schedule.</returns>
        public static Schedule CreateBiMonthly(DateTimeOffset? until = null) => new() { Interval = TimeSpan.FromDays(60), EndsAt = until };

        /// <summary>
        /// Creates a schedule with a quarterly interval.
        /// </summary>
        /// <param name="until">Optional end date for the schedule.</param>
        /// <returns>The created schedule.</returns>
        public static Schedule CreateQuarterly(DateTimeOffset? until = null) => new() { Interval = TimeSpan.FromDays(90), EndsAt = until };

        /// <summary>
        /// Creates a schedule with a semi-annual interval.
        /// </summary>
        /// <param name="until">Optional end date for the schedule.</param>
        /// <returns>The created schedule.</returns>
        public static Schedule CreateSemiAnnually(DateTimeOffset? until = null) => new() { Interval = TimeSpan.FromDays(180), EndsAt = until };

        /// <summary>
        /// Calculates the next occurrence based on the current occurrence.
        /// </summary>
        /// <param name="current">The current occurrence.</param>
        /// <returns>The next occurrence or null if it exceeds the optional end date.</returns>
        public Occurrence? NextOccurrence(Occurrence current)
        {
            if (current is null) throw new DomainException(DomainErrors.Schedule.OccurrenceNullOrEmpty);

            var next = new Occurrence(current.Start + Interval, current.Stop + Interval);

            if (EndsAt is not null && next.Start >= EndsAt) return null;

            return next;
        }

        /// <summary>
        /// Gets a sequence of occurrences starting from the specified occurrence, up to the specified limit.
        /// </summary>
        /// <param name="first">The starting occurrence.</param>
        /// <param name="limit">The maximum number of occurrences to generate.</param>
        /// <returns>A sequence of occurrences.</returns>
        public IEnumerable<Occurrence> GetOccurrences(Occurrence first, int limit)
        {
            if (first is null) throw new DomainException(DomainErrors.Schedule.OccurrenceNullOrEmpty);

            var occurrence = first;
            var count = 0;

            do
            {
                count++;
                yield return occurrence;

                occurrence = new Occurrence(occurrence.Start + Interval, occurrence.Stop + Interval);

            } while (count < limit && (EndsAt is null || occurrence.Start <= EndsAt));
        }
    }

}
