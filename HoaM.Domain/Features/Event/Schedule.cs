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

        public IEnumerable<Occurance> GetOccurances(Occurance first, int limit)
        {
            if (first is null) throw new DomainException(DomainErrors.Schedule.OccuranceNullOrEmpty);

            var occurance = first;
            var count = 0;

            while (count < limit || occurance.Start <= EndsAt)
            {
                count++;
                occurance = new Occurance(occurance.Start + Interval, occurance.Stop + Interval);

                yield return occurance;
            }
        }
    }
}
