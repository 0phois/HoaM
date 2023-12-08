using HoaM.Domain.Common;
using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HoaM.Infrastructure.Data
{
    internal sealed class AuditableEntityInterceptor(IMember member, TimeProvider timeProvider) : SaveChangesInterceptor
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

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            //TODO - implement audit logs
            //https://github.com/thepirat000/Audit.NET/blob/master/src/Audit.EntityFramework/DbContextHelper.cs#L531
            //https://github.com/thepirat000/Audit.NET/blob/master/src/Audit.EntityFramework/DbContextHelper.Core.cs#L314
            return base.SavedChanges(eventData, result);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<IAuditable>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = _member.Id;
                    entry.Entity.CreatedDate = _timeProvider.GetUtcNow();
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    entry.Entity.LastModifiedBy = _member.Id;
                    entry.Entity.LastModifiedDate = _timeProvider.GetUtcNow();
                }
            }
        }
    }

    internal static class Extensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) => entry.References.Any(reference =>
                reference.TargetEntry is not null &&
                reference.TargetEntry.Metadata.IsOwned() &&
                (reference.TargetEntry.State == EntityState.Added || reference.TargetEntry.State == EntityState.Modified)
        );
    }
}
