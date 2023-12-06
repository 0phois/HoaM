using HoaM.Domain.Common;
using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HoaM.Infrastructure.Data
{
    internal class SoftDeleteEntityInterceptor(IMember member, TimeProvider timeProvider) : SaveChangesInterceptor
    {
        private readonly IMember _member = member;
        private readonly TimeProvider _timeProvider = timeProvider;

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<ISoftDelete>())
            {
                if (entry.Entity is ISoftDelete softDelete)
                {
                    softDelete.DeletedBy = _member.Id;
                    softDelete.DeletionDate = _timeProvider.GetUtcNow();

                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
