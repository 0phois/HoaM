using HoaM.Domain.Exceptions;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents an association fee, which is a recurring expense.
    /// </summary>
    public class AssociationFee : RecurringTransaction<Expense>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="AssociationFee"/> class.
        /// </summary>
        private AssociationFee() { }

        /// <summary>
        /// Creates a new instance of the <see cref="AssociationFee"/> class with the specified expense and frequency.
        /// </summary>
        /// <param name="expense">The recurring expense associated with the association fee.</param>
        /// <param name="frequency">The schedule frequency for the association fee.</param>
        private AssociationFee(Expense expense, Schedule? frequency = null) : base(expense, frequency) { }

        /// <summary>
        /// Creates a new association fee with the specified expense and frequency.
        /// </summary>
        /// <param name="expense">The recurring expense associated with the association fee.</param>
        /// <param name="frequency">The schedule frequency for the association fee.</param>
        /// <returns>A new <see cref="AssociationFee"/> instance.</returns>
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