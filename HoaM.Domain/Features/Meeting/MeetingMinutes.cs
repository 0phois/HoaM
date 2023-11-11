using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain.Features
{
    public sealed class MeetingMinutes : AuditableSoftDeleteEntity<MinutesId>
    {
        /// <summary>
        /// Unique ID of the <see cref="MeetingMinutes"/>
        /// </summary>
        public override MinutesId Id => MinutesId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Minutes (transcript) of the meeting 
        /// </summary>
        public IReadOnlyCollection<Note> Notes => _notes.AsReadOnly();
        private readonly List<Note> _notes = new();

        /// <summary>
        /// <see cref="AssociationMember"/>s that attended the <seealso cref="Entities.Meeting"/>
        /// </summary>
        public IReadOnlyCollection<AssociationMember> Attendees => _attendees.AsReadOnly();
        private readonly List<AssociationMember> _attendees = new();

        /// <summary>
        /// <see cref="CommitteeMember"/> that published the <see cref="MeetingMinutes"/>
        /// </summary>
        public CommitteeMember? Publisher { get; private set; }

        /// <summary>
        /// Date and time the <see cref="MeetingMinutes"/> were published
        /// </summary>
        public DateTimeOffset? PublishedDate { get; private set; }

        public bool IsPublished => PublishedDate is not null;

        /// <summary>
        /// <see cref="Entities.Meeting"/> for which the <seealso cref="MeetingMinutes"/> were recorded
        /// </summary>
        public Meeting Meeting { get; private set; } = null!;

        private MeetingMinutes() { }

        public static MeetingMinutes Create()
        {
            return new MeetingMinutes();
        }

        public MeetingMinutes AddAttendees(params AssociationMember[] attendees)
        {
            if (attendees is null || attendees.Length == 0) throw new DomainException(DomainErrors.MeetingMinutes.AttendeesNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _attendees.AddRange(attendees);

            return this;
        }

        public MeetingMinutes AddAttendee(AssociationMember attendee)
        {
            if (attendee is null) throw new DomainException(DomainErrors.MeetingMinutes.AttendeesNullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _attendees.Add(attendee);

            return this;
        }

        public MeetingMinutes AddNote(Note note)
        {
            if (note is null) throw new DomainException(DomainErrors.MeetingMinutes.NoteNullOrEmpty);
            
            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            _notes.Add(note);

            return this;
        }

        internal void Publish(CommitteeMember publisher, DateTimeOffset datePublished)
        {
            if (publisher is null) throw new DomainException(DomainErrors.AssociationMember.NullOrEmpty);

            if (IsPublished) throw new DomainException(DomainErrors.MeetingMinutes.AlreadyPublished);

            if (datePublished == default) throw new DomainException(DomainErrors.MeetingMinutes.DateNullOrEmpty);

            PublishedDate = datePublished;
            Publisher = publisher;
        }
    }
}
