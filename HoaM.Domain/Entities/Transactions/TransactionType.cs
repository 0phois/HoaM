namespace HoaM.Domain.Entities
{
    /// <summary>
    /// Represents the type of <see cref="Transactions.Contracts.ITransaction"/>
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// Positive transaction representing monies owed
        /// </summary>
        Debit,
        /// <summary>
        /// Negative transaction representing monies paid
        /// </summary>
        Credit
    }
}
