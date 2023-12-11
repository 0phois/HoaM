namespace HoaM.Domain.UnitTests
{
    public class AssociationMemberTests
    {
        [Fact]
        public void Create_ValidAssociationMember_ReturnsAssociationMemberInstance()
        {
            // Arrange
            var firstName = new FirstName("John");
            var lastName = new LastName("Doe");

            // Act
            var member = AssociationMember.Create(firstName, lastName);

            // Assert
            Assert.NotNull(member);
            Assert.Equal(firstName, member.FirstName);
            Assert.Equal(lastName, member.LastName);
            Assert.Empty(member.PhoneNumbers); // By default, PhoneNumbers should be empty
            Assert.Null(member.Email); // By default, Email should be null
            Assert.Null(member.Residence); // By default, Residence should be null
            Assert.Empty(member.Notifications); // By default, Notifications should be empty
            Assert.False(member.IsDeleted); // By default, IsDeleted should be false
        }

        [Fact]
        public void Create_NullFirstName_ThrowsDomainException()
        {
            // Arrange
            FirstName firstName = null;
            var lastName = new LastName("Doe");

            // Act
            var exception = Record.Exception(() => AssociationMember.Create(firstName, lastName));

            // Assert
            Assert.IsType<DomainException>(exception);
        }

        [Fact]
        public void Create_NullLastName_ThrowsDomainException()
        {
            // Arrange
            var firstName = new FirstName("John");
            LastName lastName = null;

            // Act
            var exception = Record.Exception(() => AssociationMember.Create(firstName, lastName));

            // Assert
            Assert.IsType<DomainException>(exception);
        }

        [Fact]
        public void WithEmailAddress_ValidEmailAddress_SetsEmail()
        {
            // Arrange
            var member = AssociationMember.Create(new FirstName("John"), new LastName("Doe"));
            var emailAddress = new EmailAddress("john.doe@example.com");

            // Act
            var updatedMember = member.WithEmailAddress(emailAddress);

            // Assert
            Assert.NotNull(updatedMember.Email);
            Assert.Equal(emailAddress, updatedMember.Email.Address);
        }

        [Fact]
        public void WithResidence_ValidResidence_SetsResidence()
        {
            // Arrange
            var member = AssociationMember.Create(new FirstName("John"), new LastName("Doe"));
            var residence = Residence.Create(DevelopmentStatus.UnderConstruction, Lot.Create(LotNumber.From("50")))
                                     .WithAddress<Residence>(StreetNumber.From("8"), StreetName.From("Teak Ave."));

            // Act
            var updatedMember = member.WithResidence(residence);

            // Assert
            Assert.NotNull(updatedMember.Residence);
            Assert.Equal(residence, updatedMember.Residence);
        }

        [Fact]
        public void WithPhoneNumbers_ValidPhoneNumbers_AddsPhoneNumbers()
        {
            // Arrange
            var member = AssociationMember.Create(new FirstName("John"), new LastName("Doe"));
            var phone1 = PhoneNumber.Create(PhoneType.Mobile, new CountryCallingCode("1"), new AreaCode("123"), new PhonePrefix("456"), new LineNumber("7890"));
            var phone2 = PhoneNumber.Create(PhoneType.Home, new CountryCallingCode("1"), new AreaCode("456"), new PhonePrefix("789"), new LineNumber("0123"));

            // Act
            var updatedMember = member.WithPhoneNumbers(phone1, phone2);

            // Assert
            Assert.Equal(2, updatedMember.PhoneNumbers.Count);
            Assert.Contains(phone1, updatedMember.PhoneNumbers);
            Assert.Contains(phone2, updatedMember.PhoneNumbers);
        }

        [Fact]
        public void AddPhoneNumber_ValidPhoneNumber_AddsPhoneNumber()
        {
            // Arrange
            var member = AssociationMember.Create(new FirstName("John"), new LastName("Doe"));
            var phone1 = PhoneNumber.Create(PhoneType.Mobile, new CountryCallingCode("1"), new AreaCode("123"), new PhonePrefix("456"), new LineNumber("7890"));

            // Act
            var updatedMember = member.AddPhoneNumber(phone1);

            // Assert
            Assert.Single(updatedMember.PhoneNumbers);
            Assert.Contains(phone1, updatedMember.PhoneNumbers);
        }

        [Fact]
        public void AddPhoneNumber_DuplicateType_ThrowsDomainException()
        {
            // Arrange
            var member = AssociationMember.Create(new FirstName("John"), new LastName("Doe"));
            var phone1 = PhoneNumber.Create(PhoneType.Mobile, new CountryCallingCode("1"), new AreaCode("123"), new PhonePrefix("456"), new LineNumber("7890"));
            var phone2 = PhoneNumber.Create(PhoneType.Mobile, new CountryCallingCode("1"), new AreaCode("456"), new PhonePrefix("789"), new LineNumber("0123"));

            // Act
            member.AddPhoneNumber(phone1);

            // Assert
            Assert.Throws<DomainException>(() => member.AddPhoneNumber(phone2));
        }

        [Fact]
        public void RemovePhoneNumber_ValidPhoneNumber_RemovesPhoneNumber()
        {
            // Arrange
            var member = AssociationMember.Create(new FirstName("John"), new LastName("Doe"));
            var phone1 = PhoneNumber.Create(PhoneType.Mobile, new CountryCallingCode("1"), new AreaCode("123"), new PhonePrefix("456"), new LineNumber("7890"));
            member.AddPhoneNumber(phone1);

            // Act
            member.RemovePhoneNumber(phone1);

            // Assert
            Assert.Empty(member.PhoneNumbers);
        }

        [Fact]
        public void EditFirstName_ValidName_SetsFirstName()
        {
            // Arrange
            var member = AssociationMember.Create(new FirstName("John"), new LastName("Doe"));
            var newName = new FirstName("Jane");

            // Act
            member.EditFirstName(newName);

            // Assert
            Assert.Equal(newName, member.FirstName);
        }

        [Fact]
        public void EditLastName_ValidSurname_SetsLastName()
        {
            // Arrange
            var member = AssociationMember.Create(new FirstName("John"), new LastName("Doe"));
            var newSurname = new LastName("Smith");

            // Act
            member.EditLastName(newSurname);

            // Assert
            Assert.Equal(newSurname, member.LastName);
        }

        [Fact]
        public void RemoveResidence_SetsResidenceToNull()
        {
            // Arrange
            var member = AssociationMember.Create(new FirstName("John"), new LastName("Doe"));
            var residence = Residence.Create(DevelopmentStatus.UnderConstruction, Lot.Create(LotNumber.From("183")))
                                     .WithAddress<Residence>(StreetNumber.From("100"), StreetName.From("Mahagony Circular"));

            member.WithResidence(residence);

            // Act
            member.RemoveResidence();

            // Assert
            Assert.Null(member.Residence);
        }
    }
}
