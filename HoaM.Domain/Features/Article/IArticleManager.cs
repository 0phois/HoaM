using HoaM.Domain.Common;

namespace HoaM.Domain
{
    public interface IArticleManager
    {
        IResult PublishArticle(Article article);
    }
}