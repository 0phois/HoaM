namespace HoaM.Domain.UnitTests
{
    public class CommitteeTests
    {
        [Fact]
        public void Create_CommitteeWithValidName_ReturnsCommitteeInstance()
        {
            // Arrange
            var name = new CommitteeName("Test Committee");

            // Act
            var committee = Committee.Create(name);

            // Assert
            Assert.NotNull(committee);
            Assert.Equal(name, committee.Name);
            Assert.Null(committee.EstablishedDate);
            Assert.False(committee.IsDeleted);
            Assert.False(committee.IsDissolved);
            Assert.True(committee.IsActive);
        }

        [Fact]
        public void Create_CommitteeWithNullName_ThrowsDomainException()
        {
            // Arrange
#pragma warning disable CS8600
            CommitteeName name = null;
#pragma warning restore CS8600

            // Act & Assert
#pragma warning disable CS8604
            Assert.Throws<DomainException>(() => Committee.Create(name));
#pragma warning restore CS8604
        }

        [Fact]
        public void WithEstablishedDate_ValidEstablishedDate_SetsEstablishedDate()
        {
            // Arrange
            var committee = Committee.Create(new CommitteeName("Test Committee"));
            var established = DateOnly.FromDateTime(DateTime.UtcNow);

            // Act
            committee.WithEstablishedDate(established);

            // Assert
            Assert.Equal(established, committee.EstablishedDate);
        }

        [Fact]
        public void WithMissionStatement_ValidMissionStatement_SetsMissionStatement()
        {
            // Arrange
            var committee = Committee.Create(new CommitteeName("Test Committee"));
            var statement = new MissionStatement("Mission statement");

            // Act
            committee.WithMissionStatement(statement);

            // Assert
            Assert.Equal(statement, committee.MissionStatement);
        }

        [Fact]
        public void WithMissionStatement_NullMissionStatement_ThrowsDomainException()
        {
            // Arrange
            var committee = Committee.Create(new CommitteeName("Test Committee"));

            // Act & Assert
            Assert.Throws<DomainException>(() => committee.WithMissionStatement(null));
        }

        [Fact]
        public void EditName_ValidName_ChangesCommitteeName()
        {
            // Arrange
            var committee = Committee.Create(new CommitteeName("Old Name"));
            var newName = new CommitteeName("New Name");

            // Act
            committee.EditName(newName);

            // Assert
            Assert.Equal(newName, committee.Name);
        }

        [Fact]
        public void EditName_NullName_ThrowsDomainException()
        {
            // Arrange
            var committee = Committee.Create(new CommitteeName("Test Committee"));
            CommitteeName newName = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => committee.EditName(newName));
        }

        [Fact]
        public void WithAdditionalDetails_AddDetails_SetsAdditionalDetails()
        {
            // Arrange
            var committee = Committee.Create(new CommitteeName("Test Committee"));
            var details = Note.Create(Text.From("Additional detail"));

            // Act
            committee.WithAdditionalDetails(details);

            // Assert
            Assert.Single(committee.AdditionalDetails);
            Assert.Contains(details, committee.AdditionalDetails);
        }

        [Fact]
        public void WithAdditionalDetails_AddDetailsToExistingDetails_AppendsAdditionalDetails()
        {
            // Arrange
            var committee = Committee.Create(new CommitteeName("Test Committee"));
            var initialDetails = Note.Create(Text.From("Initial detail"));

            committee.WithAdditionalDetails(initialDetails);
            
            var newDetails = Note.Create(Text.From("Additional detail"));

            // Act
            committee.AppendAdditionalDetails(newDetails);

            // Assert
            Assert.Equal(2, committee.AdditionalDetails.Count);
            Assert.Contains(initialDetails, committee.AdditionalDetails);
            Assert.Contains(newDetails, committee.AdditionalDetails);
        }

        [Fact]
        public void WithAdditionalDetails_NullDetails_ThrowsDomainException()
        {
            // Arrange
            var committee = Committee.Create(new CommitteeName("Test Committee"));

            // Act & Assert
            Assert.Throws<DomainException>(() => committee.WithAdditionalDetails(null));
        }

        [Fact]
        public void RemoveDetails_DetailsExist_RemovesDetails()
        {
            // Arrange
            var committee = Committee.Create(new CommitteeName("Test Committee"));
            var details = Note.Create(Text.From("Additional detail"));
            committee.WithAdditionalDetails(details);

            // Act
            committee.RemoveDetails();

            // Assert
            Assert.Empty(committee.AdditionalDetails);
        }

        [Fact]
        public void RemoveDetails_NoDetails_LeavesCollectionUnchanged()
        {
            // Arrange
            var committee = Committee.Create(new CommitteeName("Test Committee"));

            // Act
            committee.RemoveDetails();

            // Assert
            Assert.Empty(committee.AdditionalDetails);
        }

        [Fact]
        public void TryDissolve_AlreadyDissolved_ReturnsFalse()
        {
            // Arrange
            var committee = Committee.Create(new CommitteeName("Test Committee"));
            committee.TryDissolve(new SystemClock());

            // Act
            var result = committee.TryDissolve(new SystemClock());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void TryDissolve_AlreadyDeleted_ReturnsFalse()
        {
            // Arrange
            var committee = Committee.Create(new CommitteeName("Test Committee"));
            committee.DeletedBy = new AssociationMemberId(Guid.NewGuid());
            committee.DeletionDate = DateTimeOffset.Now;

            // Act
            var result = committee.TryDissolve(new SystemClock());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void TryDissolve_SuccessfulDissolution_SetsDissolvedDateAndRaisesEvent()
        {
            // Arrange
            var committee = Committee.Create(new CommitteeName("Test Committee"));
            var systemClock = new SystemClock();

            // Act
            var result = committee.TryDissolve(systemClock);

            // Assert
            Assert.True(result);
            Assert.NotNull(committee.DissolvedDate);
            Assert.Single(committee.DomainEvents);
            Assert.IsType<CommitteeDissolvedNotification>(committee.DomainEvents.Single());
        }

    }
}
