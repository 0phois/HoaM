using HoaM.Domain;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data
{
    internal sealed class NotificationTemplateRepository(DbContext dbContext) : GenericRepository<NotificationTemplate, NotificationTemplateId>(dbContext), INotificationTemplateRepository
    {
    }
}
