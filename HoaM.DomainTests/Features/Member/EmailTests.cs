namespace HoaM.Domain.UnitTests
{
    public class EmailTests
    {
        [Fact]
        public void Create_ValidEmail_ReturnsEmailInstance()
        {
            // Arrange
            var emailAddress = new EmailAddress("test@example.com");

            // Act
            var email = Email.Create(emailAddress);

            // Assert
            Assert.NotNull(email);
            Assert.Equal(emailAddress, email.Address);
            Assert.False(email.IsVerified); // By default, IsVerified should be false
        }

        [Fact]
        public void Create_NullEmailAddress_ThrowsDomainException()
        {
            // Arrange
            EmailAddress emailAddress = null;

            // Act
            var exception = Record.Exception(() => Email.Create(emailAddress));

            // Assert
            Assert.IsType<DomainException>(exception);
        }

        [Fact]
        public void Verify_EmailNotVerified_SetIsVerifiedToTrue()
        {
            // Arrange
            var emailAddress = new EmailAddress("test@example.com");
            var email = Email.Create(emailAddress);

            // Act
            email.Verify();

            // Assert
            Assert.True(email.IsVerified);
        }

        [Fact]
        public void Verify_EmailAlreadyVerified_NoChangeInVerificationStatus()
        {
            // Arrange
            var emailAddress = new EmailAddress("test@example.com");
            var email = Email.Create(emailAddress);

            // Act
            email.Verify(); // First verification
            email.Verify(); // Second verification

            // Assert
            Assert.True(email.IsVerified); // The verification status should remain true
        }
    }
}
