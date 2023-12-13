namespace HoaM.Domain
{
    /// <summary>
    /// Interface defining repository operations specific to articles.
    /// Inherits from <see cref="IBaseRepository{TEntity, TId}"/> with <see cref="Article"/> entity and <see cref="ArticleId"/> identifier.
    /// </summary>
    public interface IArticleRepository : IBaseRepository<Article, ArticleId>
    {
    }

}
