namespace HoaM.Domain.Common
{
    /// <summary>
    /// Represents the type of <see cref="Contracts.ITransaction"/>
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
