namespace HoaM.Domain.UnitTests
{
    public class NotificationTests
    {
        [Fact]
        public void Create_ValidNotification_ReturnsNotificationInstance()
        {
            // Arrange
            var template = CreateTemplate();
            var recipient = CreateMember();

            // Act
            var notification = Notification.Create(template, recipient);

            // Assert
            Assert.NotNull(notification);
            Assert.Equal(template, notification.Template);
            Assert.Equal(recipient, notification.Recipient);
            Assert.Null(notification.ReceivedDate);
            Assert.Equal(DateTimeOffset.MinValue, notification.ReadDate);
            Assert.False(notification.IsRead);
        }

        [Fact]
        public void Create_NullTemplate_ThrowsDomainException()
        {
            // Arrange
            NotificationTemplate template = null;
            var recipient = CreateMember();

            // Act & Assert
            Assert.Throws<DomainException>(() => Notification.Create(template, recipient));
        }

        [Fact]
        public void Create_NullRecipient_ThrowsDomainException()
        {
            // Arrange
            var template = CreateTemplate();
            AssociationMember recipient = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => Notification.Create(template, recipient));
        }

        [Fact]
        public void MarkAsDelivered_ValidDate_SetsReceivedDate()
        {
            // Arrange
            var template = CreateTemplate();
            var recipient = CreateMember();
            var notification = Notification.Create(template, recipient);
            var dateDelivered = DateTimeOffset.Now;

            // Act
            notification.MarkAsDelivered(dateDelivered);

            // Assert
            Assert.Equal(dateDelivered, notification.ReceivedDate);
        }

        [Fact]
        public void MarkAsDelivered_DefaultDate_ThrowsDomainException()
        {
            // Arrange
            var template = CreateTemplate();
            var recipient = CreateMember();
            var notification = Notification.Create(template, recipient);
            var defaultDate = default(DateTimeOffset);

            // Act & Assert
            Assert.Throws<DomainException>(() => notification.MarkAsDelivered(defaultDate));
        }

        [Fact]
        public void MarkAsRead_ValidDateAndReceived_SetsReadDate()
        {
            // Arrange
            var template = CreateTemplate();
            var recipient = CreateMember();
            var notification = Notification.Create(template, recipient);
            var dateDelivered = DateTimeOffset.Now;
            var dateRead = DateTimeOffset.Now;

            notification.MarkAsDelivered(dateDelivered); // Mark as delivered

            // Act
            notification.MarkAsRead(dateRead);

            // Assert
            Assert.Equal(dateRead, notification.ReadDate);
            Assert.True(notification.IsRead);
        }

        [Fact]
        public void MarkAsRead_DefaultDate_ThrowsDomainException()
        {
            // Arrange
            var template = CreateTemplate();
            var recipient = CreateMember();
            var notification = Notification.Create(template, recipient);
            var defaultDate = default(DateTimeOffset);

            notification.MarkAsDelivered(DateTimeOffset.UtcNow);

            // Act & Assert
            Assert.Throws<DomainException>(() => notification.MarkAsRead(defaultDate));
        }

        [Fact]
        public void MarkAsRead_ValidDateAndNotReceived_ThrowsDomainException()
        {
            // Arrange
            var template = CreateTemplate();
            var recipient = CreateMember();
            var notification = Notification.Create(template, recipient);
            var dateRead = DateTimeOffset.Now;

            // Act & Assert
            Assert.Throws<DomainException>(() => notification.MarkAsRead(dateRead));
        }

        [Fact]
        public void MarkAsUnread_SetsReadDateToDefault()
        {
            // Arrange
            var template = CreateTemplate();
            var recipient = CreateMember();
            var notification = Notification.Create(template, recipient);
            notification.MarkAsDelivered(DateTimeOffset.Now);
            notification.MarkAsRead(DateTimeOffset.Now);

            // Act
            notification.MarkAsUnread();

            // Assert
            Assert.Equal(DateTimeOffset.MinValue, notification.ReadDate);
            Assert.False(notification.IsRead);
        }

        private static NotificationTemplate CreateTemplate()
        {
            var title = new NotificationTitle("Test Notification");
            var body = new Text("This is a test notification content.");

            return NotificationTemplate.Create(title, body, NotificationType.Information);
        }

        private static AssociationMember CreateMember()
        {
            var firstName = new FirstName("John");
            var lastName = new LastName("Doe");

            return AssociationMember.Create(firstName, lastName);
        }
    }
}
