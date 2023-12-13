using HoaM.Domain.Exceptions;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a periodic meeting with a recurring schedule.
    /// </summary>
    public class PeriodicMeeting : RecurringMeeting<Meeting>
    {
        /// <summary>
        /// Private constructor to prevent direct instantiation.
        /// </summary>
        private PeriodicMeeting() { }

        /// <summary>
        /// Private constructor to create a periodic meeting with specified parameters.
        /// </summary>
        /// <param name="meeting">The meeting to be scheduled.</param>
        /// <param name="start">The start date and time of the meeting.</param>
        /// <param name="stop">The end date and time of the meeting.</param>
        /// <param name="frequency">The recurring schedule for the meeting (optional).</param>
        private PeriodicMeeting(Meeting meeting, DateTimeOffset start, DateTimeOffset stop, Schedule? frequency = null)
            : base(meeting, start, stop, frequency) { }

        /// <summary>
        /// Creates a periodic meeting with the specified meeting and recurring schedule.
        /// </summary>
        /// <param name="meeting">The meeting to be scheduled.</param>
        /// <param name="frequency">The recurring schedule for the meeting (optional).</param>
        /// <returns>The created periodic meeting.</returns>
        public static PeriodicMeeting Create(Meeting meeting, Schedule? frequency = null)
        {
            if (meeting is null) throw new DomainException(DomainErrors.Meeting.NullOrEmpty);

            return new PeriodicMeeting(meeting, meeting.ScheduledDate, meeting.ScheduledDate, frequency);
        }
    }

}
