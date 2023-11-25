namespace HoaM.Domain.UnitTests
{
    public class ParcelTests
    {
        [Fact]
        public void CreateParcel_ValidLot_ReturnsParcelInstance()
        {
            // Arrange
            var lot = Lot.Create(LotNumber.From("123"));

            // Act
            var parcel = Residence.Create(DevelopmentStatus.EmptyLot, lot);

            // Assert
            Assert.NotNull(parcel);
        }

        [Fact]
        public void AddLot_ValidLot_ReturnsParcelInstance()
        {
            // Arrange
            var lot1 = Lot.Create(LotNumber.From("123"));

            var parcel = Residence.Create(DevelopmentStatus.EmptyLot, lot1);

            // Act
            var lot2 = Lot.Create(LotNumber.From("125"));
            var result = parcel.AddLot<Parcel>(lot2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(parcel, result);
        }

        [Fact]
        public void AddLot_NullLot_ThrowsDomainException()
        {
            // Arrange
            var parcel = CreateParcel();
            Lot lot = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => parcel.AddLot<Parcel>(lot));
        }

        [Fact]
        public void AddLot_ThrowsException_WhenDuplicateLotNumber()
        {
            // Arrange
            var parcel = CreateParcel();
            var lot = Lot.Create(LotNumber.From("123"));

            // Act & Assert
            Assert.Throws<DomainException>(() => parcel.AddLot<Parcel>(lot));
        }

        [Fact]
        public void WithLots_ValidLots_ReturnsParcelInstance()
        {
            // Arrange
            var parcel = CreateParcel();
            var lot1 = Lot.Create(LotNumber.From("455"));
            var lot2 = Lot.Create(LotNumber.From("456"));

            // Act
            var result = parcel.WithLots<Parcel>(lot1, lot2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(parcel, result);
        }

        [Fact]
        public void WithLots_NullLots_ThrowsDomainException()
        {
            // Arrange
            var parcel = CreateParcel();
            Lot[] lots = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => parcel.WithLots<Parcel>(lots));
        }

        [Fact]
        public void WithLots_EmptyLots_ThrowsDomainException()
        {
            // Arrange
            var parcel = CreateParcel();
            Lot[] lots = [];

            // Act & Assert
            Assert.Throws<DomainException>(() => parcel.WithLots<Parcel>(lots));
        }

        [Fact]
        public void WithLots_ReturnsUpdatedParcel_WhenValidLotsAdded()
        {
            // Arrange
            var parcel = CreateParcel();
            var lot1 = Lot.Create(LotNumber.From("455"));
            var lot2 = Lot.Create(LotNumber.From("456"));

            // Act
            var updatedParcel = parcel.WithLots<Parcel>(lot1, lot2);

            // Assert
            Assert.Same(parcel, updatedParcel);
            Assert.Equal(2, parcel.Lots.Count);
            Assert.Contains(lot1, parcel.Lots);
            Assert.Contains(lot2, parcel.Lots);
        }

        [Fact]
        public void WithLots_ThrowsException_WhenDuplicateLotNumberInArray()
        {
            // Arrange
            var parcel = CreateParcel();
            var lot1 = Lot.Create(LotNumber.From("123"));
            var lot2 = Lot.Create(LotNumber.From("123"));

            // Act & Assert
            Assert.Throws<DomainException>(() => parcel.WithLots<Parcel>(lot1, lot2));
        }

        [Fact]
        public void WithAddress_ValidAddress_ReturnsParcelInstance()
        {
            // Arrange
            var parcel = CreateParcel();
            var streetNumber = new StreetNumber("123");
            var streetName = new StreetName("Main Street");

            // Act
            var result = parcel.WithAddress<Parcel>(streetNumber, streetName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(parcel, result);
        }

        [Fact]
        public void WithAddress_NullStreetNumber_ThrowsDomainException()
        {
            // Arrange
            var parcel = CreateParcel();
            StreetNumber streetNumber = null;
            var streetName = new StreetName("Main Street");

            // Act & Assert
            Assert.Throws<DomainException>(() => parcel.WithAddress<Parcel>(streetNumber, streetName));
        }

        [Fact]
        public void WithAddress_NullStreetName_ThrowsDomainException()
        {
            // Arrange
            var parcel = CreateParcel();
            var streetNumber = new StreetNumber("123");
            StreetName streetName = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => parcel.WithAddress<Parcel>(streetNumber, streetName));
        }

        [Fact]
        public void EditStreetName_DifferentStreetName_ChangesStreetName()
        {
            // Arrange
            var parcel = CreateParcel();
            var originalStreetName = new StreetName("Main Street");
            parcel.EditStreetName(originalStreetName);
            var newStreetName = new StreetName("New Street");

            // Act
            parcel.EditStreetName(newStreetName);

            // Assert
            Assert.Equal(newStreetName, parcel.StreetName);
        }

        [Fact]
        public void EditStreetName_NullStreetName_ThrowsDomainException()
        {
            // Arrange
            var parcel = CreateParcel();
            StreetName streetName = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => parcel.EditStreetName(streetName));
        }

        [Fact]
        public void EditStreetNumber_DifferentStreetNumber_ChangesStreetNumber()
        {
            // Arrange
            var parcel = CreateParcel();
            var originalStreetNumber = new StreetNumber("123");
            parcel.EditStreetNumber(originalStreetNumber);
            var newStreetNumber = new StreetNumber("456");

            // Act
            parcel.EditStreetNumber(newStreetNumber);

            // Assert
            Assert.Equal(newStreetNumber, parcel.StreetNumber);
        }

        [Fact]
        public void EditStreetNumber_DefaultStreetNumber_ThrowsDomainException()
        {
            // Arrange
            var parcel = CreateParcel();
            var defaultStreetNumber = default(StreetNumber);

            // Act & Assert
            Assert.Throws<DomainException>(() => parcel.EditStreetNumber(defaultStreetNumber));
        }

        [Fact]
        public void UpdateDevelopmentStatus_ValidStatus_ChangesStatus()
        {
            // Arrange
            var parcel = CreateParcel();
            var newStatus = DevelopmentStatus.Completed;

            // Act
            parcel.UpdateDevelopmentStatus(newStatus);

            // Assert
            Assert.Equal(newStatus, parcel.Status);
        }

        [Fact]
        public void UpdateDevelopmentStatus_InvalidStatus_ThrowsDomainException()
        {
            // Arrange
            var parcel = CreateParcel();
            var invalidStatus = (DevelopmentStatus)10; // Invalid status

            // Act & Assert
            Assert.Throws<DomainException>(() => parcel.UpdateDevelopmentStatus(invalidStatus));
        }

        private static Parcel CreateParcel()
        {
            var lot = Lot.Create(LotNumber.From("123"));

            return GreenSpace.Create(DevelopmentStatus.EmptyLot, lot);
        }
    }
}
