namespace HoaM.Domain.UnitTests
{
    public class AssociationFeeTests
    {
        [Fact]
        public void Create_ValidInput_ReturnsAssociationFeeInstance()
        {
            // Arrange
            var expense = Expense.Create(new TransactionTitle("Monthly Fee"), new Money(50.00M)).WithDueDate(DateOnly.FromDateTime(DateTime.Now));
            var frequency = Schedule.CreateMonthly();

            // Act
            var associationFee = AssociationFee.Create(expense, frequency);

            // Assert
            Assert.NotNull(associationFee);
            Assert.Equal(expense, associationFee.Data);
            Assert.Equal(expense.Title.Value, associationFee.Title.Value);
            Assert.Equal(expense.EffectiveDate, associationFee.Data.EffectiveDate);
            Assert.Equal(frequency, associationFee.Schedule);
        }

        [Fact]
        public void Create_NullExpense_ThrowsDomainException()
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => AssociationFee.Create(null, Schedule.CreateMonthly()));
        }
    }

}
