namespace HoaM.Domain.Features
{
    public class Expense : Transaction
    {
        public override TransactionType Type => TransactionType.Debit;
    }
}
