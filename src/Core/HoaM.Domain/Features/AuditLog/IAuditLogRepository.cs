namespace HoaM.Domain
{
    /// <summary>
    /// Interface defining repository operations specific to audit log entries.
    /// Inherits from <see cref="IBaseRepository{TEntity, TId}"/> with <see cref="AuditLog"/> entity and <see cref="AuditId"/> identifier.
    /// </summary>
    public interface IAuditLogRepository : IBaseRepository<AuditLog, AuditId>
    {
    }

}
