﻿using HoaM.Domain;
using HoaM.Domain.Features;
using HoaM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure
{
    internal sealed class MeetingRepository(DbContext dbContext) : GenericRepository<Meeting, MeetingId>(dbContext), IMeetingRepository
    {
    }
}
