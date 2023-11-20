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
#pragma warning disable CS8600 
            CommunityName communityName = null;
#pragma warning restore CS8600 

            // Act & Assert
#pragma warning disable CS8604 
            Assert.Throws<DomainException>(() => Community.Create(communityName));
#pragma warning restore CS8604 
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
#pragma warning disable CS8600
            CommunityName newName = null;
#pragma warning restore CS8600

            // Act & Assert
#pragma warning disable CS8604
            Assert.Throws<DomainException>(() => community.EditName(newName));
#pragma warning restore CS8604
        }
    }
}
