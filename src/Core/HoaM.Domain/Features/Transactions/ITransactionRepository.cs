namespace HoaM.Domain
{
    /// <summary>
    /// Represents a repository for managing transactions.
    /// </summary>
    public interface ITransactionRepository : IBaseRepository<Transaction, TransactionId>
    {
    }
}
