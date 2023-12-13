namespace HoaM.Domain.UnitTests
{
    public class PhoneNumberTests
    {
        [Fact]
        public void Create_ValidPhoneNumber_ReturnsPhoneNumberInstance()
        {
            // Arrange
            var type = PhoneType.Mobile;
            var countryCode = new CountryCallingCode("1");
            var areaCode = new AreaCode("123");
            var prefix = new PhonePrefix("456");
            var lastDigits = new LineNumber("7890");

            // Act
            var phoneNumber = PhoneNumber.Create(type, countryCode, areaCode, prefix, lastDigits);

            // Assert
            Assert.NotNull(phoneNumber);
            Assert.Equal(type, phoneNumber.Type);
            Assert.Equal(countryCode, phoneNumber.CountryCode);
            Assert.Equal(areaCode, phoneNumber.AreaCode);
            Assert.Equal(prefix, phoneNumber.Prefix);
            Assert.Equal(lastDigits, phoneNumber.Number);
        }

        [Fact]
        public void Create_InvalidCountryCode_ThrowsDomainException()
        {
            // Arrange
            var type = PhoneType.Home;
            var countryCode = default(CountryCallingCode);  // Invalid country code
            var areaCode = new AreaCode("123");
            var prefix = new PhonePrefix("555");
            var lastDigits = new LineNumber("7890");

            // Act & Assert
            Assert.Throws<DomainException>(() => PhoneNumber.Create(type, countryCode, areaCode, prefix, lastDigits));
        }

        [Fact]
        public void Create_InvalidAreaCode_ThrowsDomainException()
        {
            // Arrange
            var type = PhoneType.Home;
            var countryCode = new CountryCallingCode("1");
            var areaCode = default(AreaCode);  // Invalid area code
            var prefix = new PhonePrefix("555");
            var lastDigits = new LineNumber("7890");

            // Act & Assert
            Assert.Throws<DomainException>(() => PhoneNumber.Create(type, countryCode, areaCode, prefix, lastDigits));
        }

        [Fact]
        public void Create_InvalidPrefix_ThrowsDomainException()
        {
            // Arrange
            var type = PhoneType.Home;
            var countryCode = new CountryCallingCode("1");
            var areaCode = new AreaCode("123");
            var prefix = default(PhonePrefix); // Invalid prefix
            var lastDigits = new LineNumber("7890");

            // Act & Assert
            Assert.Throws<DomainException>(() => PhoneNumber.Create(type, countryCode, areaCode, prefix, lastDigits));
        }

        [Fact]
        public void Create_InvalidLastDigits_ThrowsDomainException()
        {
            // Arrange
            var type = PhoneType.Work;
            var countryCode = new CountryCallingCode("1");
            var areaCode = new AreaCode("123");
            var prefix = new PhonePrefix("456");
            var lastDigits = default(LineNumber); // Invalid last digits

            // Act & Assert
            Assert.Throws<DomainException>(() => PhoneNumber.Create(type, countryCode, areaCode, prefix, lastDigits));
        }

        [Fact]
        public void Create_InvalidPhoneType_ThrowsDomainException()
        {
            // Arrange
            var type = (PhoneType)10; // Invalid phone type
            var countryCode = new CountryCallingCode("1");
            var areaCode = new AreaCode("123");
            var prefix = new PhonePrefix("456");
            var lastDigits = new LineNumber("7890");

            // Act & Assert
            Assert.Throws<DomainException>(() => PhoneNumber.Create(type, countryCode, areaCode, prefix, lastDigits));
        }
    }
}
