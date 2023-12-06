using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public interface ITransactionRepository : IBaseRepository<Transaction, TransactionId>
    {
    }
}
