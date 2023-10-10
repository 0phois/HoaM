using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Entities
{
    public sealed class Meeting : Entity<MeetingId>, ISoftDelete
    {
        /// <summary>
        /// Unique ID of the <see cref="Meeting"/>
        /// </summary>
        public override MeetingId Id => MeetingId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Title of the <see cref="Meeting"/>
        /// </summary>
        public required MeetingTitle Title { get; set; }

        /// <summary>
        /// Short description of the purpose of the <see cref="Meeting"/>
        /// </summary>
        public MeetingDescription? Description { get; set; }

        /// <summary>
        /// Planned agenda for the <see cref="Meeting"/>
        /// </summary>
        public List<Note> Agenda { get; set; } = new List<Note>();

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

        public AssociationMemberId? DeletedBy { get; set; }
        public DateTimeOffset? DeletionDate { get; set; }
    }
}