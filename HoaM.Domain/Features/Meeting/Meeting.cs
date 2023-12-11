using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    public class Meeting : AuditableSoftDeleteEntity<MeetingId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Meeting"/>
        /// </summary>
        public override MeetingId Id { get; protected set; } = MeetingId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Title of the <see cref="Meeting"/>
        /// </summary>
        public MeetingTitle Title { get; private set; } = null!;

        /// <summary>
        /// Short description of the purpose of the <see cref="Meeting"/>
        /// </summary>
        public MeetingDescription? Description { get; set; }

        /// <summary>
        /// Planned agenda for the <see cref="Meeting"/>
        /// </summary>
        public IReadOnlyCollection<Note> Agenda => _agenda.AsReadOnly();
        private readonly List<Note> _agenda = [];

        /// <summary>
        /// Scheduled date and time of the <see cref="Meeting"/>
        /// </summary>
        public DateTimeOffset ScheduledDate { get; set; }

        /// <summary>
        /// <see cref="MeetingMinutes"/> recorded for this <seealso cref="Meeting"/>
        /// </summary>
        public MeetingMinutes? Minutes { get; private set; }

        public bool HasAttachedMinutes => Minutes is not null;

        /// <summary>
        /// <see cref="Entities.Committee"/> hosting this <seealso cref="Meeting"/>
        /// </summary>
        public Committee Committee { get; private set; } = null!;

        private Meeting() { }

        public static Meeting Create(MeetingTitle title, DateTimeOffset scheduledDate, Committee hostedBy)
        {
            if (title is null) throw new DomainException(DomainErrors.Meeting.TitleNullOrEmpty);

            if (scheduledDate == default) throw new DomainException(DomainErrors.Meeting.DateNullOrEmpty);

            if (hostedBy is null) throw new DomainException(DomainErrors.Meeting.HostNullOrEmpty);

            return new Meeting() { Title = title, ScheduledDate = scheduledDate, Committee = hostedBy };
        }

        public Meeting WithDescription(MeetingDescription description)
        {
            if (description is null) throw new DomainException(DomainErrors.Meeting.DescriptionNullOrEmpty);

            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            Description = description;

            return this;
        }

        public Meeting WithAgenda(params Note[] agenda)
        {
            if (agenda is null || agenda.Length == 0) throw new DomainException(DomainErrors.Meeting.AgendaNullOrEmpty);

            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            _agenda.Clear();

            _agenda.AddRange(agenda);

            return this;
        }

        public Meeting AddAgendaItem(Note note)
        {
            if (note is null) throw new DomainException(DomainErrors.Meeting.NoteNullOrEmpty);

            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            _agenda.Add(note);

            return this;
        }

        public Meeting AddAgendaItems(params Note[] notes)
        {
            if (notes is null || notes.Length == 0) throw new DomainException(DomainErrors.Meeting.NoteNullOrEmpty);

            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            _agenda.AddRange(notes);

            return this;
        }

        public Meeting RemoveAgendaItem(Note note)
        {
            if (note is null) throw new DomainException(DomainErrors.Meeting.NoteNullOrEmpty);

            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            _agenda.Remove(note);

            return this;
        }

        public Meeting RemoveAgenda()
        {
            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            _agenda.Clear();

            return this;
        }

        public MeetingMinutes GenerateMinutes()
        {
            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            Minutes = MeetingMinutes.CreateFor(this);

            return Minutes;
        }

        public void RemoveMinutes()
        {
            if (Minutes is null) throw new DomainException(DomainErrors.MeetingMinutes.NotFound);

            if (Minutes.IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            Minutes = null;
        }

        public void EditTitle(MeetingTitle title)
        {
            if (title is null) throw new DomainException(DomainErrors.Meeting.TitleNullOrEmpty);

            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            if (title == Title) return;

            Title = title;
        }

        public void EditScheduledDate(DateTimeOffset date)
        {
            if (date == default) throw new DomainException(DomainErrors.Meeting.DateNullOrEmpty);

            if (HasAttachedMinutes) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            if (date == ScheduledDate) return;

            ScheduledDate = date;
        }
    }
}