using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a meeting within a committee.
    /// </summary>
    public class Meeting : AuditableSoftDeleteEntity<MeetingId>
    {
        /// <summary>
        /// Gets or sets the unique ID of the meeting.
        /// </summary>
        public override MeetingId Id { get; protected set; } = MeetingId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Gets or sets the title of the meeting.
        /// </summary>
        public MeetingTitle Title { get; private set; } = null!;

        /// <summary>
        /// Gets or sets the optional description of the meeting.
        /// </summary>
        public MeetingDescription? Description { get; set; }

        /// <summary>
        /// Gets the agenda items for the meeting.
        /// </summary>
        public IReadOnlyCollection<Note> Agenda => _agenda.AsReadOnly();
        private readonly List<Note> _agenda = new List<Note>();

        /// <summary>
        /// Gets or sets the scheduled date and time of the meeting.
        /// </summary>
        public DateTimeOffset ScheduledDate { get; set; }

        /// <summary>
        /// Gets or sets the minutes of the meeting.
        /// </summary>
        public MeetingMinutes? Minutes { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the meeting has attached minutes.
        /// </summary>
        public bool HasAttachedMinutes => Minutes is not null;

        /// <summary>
        /// Gets or sets the committee hosting the meeting.
        /// </summary>
        public Committee Committee { get; private set; } = null!;

        /// <summary>
        /// Private constructor to enforce the use of factory methods.
        /// </summary>
        private Meeting() { }

        /// <summary>
        /// Creates a new meeting.
        /// </summary>
        /// <param name="title">The title of the meeting.</param>
        /// <param name="scheduledDate">The scheduled date and time of the meeting.</param>
        /// <param name="hostedBy">The committee hosting the meeting.</param>
        /// <returns>An instance of the <see cref="Meeting"/> class.</returns>
        public static Meeting Create(MeetingTitle title, DateTimeOffset scheduledDate, Committee hostedBy)
        {
            if (title is null) throw new DomainException(DomainErrors.Meeting.TitleNullOrEmpty);

            if (scheduledDate == default) throw new DomainException(DomainErrors.Meeting.DateNullOrEmpty);

            if (hostedBy is null) throw new DomainException(DomainErrors.Meeting.HostNullOrEmpty);

            return new Meeting() { Title = title, ScheduledDate = scheduledDate, Committee = hostedBy };
        }

        /// <summary>
        /// Adds a description to the meeting.
        /// </summary>
        /// <param name="description">The description of the meeting.</param>
        /// <returns>The updated meeting instance.</returns>
        public Meeting WithDescription(MeetingDescription description)
        {
            if (description is null) throw new DomainException(DomainErrors.Meeting.DescriptionNullOrEmpty);

            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            Description = description;

            return this;
        }

        /// <summary>
        /// Sets the agenda items for the meeting.
        /// </summary>
        /// <param name="agenda">The agenda items for the meeting.</param>
        /// <returns>The updated meeting instance.</returns>
        public Meeting WithAgenda(params Note[] agenda)
        {
            if (agenda is null || agenda.Length == 0) throw new DomainException(DomainErrors.Meeting.AgendaNullOrEmpty);

            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            _agenda.Clear();

            _agenda.AddRange(agenda);

            return this;
        }

        /// <summary>
        /// Adds an agenda item to the meeting.
        /// </summary>
        /// <param name="note">The agenda item to add.</param>
        /// <returns>The updated meeting instance.</returns>
        public Meeting AddAgendaItem(Note note)
        {
            if (note is null) throw new DomainException(DomainErrors.Meeting.NoteNullOrEmpty);

            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            _agenda.Add(note);

            return this;
        }

        /// <summary>
        /// Adds multiple agenda items to the meeting.
        /// </summary>
        /// <param name="notes">The agenda items to add.</param>
        /// <returns>The updated meeting instance.</returns>
        public Meeting AddAgendaItems(params Note[] notes)
        {
            if (notes is null || notes.Length == 0) throw new DomainException(DomainErrors.Meeting.NoteNullOrEmpty);

            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            _agenda.AddRange(notes);

            return this;
        }

        /// <summary>
        /// Removes an agenda item from the meeting.
        /// </summary>
        /// <param name="note">The agenda item to remove.</param>
        /// <returns>The updated meeting instance.</returns>
        public Meeting RemoveAgendaItem(Note note)
        {
            if (note is null) throw new DomainException(DomainErrors.Meeting.NoteNullOrEmpty);

            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            _agenda.Remove(note);

            return this;
        }

        /// <summary>
        /// Removes all agenda items from the meeting.
        /// </summary>
        /// <returns>The updated meeting instance.</returns>
        public Meeting RemoveAgenda()
        {
            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            _agenda.Clear();

            return this;
        }
        /// <summary>
        /// Generates minutes for the meeting.
        /// </summary>
        /// <returns>The generated minutes.</returns>
        public MeetingMinutes GenerateMinutes()
        {
            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            Minutes = MeetingMinutes.CreateFor(this);

            return Minutes;
        }

        /// <summary>
        /// Removes the minutes of the meeting.
        /// </summary>
        public void RemoveMinutes()
        {
            if (Minutes is null) throw new DomainException(DomainErrors.MeetingMinutes.NotFound);

            if (Minutes.IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            Minutes = null;
        }

        /// <summary>
        /// Edits the title of the meeting.
        /// </summary>
        /// <param name="title">The new title for the meeting.</param>
        public void EditTitle(MeetingTitle title)
        {
            ValidateTitle(title);

            if (title == Title) return;

            Title = title;
        }

        /// <summary>
        /// Edits the scheduled date of the meeting.
        /// </summary>
        /// <param name="date">The new scheduled date for the meeting.</param>
        public void EditScheduledDate(DateTimeOffset date)
        {
            ValidateScheduledDate(date);

            if (date == ScheduledDate) return;

            ScheduledDate = date;
        }

        /// <summary>
        /// Validates the title of the meeting.
        /// </summary>
        /// <param name="title">The title to validate.</param>
        private void ValidateTitle(MeetingTitle title)
        {
            if (title is null) throw new DomainException(DomainErrors.Meeting.TitleNullOrEmpty);

            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);
        }

        /// <summary>
        /// Validates the scheduled date of the meeting.
        /// </summary>
        /// <param name="date">The scheduled date to validate.</param>
        private void ValidateScheduledDate(DateTimeOffset date)
        {
            if (date == default) throw new DomainException(DomainErrors.Meeting.DateNullOrEmpty);

            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);
        }
    }
}