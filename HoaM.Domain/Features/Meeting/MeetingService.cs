using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;

namespace HoaM.Domain.Features
{
    public sealed class MeetingService : IMeetingManager
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ISystemClock _systemClock;

        public MeetingService(ICurrentUserService currentUserService, ISystemClock systemClock)
        {
            _currentUserService = currentUserService;
            _systemClock = systemClock;
        }

        public async ValueTask<IResult> PublishMeetingMinutesAsync(Meeting meeting)
        {
            if (meeting.Minutes is null) return Results.Failed(DomainErrors.MeetingMinutes.NullOrEmpty.Message);

            if (meeting.Minutes.IsPublished) return Results.Failed(DomainErrors.MeetingMinutes.AlreadyPublished.Message);

            try
            {
                var publisher = await _currentUserService.GetCommitteeMember();

                meeting.Minutes.Publish(publisher, _systemClock.UtcNow);
            }
            catch (DomainException ex)
            {
                return Results.Exception(ex.Message, ex);
            }

            return Results.Success();
        }
    }
}
