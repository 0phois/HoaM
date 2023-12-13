using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;

namespace HoaM.Domain
{
    /// <summary>
    /// Service responsible for managing articles.
    /// Implements the <see cref="IArticleManager"/> interface.
    /// </summary>
    public sealed class ArticleService : IArticleManager
    {
        /// <summary>
        /// Gets the system clock provided for handling time-related operations.
        /// </summary>
        public TimeProvider SystemClock { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleService"/> class with the specified system clock.
        /// </summary>
        /// <param name="systemClock">The system clock to use for handling time-related operations.</param>
        public ArticleService(TimeProvider systemClock)
        {
            SystemClock = systemClock ?? throw new ArgumentNullException(nameof(systemClock));
        }

        /// <summary>
        /// Publishes the specified article.
        /// </summary>
        /// <param name="article">The article to be published.</param>
        /// <returns>A result indicating the success or failure of the publishing operation.</returns>
        public IResult PublishArticle(Article article)
        {
            // Check if the article is already published
            if (article.IsPublished)
            {
                return Results.Failed(DomainErrors.Article.AlreadyPublished.Message);
            }

            try
            {
                // Attempt to publish the article using the current system time
                article.Publish(SystemClock.GetUtcNow());
            }
            catch (DomainException ex)
            {
                // Handle domain-specific exceptions and return an exception result
                return Results.Exception(ex.Message, ex);
            }

            // Return a success result if the publishing operation is successful
            return Results.Success();
        }
    }

}
