using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;

namespace HoaM.Domain.Features
{
    public sealed class MeetingService : IMeetingManager
    {
        public ISystemClock SystemClock { get; }

        public MeetingService(ISystemClock systemClock) => SystemClock = systemClock;

        public IResult PublishMeetingMinutes(Meeting meeting)
        {
            if (meeting.Minutes is null) return Results.Failed(DomainErrors.MeetingMinutes.NullOrEmpty.Message);

            if (meeting.Minutes.IsPublished) return Results.Failed(DomainErrors.MeetingMinutes.AlreadyPublished.Message);

            try
            {
                meeting.Minutes.Publish(SystemClock.UtcNow);
            }
            catch (DomainException ex)
            {
                return Results.Exception(ex.Message, ex);
            }

            return Results.Success();
        }
    }
}
