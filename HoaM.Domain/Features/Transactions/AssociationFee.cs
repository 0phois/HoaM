using HoaM.Domain.Exceptions;

namespace HoaM.Domain
{
    public class AssociationFee : RecurringTransaction<Expense>
    {
        private AssociationFee() { }

        private AssociationFee(Expense expense, Schedule? frequency = null) : base(expense, frequency) { }

        public static AssociationFee Create(Expense expense, Schedule? frequency = null)
        {
            if (expense is null) throw new DomainException(DomainErrors.AssociationFee.ExpenseNullOrEmpty);

            if (expense.EffectiveDate == default) throw new DomainException(DomainErrors.AssociationFee.DateNullOrEmpty);

            return new AssociationFee(expense, frequency);
        }
    }
}
//TODO - Fee Manager
/*
 Create a service that facilitates editing fee amount, schedule and due date
 Editing the fee means deleting the existing transaction and creating a new copy with the specified updates
 */