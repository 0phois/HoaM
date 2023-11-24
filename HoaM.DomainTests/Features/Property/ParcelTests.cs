namespace HoaM.Domain.UnitTests
{
    public class ParcelTests
    {
        //[Fact]
        //public void AddLot_ValidLot_ReturnsParcelInstance()
        //{
        //    // Arrange
        //    var parcel = CreateParcel();

        //    // Act
        //    var result = parcel.AddLot<Parcel>(lot);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(parcel, result);
        //}

        //[Fact]
        //public void AddLot_NullLot_ThrowsDomainException()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    Lot lot = null;

        //    // Act & Assert
        //    Assert.Throws<DomainException>(() => parcelMock.AddLot<Parcel>(lot));
        //}

        //[Fact]
        //public void WithLots_ValidLots_ReturnsParcelInstance()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    var lot1 = new Lot();
        //    var lot2 = new Lot();

        //    // Act
        //    var result = parcelMock.WithLots<Parcel>(lot1, lot2);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(parcelMock, result);
        //}

        //[Fact]
        //public void WithLots_NullLots_ThrowsDomainException()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    Lot[] lots = null;

        //    // Act & Assert
        //    Assert.Throws<DomainException>(() => parcelMock.WithLots<Parcel>(lots));
        //}

        //[Fact]
        //public void WithLots_EmptyLots_ThrowsDomainException()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    Lot[] lots = Array.Empty<Lot>();

        //    // Act & Assert
        //    Assert.Throws<DomainException>(() => parcelMock.WithLots<Parcel>(lots));
        //}

        //[Fact]
        //public void WithAddress_ValidAddress_ReturnsParcelInstance()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    var streetNumber = new StreetNumber("123");
        //    var streetName = new StreetName("Main Street");

        //    // Act
        //    var result = parcelMock.WithAddress<Parcel>(streetNumber, streetName);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(parcelMock, result);
        //}

        //[Fact]
        //public void WithAddress_NullStreetNumber_ThrowsDomainException()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    StreetNumber streetNumber = null;
        //    var streetName = new StreetName("Main Street");

        //    // Act & Assert
        //    Assert.Throws<DomainException>(() => parcelMock.WithAddress<Parcel>(streetNumber, streetName));
        //}

        //[Fact]
        //public void WithAddress_NullStreetName_ThrowsDomainException()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    var streetNumber = new StreetNumber("123");
        //    StreetName streetName = null;

        //    // Act & Assert
        //    Assert.Throws<DomainException>(() => parcelMock.WithAddress<Parcel>(streetNumber, streetName));
        //}

        //[Fact]
        //public void EditStreetName_SameStreetName_NoChangeInStreetName()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    var streetName = new StreetName("Main Street");
        //    parcelMock.EditStreetName(streetName);

        //    // Act
        //    parcelMock.EditStreetName(streetName);

        //    // Assert
        //    Assert.Equal(streetName, parcelMock.StreetName);
        //}

        //[Fact]
        //public void EditStreetName_DifferentStreetName_ChangesStreetName()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    var originalStreetName = new StreetName("Main Street");
        //    parcelMock.EditStreetName(originalStreetName);
        //    var newStreetName = new StreetName("New Street");

        //    // Act
        //    parcelMock.EditStreetName(newStreetName);

        //    // Assert
        //    Assert.Equal(newStreetName, parcelMock.StreetName);
        //}

        //[Fact]
        //public void EditStreetName_NullStreetName_ThrowsDomainException()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    StreetName streetName = null;

        //    // Act & Assert
        //    Assert.Throws<DomainException>(() => parcelMock.EditStreetName(streetName));
        //}

        //[Fact]
        //public void EditStreetNumber_SameStreetNumber_NoChangeInStreetNumber()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    var streetNumber = new StreetNumber("123");
        //    parcelMock.EditStreetNumber(streetNumber);

        //    // Act
        //    parcelMock.EditStreetNumber(streetNumber);

        //    // Assert
        //    Assert.Equal(streetNumber, parcelMock.StreetNumber);
        //}

        //[Fact]
        //public void EditStreetNumber_DifferentStreetNumber_ChangesStreetNumber()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    var originalStreetNumber = new StreetNumber("123");
        //    parcelMock.EditStreetNumber(originalStreetNumber);
        //    var newStreetNumber = new StreetNumber("456");

        //    // Act
        //    parcelMock.EditStreetNumber(newStreetNumber);

        //    // Assert
        //    Assert.Equal(newStreetNumber, parcelMock.StreetNumber);
        //}

        //[Fact]
        //public void EditStreetNumber_DefaultStreetNumber_ThrowsDomainException()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    var defaultStreetNumber = default(StreetNumber);

        //    // Act & Assert
        //    Assert.Throws<DomainException>(() => parcelMock.EditStreetNumber(defaultStreetNumber));
        //}

        //[Fact]
        //public void UpdateDevelopmentStatus_ValidStatus_ChangesStatus()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    var originalStatus = DevelopmentStatus.Pending;
        //    parcelMock.UpdateDevelopmentStatus(originalStatus);
        //    var newStatus = DevelopmentStatus.Completed;

        //    // Act
        //    parcelMock.UpdateDevelopmentStatus(newStatus);

        //    // Assert
        //    Assert.Equal(newStatus, parcelMock.Status);
        //}

        //[Fact]
        //public void UpdateDevelopmentStatus_SameStatus_NoChangeInStatus()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    var originalStatus = DevelopmentStatus.Pending;
        //    parcelMock.UpdateDevelopmentStatus(originalStatus);

        //    // Act
        //    parcelMock.UpdateDevelopmentStatus(originalStatus);

        //    // Assert
        //    Assert.Equal(originalStatus, parcelMock.Status);
        //}

        //[Fact]
        //public void UpdateDevelopmentStatus_InvalidStatus_ThrowsDomainException()
        //{
        //    // Arrange
        //    var parcelMock = new Mock<Parcel>().Object;
        //    var originalStatus = DevelopmentStatus.Pending;
        //    parcelMock.UpdateDevelopmentStatus(originalStatus);
        //    var invalidStatus = (DevelopmentStatus)10; // Invalid status

        //    // Act & Assert
        //    Assert.Throws<DomainException>(() => parcelMock.UpdateDevelopmentStatus(invalidStatus));
        //}

        private static Parcel CreateParcel()
        {
            var lot = Lot.Create(LotNumber.From("123"));

            return GreenSpace.Create(DevelopmentStatus.EmptyLot, lot);
        }
    }
}
