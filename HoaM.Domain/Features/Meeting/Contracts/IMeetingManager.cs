using HoaM.Domain.Common;

namespace HoaM.Domain
{
    public interface IMeetingManager
    {
        ValueTask<IResult> PublishMeetingMinutesAsync(Meeting meeting);
    }
}