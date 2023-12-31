﻿using HoaM.Domain.Common;

namespace HoaM.Application.Common
{
    /// <summary>
    /// Binds an <see cref="IEntity{TId}"/> to a command.
    /// </summary>
    public interface ICommandBinder<T, out TId> where T : IEntity<TId>
    {
        TId Id { get; }
        T? Entity { get; set; }
    }
}
