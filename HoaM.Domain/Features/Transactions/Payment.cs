namespace HoaM.Domain.Features
{
    public sealed class Payment : Transaction
    {
        public override TransactionType Type => TransactionType.Credit;
    }
}
