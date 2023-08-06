using HoaM.Domain.Common;

namespace HoaM.Domain.Entities
{
    public class Expense : Transaction
    {
        public override TransactionType Type => TransactionType.Debit;
    }
}
