﻿using HoaM.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoaM.Extensions.Mediatr
{
    internal sealed class MediatrDomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MediatrDomainEventDispatcher> _logger;

        public MediatrDomainEventDispatcher(IMediator mediator, ILogger<MediatrDomainEventDispatcher> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task PublishAsync(IDomainEvent devent)
        {
            var domainEventNotification = CreateDomainEventNotification(devent);

            _logger.LogDebug("Dispatching Domain Event as MediatR notification.  EventType: {eventType}", devent.GetType());

            await _mediator.Publish(domainEventNotification);
        }

        private static INotification CreateDomainEventNotification<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            return new MediatrDomainEventMessage<TDomainEvent>(domainEvent);
        }
    }
}