using HoaM.Domain;
using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data
{
    internal sealed class AuditLogRepository(DbContext dbContext) : GenericRepository<AuditLog, AuditId>(dbContext), IAuditLogRepository
    {
    }
}
