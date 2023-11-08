using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;

namespace HoaM.Domain.Features
{
    public sealed class ArticleService : IArticleManager
    {
        public ISystemClock SystemClock { get; }

        public ArticleService(ISystemClock systemClock) => SystemClock = systemClock;

        public IResult PublishArticle(Article article)
        {
            if (article.IsPublished) return Results.Failed(DomainErrors.Article.AlreadyPublished.Message);

            try
            {
                article.Publish(SystemClock.UtcNow);
            }
            catch (DomainException ex)
            {
                return Results.Exception(ex.Message, ex);
            }

            return Results.Success();
        }
    }
}
