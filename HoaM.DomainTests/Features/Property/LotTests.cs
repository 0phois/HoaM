namespace HoaM.Domain.UnitTests
{
    public class LotTests
    {
        [Fact]
        public void Create_ValidLot_ReturnsLotInstance()
        {
            // Arrange
            var lotNumber = new LotNumber("123");

            // Act
            var lot = Lot.Create(lotNumber);

            // Assert
            Assert.NotNull(lot);
            Assert.Equal(lotNumber, lot.Number);
        }

        [Fact]
        public void Create_NullLotNumber_ThrowsDomainException()
        {
            // Arrange
            LotNumber lotNumber = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => Lot.Create(lotNumber));
        }
    }
}
