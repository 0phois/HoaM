namespace HoaM.Domain.UnitTests
{
    public class ResidenceTests
    {
        [Fact]
        public void Create_ValidInput_ReturnsResidenceInstance()
        {
            // Arrange
            var lot1 = Lot.Create(new LotNumber("123"));
            var lot2 = Lot.Create(new LotNumber("124"));

            // Act
            var residence = Residence.Create(DevelopmentStatus.Completed, lot1, lot2);

            // Assert
            Assert.NotNull(residence);
            Assert.Equal(DevelopmentStatus.Completed, residence.Status);
            Assert.Equal(2, residence.Lots.Count);
        }

        [Fact]
        public void Create_NullLots_ThrowsDomainException()
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => Residence.Create(DevelopmentStatus.Completed, null));
        }

        [Fact]
        public void Create_NoLots_ThrowsDomainException()
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => Residence.Create(DevelopmentStatus.Completed));
        }

        [Fact]
        public void Create_InvalidStatus_ThrowsDomainException()
        {
            // Arrange
            var lot = Lot.Create(new LotNumber("123"));

            // Act & Assert
            Assert.Throws<DomainException>(() => Residence.Create((DevelopmentStatus)10, lot));
        }
    }

}
