namespace HoaM.Domain.UnitTests
{
    public class CommunityTests
    {
        [Fact]
        public void Create_CommunityWithValidName_ShouldSucceed()
        {
            // Arrange
            var communityName = CommunityName.From("My Community");

            // Act
            var community = Community.Create(communityName);

            // Assert
            Assert.NotNull(community);
            Assert.NotEmpty(community.Id.ToString());
            Assert.Equal(communityName, community.Name);
        }

        [Fact]
        public void Create_CommunityWithNullName_ShouldThrowException()
        {
            // Arrange
            CommunityName communityName = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => Community.Create(communityName));
        }

        [Fact]
        public void EditName_NewNameIsValid_ShouldChangeCommunityName()
        {
            // Arrange
            var community = Community.Create(new CommunityName("Old Community"));
            var newName = new CommunityName("New Community");

            // Act
            community.EditName(newName);

            // Assert
            Assert.Equal(newName, community.Name);
        }

        [Fact]
        public void EditName_NewNameIsNull_ShouldThrowException()
        {
            // Arrange
            var community = Community.Create(new CommunityName("My Community"));
            CommunityName newName = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => community.EditName(newName));
        }
    }
}
