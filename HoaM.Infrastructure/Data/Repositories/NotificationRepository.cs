using HoaM.Domain;
using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data.Repositories
{
    internal sealed class NotificationRepository(DbContext dbContext) : GenericRepository<Notification, NotificationId>(dbContext), INotificationRepository
    {
    }
}
