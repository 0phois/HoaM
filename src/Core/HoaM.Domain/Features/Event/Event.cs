using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents an abstract class for managing and auditing events with soft delete functionality.
    /// Inherits from <see cref="AuditableSoftDeleteEntity{TId}"/>.
    /// </summary>
    public abstract class Event : AuditableSoftDeleteEntity<EventId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Event"/>.
        /// </summary>
        public override EventId Id { get; protected set; } = EventId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Name of the <see cref="Event"/>.
        /// </summary>
        public EventTitle Title { get; protected set; } = null!;

        /// <summary>
        /// Start time and end time of the <see cref="Event"/>.
        /// </summary>
        public Occurrence Occurrence { get; init; } = null!;

        /// <summary>
        /// Interval at which the <see cref="Event"/> occurs.
        /// </summary>
        public Schedule? Schedule { get; init; }

        /// <summary>
        /// Edit the title of the <see cref="Event"/>.
        /// </summary>
        /// <param name="title">The new title for the event.</param>
        public void EditTitle(EventTitle title)
        {
            if (title is null) throw new DomainException(DomainErrors.Event.TitleNullOrEmpty);

            if (title == Title) return;

            Title = title;
        }
    }

    /// <summary>
    /// Represents a generic class for managing events with a specified activity type.
    /// Inherits from <see cref="Event"/>.
    /// </summary>
    /// <typeparam name="T">The type representing the basis of the event.</typeparam>
    public class Event<T> : Event
    {
        /// <summary>
        /// Represents the basis of the event.
        /// </summary>
        public T Data { get; private set; } = default!;

        /// <summary>
        /// Default constructor for <see cref="Event{T}"/>.
        /// </summary>
        private protected Event() { }

        /// <summary>
        /// Constructor for creating an instance of <see cref="Event{T}"/> with specified parameters.
        /// </summary>
        /// <param name="activity">The activity associated with the event.</param>
        /// <param name="title">The title of the event.</param>
        /// <param name="occurance">The occurrence details of the event.</param>
        /// <param name="schedule">The schedule for the event (optional).</param>
        private protected Event(T activity, EventTitle title, Occurrence occurance, Schedule? schedule)
        {
            if (activity is null) throw new DomainException(DomainErrors.Event.ActivityNullOrEmpty);

            if (title is null) throw new DomainException(DomainErrors.Event.TitleNullOrEmpty);

            if (occurance is null) throw new DomainException(DomainErrors.Event.OccuranceNullOrEmpty);

            Title = title;
            Data = activity;
            Occurrence = occurance;
            Schedule = schedule;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Event{T}"/>.
        /// </summary>
        /// <param name="activity">The activity associated with the event.</param>
        /// <param name="title">The title of the event.</param>
        /// <param name="start">The start time of the event.</param>
        /// <param name="stop">The end time of the event.</param>
        /// <param name="schedule">The schedule for the event (optional).</param>
        /// <returns>An instance of <see cref="Event{T}"/>.</returns>
        public static Event<T> Create(T activity, EventTitle title, DateTimeOffset start, DateTimeOffset stop, Schedule? schedule = null)
        {
            if (start == default) throw new DomainException(DomainErrors.Event.StartNullOrEmpty);

            if (stop == default) throw new DomainException(DomainErrors.Event.StopNullOrEmpty);

            var occurance = new Occurrence(start, stop);

            return new(activity, title, occurance, schedule);
        }

        /// <summary>
        /// Gets a sequence of instances of <see cref="Event{T}"/> based on the schedule.
        /// </summary>
        /// <param name="limit">The maximum number of instances to retrieve.</param>
        /// <returns>A sequence of instances of <see cref="Event{T}"/>.</returns>
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
                yield return Create(Data, Title, occurrence.Start, occurrence.Stop);
            }
        }
    }

    /// <summary>
    /// Represents a record for specifying the time an event occurs.
    /// </summary>
    public sealed record Occurrence(DateTimeOffset Start, DateTimeOffset Stop);

}
