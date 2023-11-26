namespace HoaM.Domain.UnitTests
{
    public class ExpenseTests
    {
        [Fact]
        public void Create_ValidInput_ReturnsExpenseInstance()
        {
            // Arrange
            var title = new TransactionTitle("Maintenance Fee");
            var amount = new Money(100.00M);

            // Act
            var expense = Expense.Create(title, amount);

            // Assert
            Assert.NotNull(expense);
            Assert.Equal(title, expense.Title);
            Assert.Equal(amount, expense.Amount);
            Assert.Equal(TransactionType.Debit, expense.Type);
        }

        [Fact]
        public void Create_NullTitle_ThrowsDomainException()
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => Expense.Create(null, new Money(100.00M)));
        }

        [Fact]
        public void Create_NullAmount_ThrowsDomainException()
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => Expense.Create(new TransactionTitle("Expense"), default));
        }

        [Fact]
        public void WithDueDate_ValidDueDate_SetsEffectiveDate()
        {
            // Arrange
            var expense = CreateExpense();
            var dueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7));

            // Act
            var result = expense.WithDueDate(dueDate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dueDate, expense.EffectiveDate);
        }

        [Fact]
        public void WithDueDate_DefaultDueDate_ThrowsDomainException()
        {
            // Arrange
            var expense = CreateExpense();
            DateOnly dueDate = default;

            // Act & Assert
            Assert.Throws<DomainException>(() => expense.WithDueDate(dueDate));
        }

        [Fact]
        public void WithMemo_ValidMemo_SetsMemo()
        {
            // Arrange
            var expense = CreateExpense();
            var memo = Note.Create(Text.From("Recurring monthly fee"));

            // Act
            expense.WithMemo(memo);

            // Assert
            Assert.Equal(memo, expense.Memo);
        }

        [Fact]
        public void WithMemo_NullMemo_ThrowsDomainException()
        {
            // Arrange
            var expense = CreateExpense();
            Note memo = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => expense.WithMemo(memo));
        }

        private static Expense CreateExpense()
        {
            var title = new TransactionTitle("Maintenance Fee");
            var amount = new Money(100.00M);

            return Expense.Create(title, amount);
        }
    }
}
