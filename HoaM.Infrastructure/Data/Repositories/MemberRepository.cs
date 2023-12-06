﻿using HoaM.Domain;
using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data
{
    internal sealed class MemberRepository(DbContext dbContext) : GenericRepository<AssociationMember, AssociationMemberId>(dbContext), IMemberRepository
    {
        public Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
