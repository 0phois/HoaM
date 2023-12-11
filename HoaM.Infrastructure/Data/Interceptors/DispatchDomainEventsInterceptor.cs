using HoaM.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Immutable;

namespace HoaM.Infrastructure.Data
{
    internal sealed class DispatchDomainEventsInterceptor(IDomainEventDispatcher eventDispatcher) : SaveChangesInterceptor
    {
        private IDomainEventDispatcher Dispatcher { get; } = eventDispatcher;

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents<DomainEvent>(eventData.Context).GetAwaiter().GetResult();

            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents<DomainEvent>(eventData.Context);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            DispatchDomainEvents<DomainNotification>(eventData.Context).GetAwaiter().GetResult();

            return base.SavedChanges(eventData, result);
        }

        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents<DomainNotification>(eventData.Context);

            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        public async Task DispatchDomainEvents<TEventType>(DbContext? context) where TEventType : class, IDomainEvent
        {
            if (context == null) return;

            var entities = context.ChangeTracker.Entries<IEntity>()
                                                .Where(e => e.Entity.DomainEvents.OfType<TEventType>().Any())
                                                .Select(e => e.Entity);

            var domainEvents = entities.SelectMany(e => e.DomainEvents.OfType<TEventType>()).ToImmutableArray();

            if (typeof(TEventType) == typeof(DomainNotification))
                entities.ToList().ForEach(e => e.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await Dispatcher.PublishAsync(domainEvent);
        }
    }
}
