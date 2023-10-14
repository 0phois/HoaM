using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Features
{
    public class Event<T> : AuditableSoftDeleteEntity<EventId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Event"/>
        /// </summary>
        public override EventId Id => EventId.From(NewId.Next().ToGuid());

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
        public Occurance Occurance { get; init; } = null!;

        /// <summary>
        /// Interval at which the <see cref="Event"/> occurs
        /// </summary>
        public Schedule? Schedule { get; init; }

        internal Event() { }

        protected Event(T activity, EventTitle title, Occurance occurance, Schedule? schedule)
        {
            Activity = activity;
            Title = title;
            Occurance = occurance;
            Schedule = schedule;
        }

        public static Event<T> Create(T activity, EventTitle title, DateTimeOffset start, DateTimeOffset stop, Schedule? schedule = null)
        {
            return new() { Activity = activity, Title = title, Occurance = new(start, stop), Schedule = schedule };
        }

        public void EditTitle(EventTitle title) 
        {
            if (title == Title) return;

            Title = title; 
        }

        public IEnumerable<Event<T>> GetInstances(int limit)
        {
            yield return this;

            if (Schedule is null) yield break;

            foreach (var occurance in Schedule.GetOccurances(Occurance, limit))
            {
                yield return Event<T>.Create(Activity, Title, occurance.Start, occurance.Stop);
            }
        }
    }

    public record Occurance(DateTimeOffset Start, DateTimeOffset Stop);
}
