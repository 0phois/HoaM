﻿using System;

namespace HoaM.Domain.UnitTests
{
    public class MeetingMinutesTests
    {
        [Fact]
        public void Create_ShouldCreateInstance()
        {
            // Arrange
            var meeting = CreateMeeting();

            // Act
            var meetingMinutes = MeetingMinutes.CreateFor(meeting);

            // Assert
            Assert.NotNull(meetingMinutes);
        }

        [Fact]
        public void WithAttendees_ShouldSetAttendees()
        {
            // Arrange
            var meeting = CreateMeeting();
            var meetingMinutes = MeetingMinutes.CreateFor(meeting);
            var jack = AssociationMember.Create(FirstName.From("Jack"), LastName.From("Samurai"));
            var jane = AssociationMember.Create(FirstName.From("Jane"), LastName.From("Smith"));
            var george = AssociationMember.Create(FirstName.From("George"), LastName.From("Jetson"));

            var attendees = new AssociationMember[] { jack, jane, george };

            // Act
            meetingMinutes.WithAttendees(attendees);

            // Assert
            Assert.Equal(meetingMinutes.Attendees, attendees);
        }

        [Fact]
        public void WithAttendees_WithNullAttendees_ShouldThrowDomainException()
        {
            // Arrange
            var meeting = CreateMeeting();
            var meetingMinutes = MeetingMinutes.CreateFor(meeting);

            // Act
            Action action = () => meetingMinutes.WithAttendees(null);

            // Assert
            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal(exception.Message, DomainErrors.MeetingMinutes.AttendeesNullOrEmpty.Message);

        }

        // Add similar tests for other methods...

        [Fact]
        public void Publish_ShouldSetPublishedDateAndPublisher()
        {
            // Arrange
            var meeting = CreateMeeting();
            var meetingMinutes = MeetingMinutes.CreateFor(meeting);
            var publisher = CommitteeMember.Create(FirstName.From("Bob"), LastName.From("Barker"));
            var datePublished = DateTimeOffset.Now;

            // Act
            meetingMinutes.Publish(publisher, datePublished);

            // Assert
            Assert.Equal(datePublished, meetingMinutes.PublishedDate);
            Assert.Equal(publisher, meetingMinutes.Publisher);
        }

        [Fact]
        public void Publish_AlreadyPublished_ShouldThrowDomainException()
        {
            // Arrange
            var meeting = CreateMeeting();
            var meetingMinutes = MeetingMinutes.CreateFor(meeting);
            var publisher = CommitteeMember.Create(FirstName.From("Peter"), LastName.From("Parker"));

            meetingMinutes.Publish(publisher, DateTimeOffset.Now);

            // Act
            Action action = () => meetingMinutes.Publish(publisher, DateTimeOffset.Now);

            // Assert
            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal(exception.Message, DomainErrors.MeetingMinutes.AlreadyPublished.Message);
        }

        [Fact]
        public void Publish_WithNullPublisher_ShouldThrowDomainException()
        {
            // Arrange
            var meeting = CreateMeeting();
            var meetingMinutes = MeetingMinutes.CreateFor(meeting);

            // Act
            Action action = () => meetingMinutes.Publish(null, DateTimeOffset.Now);

            // Assert
            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal(exception.Message, DomainErrors.AssociationMember.NullOrEmpty.Message);
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