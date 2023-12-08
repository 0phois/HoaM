using NSubstitute.ExceptionExtensions;

namespace HoaM.Domain.UnitTests
{
    public class MeetingServiceTests
    {
        [Fact]
        public async Task PublishMeetingMinutesAsync_ValidMeetingAndUser_Success()
        {
            // Arrange
            var currentUserService = Substitute.For<ICurrentUserService>();
            var systemClock = Substitute.For<TimeProvider>();
            var meetingService = new MeetingService(currentUserService, systemClock);

            var meeting = CreateMeeting();
            meeting.GenerateMinutes();

            systemClock.GetUtcNow().Returns(DateTime.UtcNow);
            currentUserService.CurrentUser.Returns(AssociationMember.Create(FirstName.From("Jane"), LastName.From("Doe")));

            // Act
            var result = await meetingService.PublishMeetingMinutesAsync(meeting);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(meeting.Minutes!.IsPublished);
        }

        [Fact]
        public async Task PublishMeetingMinutesAsync_NullMeetingMinutes_Fails()
        {
            // Arrange
            var currentUserService = Substitute.For<ICurrentUserService>();
            var systemClock = Substitute.For<TimeProvider>();
            var meetingService = new MeetingService(currentUserService, systemClock);

            var meeting = CreateMeeting();

            // Act
            var result = await meetingService.PublishMeetingMinutesAsync(meeting);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(DomainErrors.MeetingMinutes.NullOrEmpty.Message, result.Message);
        }

        [Fact]
        public async Task PublishMeetingMinutesAsync_AlreadyPublished_Fails()
        {
            // Arrange
            var currentUserService = Substitute.For<ICurrentUserService>();
            var systemClock = Substitute.For<TimeProvider>();
            var meetingService = new MeetingService(currentUserService, systemClock);

            var meeting = CreateMeeting();
            var minutes = MeetingMinutes.CreateFor(meeting);

            meeting.GenerateMinutes();

            systemClock.GetUtcNow().Returns(DateTime.UtcNow);
            currentUserService.CurrentUser.Returns(AssociationMember.Create(FirstName.From("Jane"), LastName.From("Doe")));

            await meetingService.PublishMeetingMinutesAsync(meeting);
            currentUserService.ClearReceivedCalls();

            // Act
            var result = await meetingService.PublishMeetingMinutesAsync(meeting);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(DomainErrors.MeetingMinutes.AlreadyPublished.Message, result.Message);
        }

        private static Meeting CreateMeeting()
        {
            var title = new MeetingTitle("Meeting Title");
            var scheduledDate = DateTimeOffset.UtcNow;
            var committee = Committee.Create(CommitteeName.From("Executive Committee"));

            return Meeting.Create(title, scheduledDate, committee);
        }
    }
}
