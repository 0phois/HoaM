using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Entities
{
    public sealed class MeetingMinutes : Entity<MinutesId>
    {
        /// <summary>
        /// Unique ID of the <see cref="MeetingMinutes"/>
        /// </summary>
        public override MinutesId Id => MinutesId.From(NewId.Next().ToGuid());

        /// <summary>
        /// <see cref="CommitteeMember"/> that created the <seealso cref="MeetingMinutes"/>
        /// </summary>
        public required CommitteeMember Author { get; init; }

        /// <summary>
        /// Date and time the <see cref="MeetingMinutes"/> were created
        /// </summary>
        public DateTimeOffset CreatedDate { get; private set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Minutes (transcript) of the meeting 
        /// </summary>
        public List<Note> Notes { get; set; } = new List<Note>();

        /// <summary>
        /// <see cref="AssociationMember"/>s that attended the <seealso cref="Entities.Meeting"/>
        /// </summary>
        public List<AssociationMember> Attendees { get; set; } = new List<AssociationMember>();

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
    }
}
