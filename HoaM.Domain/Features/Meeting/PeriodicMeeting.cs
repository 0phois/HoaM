using HoaM.Domain.Exceptions;

namespace HoaM.Domain.Features
{
    public class PeriodicMeeting : RecurringMeeting<Meeting>
    {
        private PeriodicMeeting() { }

        private PeriodicMeeting(Meeting activity, DateTimeOffset start, DateTimeOffset stop, Schedule? frequency = null)
            : base(activity, start, stop, frequency) { }

        public static PeriodicMeeting Create(Meeting meeting, Schedule? frequeny = null)
        {
            if (meeting is null) throw new DomainException(DomainErrors.Meeting.NullOrEmpty);

            return new PeriodicMeeting(meeting, meeting.ScheduledDate, meeting.ScheduledDate, frequeny);
        }
    }
}
