﻿using HoaM.Domain;
using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data
{
    internal sealed class LotRepository(DbContext dbContext) : GenericRepository<Lot, LotId>(dbContext), ILotRepository
    {
        public Task<Lot> GetByIdAsync(LotId parcelId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsLotNumberUnique(LotNumber number, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}