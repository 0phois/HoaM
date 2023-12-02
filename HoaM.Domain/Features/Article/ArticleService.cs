using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;

namespace HoaM.Domain.Features
{
    public sealed class ArticleService : IArticleManager
    {
        public TimeProvider SystemClock { get; }

        public ArticleService(TimeProvider systemClock) => SystemClock = systemClock;

        public IResult PublishArticle(Article article)
        {
            if (article.IsPublished) return Results.Failed(DomainErrors.Article.AlreadyPublished.Message);

            try
            {
                article.Publish(SystemClock.GetUtcNow());
            }
            catch (DomainException ex)
            {
                return Results.Exception(ex.Message, ex);
            }

            return Results.Success();
        }
    }
}
