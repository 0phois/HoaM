using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain.Features
{
    public class Meeting : AuditableSoftDeleteEntity<MeetingId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Meeting"/>
        /// </summary>
        public override MeetingId Id => MeetingId.From(NewId.Next().ToGuid());

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
        private readonly List<Note> _agenda = new();

        /// <summary>
        /// Scheduled date and time of the <see cref="Meeting"/>
        /// </summary>
        public DateTimeOffset ScheduledDate { get; set; }

        /// <summary>
        /// <see cref="MeetingMinutes"/> recorded for this <seealso cref="Meeting"/>
        /// </summary>
        public MeetingMinutes? Minutes { get; set; }

        public bool IsMinutesAttached => Minutes is not null;

        /// <summary>
        /// <see cref="Entities.Committee"/> hosting this <seealso cref="Meeting"/>
        /// </summary>
        public Committee Committee { get; private set; } = null!;

        private Meeting() { }

        public static Meeting Create(MeetingTitle title, DateTimeOffset scheduledDate, Committee host)
        {
            return new Meeting() { Title = title,  ScheduledDate = scheduledDate, Committee = host };
        }

        public Meeting WithDescription(MeetingDescription description)
        {
            if (IsMinutesAttached) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            Description = description;

            return this;
        }

        public Meeting WithAgenda(params Note[] agenda)
        {
            if (IsMinutesAttached) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            if (agenda is null || agenda.Length == 0) throw new DomainException(DomainErrors.Meeting.MissingAgenda);

            _agenda.Clear();

            _agenda.AddRange(agenda);

            return this;
        }

        public Meeting AddAgendaItem(Note note)
        {
            if (IsMinutesAttached) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            _agenda.Add(note);

            return this;
        }

        public Meeting AddAgendaItems(params Note[] notes)
        {
            if (IsMinutesAttached) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            if (notes is null || notes.Length == 0) throw new DomainException(DomainErrors.Meeting.MissingNote);

            _agenda.AddRange(notes);

            return this;
        }

        public Meeting RemoveAgendaItem(Note note)
        {
            if (IsMinutesAttached) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            _agenda.Remove(note);

            return this;
        }

        public Meeting RemoveAgenda() 
        {
            if (IsMinutesAttached) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            _agenda.Clear();

            return this;
        }

        public void AddMinutes(MeetingMinutes minutes)
        {
            if (IsMinutesAttached) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            Minutes = minutes;
        }

        public void EditTitle(MeetingTitle title)
        {
            if (IsMinutesAttached) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            if (title == Title) return;

            Title = title;
        }

        public void EditScheduledDate(DateTimeOffset date)
        {
            if (IsMinutesAttached) throw new DomainException(DomainErrors.Meeting.MinutesAlreadyAttached);

            if (date == ScheduledDate) return;

            ScheduledDate = date;
        }
    }
}

//TODO - Meeting manage 
/*
 - Edit meeting minutes 
 */