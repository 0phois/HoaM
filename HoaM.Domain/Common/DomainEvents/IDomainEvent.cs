﻿namespace HoaM.Domain.Common
{
    public interface IDomainEvent
    {
        Guid Id { get; }
        DateTimeOffset TriggeredOn { get; }
    }

    public interface INotifyBefore { }

    public interface INotifyAfter { }
}
