namespace HoaM.Domain.UnitTests
{
    public class NotificationTemplateTests
    {
        [Fact]
        public void Create_ValidNotificationTemplate_ReturnsNotificationTemplateInstance()
        {
            // Arrange
            var title = new NotificationTitle("Test Notification");
            var content = new Text("This is a test notification content.");
            var type = NotificationType.Information;

            // Act
            var template = NotificationTemplate.Create(title, content, type);

            // Assert
            Assert.NotNull(template);
            Assert.Equal(title, template.Title);
            Assert.Equal(content, template.Content);
            Assert.Equal(type, template.Type);
        }

        [Fact]
        public void Create_NullTitle_ThrowsDomainException()
        {
            // Arrange
            NotificationTitle title = null;
            var content = new Text("This is a test notification content.");
            var type = NotificationType.Information;

            // Act & Assert
            Assert.Throws<DomainException>(() => NotificationTemplate.Create(title, content, type));
        }

        [Fact]
        public void Create_NullContent_ThrowsDomainException()
        {
            // Arrange
            var title = new NotificationTitle("Test Notification");
            Text content = null;
            var type = NotificationType.Information;

            // Act & Assert
            Assert.Throws<DomainException>(() => NotificationTemplate.Create(title, content, type));
        }

        [Fact]
        public void Create_InvalidNotificationType_ThrowsDomainException()
        {
            // Arrange
            var title = new NotificationTitle("Test Notification");
            var content = new Text("This is a test notification content.");
            var type = (NotificationType)10; // Invalid notification type

            // Act & Assert
            Assert.Throws<DomainException>(() => NotificationTemplate.Create(title, content, type));
        }
    }
}
