using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public interface IMeetingManager
    {
        ISystemClock SystemClock { get; }

        IResult PublishMeetingMinutes(MeetingMinutes meetingMinutes);
    }
}