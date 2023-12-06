using HoaM.Domain;
using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data
{
    internal sealed class NotificationTemplateRepository(DbContext dbContext) : GenericRepository<NotificationTemplate, NotificationTemplateId>(dbContext), ITemplateRepository
    {
    }
}
