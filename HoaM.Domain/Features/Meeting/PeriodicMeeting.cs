using HoaM.Domain.Exceptions;

namespace HoaM.Domain.Features
{
    public class PeriodicMeeting : RecurringMeeting<Meeting>
    {
        private PeriodicMeeting() { }

        private PeriodicMeeting(Meeting activity, EventTitle title, DateTimeOffset start, DateTimeOffset stop, Schedule? frequency = null)
            : base(activity, title, start, stop, frequency) { }

        public static PeriodicMeeting Create(Meeting meeting, Schedule? frequeny = null)
        {
            if (meeting is null) throw new DomainException(DomainErrors.Meeting.NullOrEmpty);

            return new PeriodicMeeting(meeting, EventTitle.From(meeting.Title.Value), meeting.ScheduledDate, meeting.ScheduledDate, frequeny);
        }
    }
}
