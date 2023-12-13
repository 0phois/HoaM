using HoaM.Domain;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data
{
    internal sealed class AuditLogRepository(DbContext dbContext) : GenericRepository<AuditLog, AuditId>(dbContext), IAuditLogRepository
    {
        public override void Remove(AuditLog entity) => throw new NotImplementedException();
    }
}
