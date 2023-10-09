namespace HoaM.Domain.Entities
{
    public sealed class RecurringExpense : Expense
    {
        /// <summary>
        /// Period/Frequency at which the <see cref="Expense"/> should repeat
        /// </summary>
        public TimeSpan Interval { get; set; }

        /// <summary>
        /// Date and time of the last occurrence of the <see cref="Expense"/>
        /// </summary>
        public DateTimeOffset LastRunDate { get; private set; }
    }
}
