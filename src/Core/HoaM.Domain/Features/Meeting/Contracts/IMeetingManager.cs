using HoaM.Domain.Common;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents an interface for managing meetings.
    /// </summary>
    public interface IMeetingManager
    {
        /// <summary>
        /// Asynchronously publishes the meeting minutes for the specified meeting.
        /// </summary>
        /// <param name="meeting">The meeting for which the minutes should be published.</param>
        /// <returns>A <see cref="ValueTask{TResult}"/> representing the result of the operation.</returns>
        ValueTask<IResult> PublishMeetingMinutesAsync(Meeting meeting);
    }

}