using HoaM.Domain;
using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;
namespace HoaM.Infrastructure.Data.Repositories
{
    internal sealed class EventRepository<T>(DbContext dbContext) : GenericRepository<Event<T>, EventId>(dbContext), IEventRepository<T>
    {
    }
}
