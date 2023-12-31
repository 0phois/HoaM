﻿namespace HoaM.Domain.UnitTests
{
    public class ArticleTests
    {
        [Fact]
        public void CreateBulletin_WithValidParameters_ShouldCreateBulletinArticle()
        {
            // Arrange
            var title = new ArticleTitle("Valid Title");
            var body = new Text("Valid Body");

            // Act
            var article = Article.CreateBulletin(title, body);

            // Assert
            Assert.NotNull(article);
            Assert.Equal(ArticleType.Bulletin, article.Type);
            Assert.Equal(title, article.Title);
            Assert.Equal(body, article.Body);
            Assert.False(article.IsPublished);
        }

        [Fact]
        public void CreateBulletin_WithNullBody_ShouldThrowException()
        {
            // Arrange
            var title = new ArticleTitle("Title");
            Text body = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => Article.CreateBulletin(title, body));
        }

        [Fact]
        public void CreateAnnouncement_WithValidParameters_ShouldCreateAnnouncementArticle()
        {
            // Arrange
            var title = new ArticleTitle("Valid Title");
            var body = new Text("Valid Body");

            // Act
            var article = Article.CreateAnnouncement(title, body);

            // Assert
            Assert.NotNull(article);
            Assert.Equal(ArticleType.Announcement, article.Type);
            Assert.Equal(title, article.Title);
            Assert.Equal(body, article.Body);
            Assert.False(article.IsPublished);
        }

        [Fact]
        public void CreateAnnouncement_WithNullTitle_ShouldThrowException()
        {
            // Arrange 
            ArticleTitle title = null;
            var body = new Text("Body");

            // Act & Assert
            Assert.Throws<DomainException>(() => Article.CreateAnnouncement(title, body));
        }

        [Fact]
        public void EditTitle_WithValidTitle_ShouldChangeTitle()
        {
            // Arrange
            var article = Article.CreateBulletin(new ArticleTitle("Initial Title"), new Text("Body"));

            // Act
            article.EditTitle(new ArticleTitle("Updated Title"));

            // Assert
            Assert.Equal("Updated Title", article.Title.Value);
        }

        [Fact]
        public void EditTitle_WithNullTitle_ShouldThrowException()
        {
            // Arrange
            var article = Article.CreateBulletin(new ArticleTitle("Title"), new Text("Body"));

            // Act & Assert
            Assert.Throws<DomainException>(() => article.EditTitle(null));
        }

        [Fact]
        public void EditBody_WithValidBody_ShouldChangeBody()
        {
            // Arrange
            var article = Article.CreateBulletin(new ArticleTitle("Title"), new Text("Initial Body"));

            // Act
            article.EditBody(new Text("Updated Body"));

            // Assert
            Assert.Equal("Updated Body", article.Body.Value);
        }

        [Fact]
        public void EditBody_WithNullBody_ShouldThrowException()
        {
            // Arrange
            var article = Article.CreateBulletin(new ArticleTitle("Title"), new Text("Body"));

            // Act & Assert
            Assert.Throws<DomainException>(() => article.EditBody(null));
        }

        [Fact]
        public void Publish_WithValidDate_ShouldSetPublishedDate()
        {
            // Arrange
            var article = Article.CreateBulletin(new ArticleTitle("Title"), new Text("Body"));
            var datePublished = DateTimeOffset.Now;

            // Act
            article.Publish(datePublished);

            // Assert
            Assert.True(article.IsPublished);
            Assert.Equal(datePublished, article.PublishedDate);
        }

        [Fact]
        public void Publish_AlreadyPublishedArticle_ShouldThrowException()
        {
            // Arrange
            var article = Article.CreateBulletin(new ArticleTitle("Title"), new Text("Body"));
            var datePublished = DateTimeOffset.Now;
            article.Publish(datePublished);

            // Act & Assert
            Assert.Throws<DomainException>(() => article.Publish(DateTimeOffset.Now.AddDays(1)));
        }

        [Fact]
        public void Publish_WithDefaultDate_ShouldThrowException()
        {
            // Arrange
            var article = Article.CreateBulletin(new ArticleTitle("Title"), new Text("Body"));

            // Act & Assert
            Assert.Throws<DomainException>(() => article.Publish(default));
        }
    }
}
