namespace HoaM.Domain.UnitTests
{
    public class ArticleServiceTests
    {
        [Fact]
        public void PublishArticle_WhenArticleIsNotPublished_ReturnsSuccess()
        {
            // Arrange
            var systemClock = TimeProvider.System;
            var articleService = new ArticleService(systemClock);
            var article = Article.CreateAnnouncement(ArticleTitle.From("New Announcement"), Text.From("Successfully published article"));

            // Act
            var result = articleService.PublishArticle(article);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void PublishArticle_WhenArticleIsAlreadyPublished_ReturnsFailedResult()
        {
            // Arrange
            var systemClock = TimeProvider.System;
            var articleService = new ArticleService(systemClock);
            var article = Article.CreateAnnouncement(ArticleTitle.From("New Announcement"), Text.From("Successfully published article"));

            var firstPublish = articleService.PublishArticle(article);

            // Act
            var secondPublish = articleService.PublishArticle(article);

            // Assert
            Assert.True(firstPublish.IsSuccess);
            Assert.False(secondPublish.IsSuccess);
            Assert.Equal(DomainErrors.Article.AlreadyPublished.Message, secondPublish.Message);
        }

        [Fact]
        public void PublishArticle_WhenArticlePublishThrowsDomainException_ReturnsExceptionResult()
        {
            // Arrange
            var systemClock = TimeProvider.System;
            var articleService = new ArticleService(systemClock);
            var article = Article.CreateAnnouncement(ArticleTitle.From("New Announcement"), Text.From("Announcement content"));

            // Act
            var result = articleService.PublishArticle(article);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.IsType<DomainException>((result as ExceptionResult)?.Exception);
        }
    }
}
