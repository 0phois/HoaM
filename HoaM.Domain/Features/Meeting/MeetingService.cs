using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;

namespace HoaM.Domain
{
    public sealed class MeetingService(ICurrentUserService currentUserService, TimeProvider systemClock) : IMeetingManager
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly TimeProvider _systemClock = systemClock;

        public ValueTask<IResult> PublishMeetingMinutesAsync(Meeting meeting)
        {
            if (meeting.Minutes is null) return Results.Failed(DomainErrors.MeetingMinutes.NullOrEmpty.Message);

            if (meeting.Minutes.IsPublished) return Results.Failed(DomainErrors.MeetingMinutes.AlreadyPublished.Message);

            try
            {
                var publisher = _currentUserService.CurrentUser;

                meeting.Minutes.Publish(publisher.Id, _systemClock.GetUtcNow());
            }
            catch (DomainException ex)
            {
                return Results.Exception(ex.Message, ex);
            }

            return Results.Success();
        }
    }
}
