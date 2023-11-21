using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain.Features
{
    public class Event<T> : AuditableSoftDeleteEntity<EventId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Event"/>
        /// </summary>
        public override EventId Id { get; protected set; } = EventId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Represents the basis of the event
        /// </summary>
        public T Activity { get; private set; } = default!;

        /// <summary>
        /// Name of the <see cref="Event"/>
        /// </summary>
        public EventTitle Title { get; private set; } = null!;

        /// <summary>
        /// Start time and end time of the <see cref="Event"/>
        /// </summary>
        public Occurrence Occurrence { get; init; } = null!;

        /// <summary>
        /// Interval at which the <see cref="Event"/> occurs
        /// </summary>
        public Schedule? Schedule { get; init; }

        internal Event() { }

        protected Event(T activity, EventTitle title, Occurrence occurance, Schedule? schedule)
        {
            if (activity is null) throw new DomainException(DomainErrors.Event.ActivityNullOrEmpty);

            if (title is null) throw new DomainException(DomainErrors.Event.TitleNullOrEmpty);

            if (occurance is null) throw new DomainException(DomainErrors.Event.OccuranceNullOrEmpty);

            Activity = activity;
            Title = title;
            Occurrence = occurance;
            Schedule = schedule;
        }

        public static Event<T> Create(T activity, EventTitle title, DateTimeOffset start, DateTimeOffset stop, Schedule? schedule = null)
        {
            if (start == default) throw new DomainException(DomainErrors.Event.StartNullOrEmpty);

            if (stop == default) throw new DomainException(DomainErrors.Event.StopNullOrEmpty);

            var occurance = new Occurrence(start, stop);

            return new(activity, title, occurance, schedule);
        }

        public void EditTitle(EventTitle title)
        {
            if (title is null) throw new DomainException(DomainErrors.Event.TitleNullOrEmpty);
           
            if (title == Title) return;

            Title = title;
        }

        public IEnumerable<Event<T>> GetInstances(int limit)
        {
            if (limit <= 0) throw new DomainException(DomainErrors.Event.InvalidLimit);

            yield return this;

            if (Schedule is null) yield break;

            var nextOccurrence = Schedule.NextOccurrence(Occurrence);

            if (nextOccurrence is null) yield break;

            var occurrences = Schedule.GetOccurrences(nextOccurrence, limit - 1).ToList();

            foreach (var occurrence in occurrences)
            {
                yield return Event<T>.Create(Activity, Title, occurrence.Start, occurrence.Stop);
            }
        }
    }

    public record Occurrence(DateTimeOffset Start, DateTimeOffset Stop);
}
