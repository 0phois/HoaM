using HoaM.Domain.Exceptions;

namespace HoaM.Domain.Features
{
    public class Schedule
    {
        public TimeSpan Interval { get; init; }
        public DateTimeOffset? EndsAt { get; init; }

        private Schedule() { }

        public static Schedule Create(TimeSpan interval, DateTimeOffset? until = null)
        {
            if (interval < TimeSpan.FromDays(1)) throw new DomainException(DomainErrors.Schedule.IntervalTooSmall);

            return new() { Interval = interval, EndsAt = until };
        }

        public static Schedule CreateWeekly(DateTimeOffset? until = null) => new() { Interval = TimeSpan.FromDays(7), EndsAt = until };

        public static Schedule CreateBiWeekly(DateTimeOffset? until = null) => new() { Interval = TimeSpan.FromDays(14), EndsAt = until };

        public static Schedule CreateMonthly(DateTimeOffset? until = null) => new() { Interval = TimeSpan.FromDays(30), EndsAt = until };

        public static Schedule CreateBiMonthly(DateTimeOffset? until = null) => new() { Interval = TimeSpan.FromDays(60), EndsAt = until };

        public static Schedule CreateQuarterly(DateTimeOffset? until = null) => new() { Interval = TimeSpan.FromDays(90), EndsAt = until };

        public static Schedule CreateSemiAnnually(DateTimeOffset? until = null) => new() { Interval = TimeSpan.FromDays(180), EndsAt = until };

        public static Schedule CreateAnnually(DateTimeOffset? until = null) => new() { Interval = TimeSpan.FromDays(365), EndsAt = until };

        public Occurrence? NextOccurrence(Occurrence current)
        {
            if (current is null) throw new DomainException(DomainErrors.Schedule.OccuranceNullOrEmpty);

            var next = new Occurrence(current.Start + Interval, current.Stop + Interval);

            if (EndsAt is not null && next.Start >= EndsAt) return null;

            return next;
        }

        public IEnumerable<Occurrence> GetOccurrences(Occurrence first, int limit)
        {
            if (first is null) throw new DomainException(DomainErrors.Schedule.OccuranceNullOrEmpty);

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
