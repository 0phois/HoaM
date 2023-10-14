using HoaM.Domain.Common;
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
            if (PublishedDate != null) throw new InvalidOperationException("Meeting minutes have already been published!");

            _attendees.AddRange(attendees);

            return this;
        } 

        public MeetingMinutes AddAttendee(AssociationMember attendee)
        {
            if (PublishedDate != null) throw new InvalidOperationException("Meeting minutes have already been published!");

            _attendees.Add(attendee);

            return this;
        }

        public MeetingMinutes AddNote(Note note)
        {
            if (PublishedDate != null) throw new InvalidOperationException("Meeting minutes have already been published!");

            _notes.Add(note);

            return this;
        }

        public IResult Publish(IMeetingManager meetingManager)
        {
            if (PublishedDate != null) throw new InvalidOperationException("Meeting minutes have already been published!");

            var publishResult = meetingManager.PublishMeetingMinutes(this);

            if (publishResult.IsSuccess) PublishedDate = meetingManager.SystemClock.UtcNow;

            return publishResult;
        }
    }
}
