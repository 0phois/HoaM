namespace HoaM.Domain.UnitTests
{
    public class MeetingTests
    {
        [Fact]
        public void CreateMeeting_WithValidParameters_ReturnsMeetingInstance()
        {
            // Arrange
            var title = new MeetingTitle("Meeting Title");
            var scheduledDate = DateTimeOffset.UtcNow.AddDays(1);
            var committee = Committee.Create(CommitteeName.From("Executive Committee"));

            // Act
            var meeting = Meeting.Create(title, scheduledDate, committee);

            // Assert
            Assert.NotNull(meeting);
            Assert.Equal(title, meeting.Title);
            Assert.Equal(scheduledDate, meeting.ScheduledDate);
            Assert.Equal(committee, meeting.Committee);
        }

        [Fact]
        public void CreateMeeting_WithNullTitle_ThrowsDomainException()
        {
            // Arrange
            MeetingTitle title = null;
            var scheduledDate = DateTimeOffset.UtcNow;
            var committee = Committee.Create(CommitteeName.From("Executive Committee"));

            // Act & Assert
            Assert.Throws<DomainException>(() => Meeting.Create(title, scheduledDate, committee));
        }

        [Fact]
        public void WithDescription_ValidDescription_SetsDescription()
        {
            // Arrange
            var meeting = CreateMeeting();
            var description = new MeetingDescription("Meeting Description");

            // Act
            var result = meeting.WithDescription(description);

            // Assert
            Assert.Same(meeting, result);
            Assert.Equal(description, meeting.Description);
        }

        [Fact]
        public void WithAgenda_ValidAgenda_SetsAgenda()
        {
            // Arrange
            var meeting = CreateMeeting();
            var agenda = new Note[] { Note.Create(Text.From("Agenda Item 1")), Note.Create(Text.From("Agenda Item 2")) };

            // Act
            var result = meeting.WithAgenda(agenda);

            // Assert
            Assert.Same(meeting, result);
            Assert.Equal(agenda, meeting.Agenda);
        }

        [Fact]
        public void WithAgenda_NullAgenda_ThrowsDomainException()
        {
            // Arrange
            var meeting = CreateMeeting();

            // Act & Assert
            Assert.Throws<DomainException>(() => meeting.WithAgenda(null));
        }

        // For AddAgendaItem method
        [Fact]
        public void AddAgendaItem_ValidNote_AddsNoteToAgenda()
        {
            // Arrange
            var meeting = CreateMeeting();
            var note = Note.Create(Text.From("New Agenda Item"));

            // Act
            var result = meeting.AddAgendaItem(note);

            // Assert
            Assert.Same(meeting, result);
            Assert.Contains(note, meeting.Agenda);
        }

        [Fact]
        public void AddAgendaItem_NullNote_ThrowsDomainException()
        {
            // Arrange
            var meeting = CreateMeeting();

            // Act & Assert
            Assert.Throws<DomainException>(() => meeting.AddAgendaItem(null));
        }

        // For RemoveAgendaItem method
        [Fact]
        public void RemoveAgendaItem_ExistingNote_RemovesNoteFromAgenda()
        {
            // Arrange
            var meeting = CreateMeeting();
            var note = Note.Create(Text.From("Existing Agenda Item"));
            meeting.AddAgendaItem(note);

            // Act
            var result = meeting.RemoveAgendaItem(note);

            // Assert
            Assert.Same(meeting, result);
            Assert.DoesNotContain(note, meeting.Agenda);
        }

        [Fact]
        public void RemoveAgenda_ClearsAgenda()
        {
            // Arrange
            var meeting = CreateMeeting();
            meeting.AddAgendaItem(Note.Create(Text.From("Agenda Item")));

            // Act
            var result = meeting.RemoveAgenda();

            // Assert
            Assert.Same(meeting, result);
            Assert.Empty(meeting.Agenda);
        }

        [Fact]
        public void GenerateMinutes_NoMinutesAttached_CreatesMinutes()
        {
            // Arrange
            var meeting = CreateMeeting();

            // Act
            var minutes = meeting.GenerateMinutes();

            // Assert
            Assert.NotNull(minutes);
            Assert.Equal(meeting, minutes.Meeting);
        }

        [Fact]
        public void GenerateMinutes_MinutesAlreadyAttached_ThrowsDomainException()
        {
            // Arrange
            var meeting = CreateMeeting();
            meeting.GenerateMinutes();

            // Act & Assert
            Assert.Throws<DomainException>(() => meeting.GenerateMinutes());
        }

        [Fact]
        public void GenerateMinutes_AssociatesMinutesWithMeeting()
        {
            // Arrange
            var meeting = CreateMeeting();

            // Act
            var minutes = meeting.GenerateMinutes();

            // Assert
            Assert.NotNull(minutes);
            Assert.Equal(meeting, minutes.Meeting);
        }

        [Fact]
        public void RemoveMinutes_MinutesAttached_NotPublished_RemovesMinutes()
        {
            // Arrange
            var meeting = CreateMeeting();
            meeting.GenerateMinutes();

            // Act
            meeting.RemoveMinutes();

            // Assert
            Assert.Null(meeting.Minutes);
        }

        [Fact]
        public void RemoveMinutes_AlreadyPublished_ThrowsDomainException()
        {
            // Arrange
            var meeting = CreateMeeting();
            var member = AssociationMember.Create(FirstName.From("Alexander"), LastName.From("Pope"));
            
            meeting.GenerateMinutes().Publish(member.Id, DateTimeOffset.UtcNow);

            // Act & Assert
            Assert.Throws<DomainException>(() => meeting.RemoveMinutes());
        }

        [Fact]
        public void RemoveMinutes_NoMinutesAttached_ThrowsDomainException()
        {
            // Arrange
            var meeting = CreateMeeting();

            // Act & Assert
            Assert.Throws<DomainException>(() => meeting.RemoveMinutes());
        }

        [Fact]
        public void EditTitle_ValidTitle_EditsTitle()
        {
            // Arrange
            var meeting = CreateMeeting();
            var newTitle = new MeetingTitle("New Meeting Title");

            // Act
            meeting.EditTitle(newTitle);

            // Assert
            Assert.Equal(newTitle, meeting.Title);
        }

        [Fact]
        public void EditTitle_MinutesAlreadyAttached_ThrowsDomainException()
        {
            // Arrange
            var meeting = CreateMeeting();
            meeting.GenerateMinutes();

            // Act & Assert
            Assert.Throws<DomainException>(() => meeting.EditTitle(new MeetingTitle("New Title")));
        }

        [Fact]
        public void EditScheduledDate_ValidDate_IsSuccessful()
        {
            // Arrange
            var meeting = CreateMeeting();
            var newDate = DateTimeOffset.UtcNow.AddHours(1);

            // Act
            meeting.EditScheduledDate(newDate);

            // Assert
            Assert.Equal(newDate, meeting.ScheduledDate);
        }

        [Fact]
        public void EditScheduledDate_MinutesAlreadyAttached_ThrowsDomainException()
        {
            // Arrange
            var meeting = CreateMeeting();
            meeting.GenerateMinutes();

            // Act & Assert
            Assert.Throws<DomainException>(() => meeting.EditScheduledDate(DateTimeOffset.UtcNow.AddHours(1)));
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
