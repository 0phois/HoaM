using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public interface IArticleManager
    {
        IResult PublishArticle(Article article);
    }
}