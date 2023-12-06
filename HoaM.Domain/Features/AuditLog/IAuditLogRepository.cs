using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public interface IAuditLogRepository : IBaseRepository<AuditLog, AuditId>
    {
    }
}
