using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;

namespace HoaM.Domain
{
    /// <summary>
    /// Service responsible for managing meeting-related operations.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="MeetingService"/> class.
    /// </remarks>
    /// <param name="currentUserService">The service providing information about the current user.</param>
    /// <param name="systemClock">The provider for the current system time.</param>
    public sealed class MeetingService(ICurrentUserService currentUserService, TimeProvider systemClock) : IMeetingManager
    {
        private readonly ICurrentUserService _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        private readonly TimeProvider _systemClock = systemClock ?? throw new ArgumentNullException(nameof(systemClock));

        /// <summary>
        /// Publishes the meeting minutes asynchronously.
        /// </summary>
        /// <param name="meeting">The meeting for which to publish the minutes.</param>
        /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation result.</returns>
        public ValueTask<IResult> PublishMeetingMinutesAsync(Meeting meeting)
        {
            if (meeting.Minutes is null)
                return Results.Failed(DomainErrors.MeetingMinutes.NullOrEmpty.Message);

            if (meeting.Minutes.IsPublished)
                return Results.Failed(DomainErrors.MeetingMinutes.AlreadyPublished.Message);

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
