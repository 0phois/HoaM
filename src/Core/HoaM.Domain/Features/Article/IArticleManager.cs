using HoaM.Domain.Common;

namespace HoaM.Domain
{
    /// <summary>
    /// Interface defining operations for managing articles.
    /// </summary>
    public interface IArticleManager
    {
        /// <summary>
        /// Publishes the specified article.
        /// </summary>
        /// <param name="article">The article to be published.</param>
        /// <returns>A result indicating the success or failure of the publishing operation.</returns>
        IResult PublishArticle(Article article);
    }

}