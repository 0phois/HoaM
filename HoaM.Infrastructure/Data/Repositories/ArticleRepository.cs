using HoaM.Domain;
using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data
{
    internal sealed class ArticleRepository(DbContext dbContext) : GenericRepository<Article, ArticleId>(dbContext), IArticleRepository
    {
    }
}
