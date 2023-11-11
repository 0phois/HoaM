using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public interface IMeetingManager
    {
        ValueTask<IResult> PublishMeetingMinutesAsync(Meeting meeting);
    }
}