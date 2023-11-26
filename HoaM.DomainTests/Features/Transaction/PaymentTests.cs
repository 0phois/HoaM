namespace HoaM.Domain.UnitTests
{
    public class PaymentTests
    {
        [Fact]
        public void Create_ValidInput_ReturnsPaymentInstance()
        {
            // Arrange
            var title = new TransactionTitle("Maintenance fee payment");
            var amount = new Money(100.00M);

            // Act
            var payment = Payment.Create(title, amount);

            // Assert
            Assert.NotNull(payment);
            Assert.Equal(title, payment.Title);
            Assert.Equal(amount, payment.Amount);
            Assert.Equal(TransactionType.Credit, payment.Type);
        }

        [Fact]
        public void Create_NullTitle_ThrowsDomainException()
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => Payment.Create(null, new Money(500.00M)));
        }

        [Fact]
        public void Create_NullAmount_ThrowsDomainException()
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => Payment.Create(new TransactionTitle("Payment"), default));
        }

        [Fact]
        public void WithDepositDate_ValidDepositDate_SetsEffectiveDate()
        {
            // Arrange
            var payment = CreatePayment();
            var depositDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7));

            // Act
            var result = payment.WithDepositDate(depositDate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(depositDate, payment.EffectiveDate);
        }

        [Fact]
        public void WithDepositDate_DefaultDepositDate_ThrowsDomainException()
        {
            // Arrange
            var payment = CreatePayment();
            DateOnly depositDate = default;

            // Act & Assert
            Assert.Throws<DomainException>(() => payment.WithDepositDate(depositDate));
        }

        [Fact]
        public void WithMemo_ValidMemo_SetsMemo()
        {
            // Arrange
            var payment = CreatePayment();
            var memo = Note.Create(Text.From("Payment for monthly maintenance"));

            // Act
            payment.WithMemo(memo);

            // Assert
            Assert.Equal(memo, payment.Memo);
        }

        [Fact]
        public void WithMemo_NullMemo_ThrowsDomainException()
        {
            // Arrange
            var payment = CreatePayment();
            Note memo = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => payment.WithMemo(memo));
        }

        private static Payment CreatePayment()
        {
            var title = new TransactionTitle("Maintenance payment");
            var amount = new Money(100.00M);

            return Payment.Create(title, amount);
        }
    }
}
