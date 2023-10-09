namespace HoaM.Domain.Entities
{
    public sealed class Payment : Transaction
    {
        public override TransactionType Type => TransactionType.Credit;
    }
}
