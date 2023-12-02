using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;

namespace HoaM.Domain.Features
{
    public sealed class MeetingService : IMeetingManager
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly TimeProvider _systemClock;

        public MeetingService(ICurrentUserService currentUserService, TimeProvider systemClock)
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

                meeting.Minutes.Publish(publisher, _systemClock.GetUtcNow());
            }
            catch (DomainException ex)
            {
                return Results.Exception(ex.Message, ex);
            }

            return Results.Success();
        }
    }
}
