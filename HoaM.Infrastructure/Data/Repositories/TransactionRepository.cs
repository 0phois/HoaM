using HoaM.Domain;
using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data
{
    internal sealed class TransactionRepository(DbContext dbContext) : GenericRepository<Transaction, TransactionId>(dbContext), ITransactionRepository
    {
    }
}
