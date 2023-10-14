using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public interface IArticleManager
    {
        ISystemClock SystemClock { get; }

        IResult PublishArticle(Article article);
    }
}