namespace HoaM.Domain.Features
{
    public class AssociationFee : RecurringTransaction<Expense>
    {
        private AssociationFee() { }

        private AssociationFee(Expense activity, EventTitle title, DateTimeOffset start, DateTimeOffset stop, Schedule? frequency = null)
            : base(activity, title, start, stop, frequency) { }

        public static AssociationFee Create(Expense expense, Schedule? frequency = null)
        {
            return new AssociationFee(expense, EventTitle.From(expense.Title.Value), expense.EffectiveDate, expense.EffectiveDate, frequency);
        }
    }
}
//TODO - Fee Manager
/*
 Create a service that facilitates editing fee amount, schedule and due date
 Editing the fee means deleting the existing transaction and creating a new copy with the specified updates
 */