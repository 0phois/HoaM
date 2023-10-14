using HoaM.Domain.Common;
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

        /// <summary>
        /// <see cref="Entities.Committee"/> hosting this <seealso cref="Meeting"/>
        /// </summary>
        public Committee Committee { get; private set; } = null!;

        private Meeting() { }

        public static Meeting Create(MeetingTitle title, DateTimeOffset scheduledDate, Committee host)
        {
            return new Meeting() { Title = title,  ScheduledDate = scheduledDate, Committee = host };
        }

        public Meeting AddDescription(MeetingDescription description) 
        {
            if (Description is not null) throw new InvalidOperationException("A description has already been added");

            Description = description;

            return this;
        }

        public Meeting WithAgenda(params Note[] agenda)
        {
            if (agenda is null || agenda.Length == 0) throw new ArgumentNullException(nameof(agenda), "Value cannot be null or empty.");

            if (_agenda.Count > 0) throw new InvalidOperationException("Meeting agenda has already been defined");

            _agenda.AddRange(agenda);

            return this;
        }

        public Meeting AddAgenda(Note agenda)
        {
            _agenda.Add(agenda);

            return this;
        }

        public Meeting RemoveAgendaItem(Note agenda)
        {
            _agenda.Remove(agenda);

            return this;
        }

        public Meeting RemoveAgenda() 
        {
            _agenda.Clear();

            return this;
        }

        public void AddMinutes(MeetingMinutes minutes)
        {
            if (Minutes is not null) throw new InvalidOperationException("Meeting minutes have already been set");

            Minutes = minutes;
        }

        public void EditTitle(MeetingTitle title)
        {
            if (title == Title) return;

            Title = title;
        }

        public void EditDescription(MeetingDescription description)
        {
            if (description == Description) return;

            Description = description;
        }

        public void EditScheduledDate(DateTimeOffset date)
        {
            if (date == ScheduledDate) return;

            ScheduledDate = date;
        }

        public void EditAgenda(params Note[] agenda)
        {
            if (agenda is null || agenda.Length == 0) throw new ArgumentNullException(nameof(agenda), "Value cannot be null or empty.");

            _agenda.Clear();

            _agenda.AddRange(agenda);
        }
    }
}

//TODO - Meeting manage 
/*
 - Edit meeting minutes 
 */