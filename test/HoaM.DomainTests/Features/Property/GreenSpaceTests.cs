namespace HoaM.Domain.UnitTests
{
    public class GreenSpaceTests
    {
        [Fact]
        public void Create_ValidInput_ReturnsGreenSpaceInstance()
        {
            // Arrange
            var lot1 = Lot.Create(new LotNumber("123"));
            var lot2 = Lot.Create(new LotNumber("124"));

            // Act
            var greenSpace = GreenSpace.Create(DevelopmentStatus.UnderConstruction, lot1, lot2);

            // Assert
            Assert.NotNull(greenSpace);
            Assert.Equal(DevelopmentStatus.UnderConstruction, greenSpace.Status);
            Assert.Equal(2, greenSpace.Lots.Count);
        }

        [Fact]
        public void Create_NullLots_ThrowsDomainException()
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => GreenSpace.Create(DevelopmentStatus.UnderConstruction, null));
        }

        [Fact]
        public void Create_NoLots_ThrowsDomainException()
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => GreenSpace.Create(DevelopmentStatus.UnderConstruction));
        }

        [Fact]
        public void Create_InvalidStatus_ThrowsDomainException()
        {
            // Arrange
            var lot = Lot.Create(new LotNumber("123"));

            // Act & Assert
            Assert.Throws<DomainException>(() => GreenSpace.Create((DevelopmentStatus)10, lot));
        }

        [Fact]
        public void WithTitle_ValidTitle_SetsTitle()
        {
            // Arrange
            var greenSpace = CreateGreenSpace();
            var newTitle = ParcelTitle.From("New Green Space");

            // Act
            var result = greenSpace.WithTitle(newTitle);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newTitle, greenSpace.Title);
        }

        [Fact]
        public void WithTitle_NullTitle_ThrowsDomainException()
        {
            // Arrange
            var greenSpace = CreateGreenSpace();
            ParcelTitle newTitle = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => greenSpace.WithTitle(newTitle));
        }

        [Fact]
        public void WithDescription_ValidDescription_SetsDescription()
        {
            // Arrange
            var greenSpace = CreateGreenSpace();
            var newDescription = Note.Create(new Text("A beautiful green space."));

            // Act
            var result = greenSpace.WithDescription(newDescription);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newDescription, greenSpace.Description);
        }

        [Fact]
        public void WithDescription_NullDescription_ThrowsDomainException()
        {
            // Arrange
            var greenSpace = CreateGreenSpace();
            Note newDescription = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => greenSpace.WithDescription(newDescription));
        }

        [Fact]
        public void WithOpeningHours_ValidHours_SetsOpeningHours()
        {
            // Arrange
            var greenSpace = CreateGreenSpace();
            var newOpeningHours = new OpeningHours(TimeOnly.FromTimeSpan(TimeSpan.FromHours(8)), TimeOnly.FromTimeSpan(TimeSpan.FromHours(18)));

            // Act
            var result = greenSpace.WithOpeningHours(newOpeningHours);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newOpeningHours, greenSpace.OpeningHours);
        }

        [Fact]
        public void WithOpeningHours_NullHours_ThrowsDomainException()
        {
            // Arrange
            var greenSpace = CreateGreenSpace();
            OpeningHours newOpeningHours = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => greenSpace.WithOpeningHours(newOpeningHours));
        }

        [Fact]
        public void AddRule_ValidRule_AddsRule()
        {
            // Arrange
            var greenSpace = CreateGreenSpace();
            var ruleToAdd = new Text("No loud music after 10 PM");

            // Act
            var result = greenSpace.AddRule(ruleToAdd);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(ruleToAdd, greenSpace.Rules);
        }

        [Fact]
        public void AddRule_NullRule_ThrowsDomainException()
        {
            // Arrange
            var greenSpace = CreateGreenSpace();
            Text ruleToAdd = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => greenSpace.AddRule(ruleToAdd));
        }

        [Fact]
        public void RemoveRules_HasRules_ClearsRulesList()
        {
            // Arrange
            var greenSpace = CreateGreenSpace();
            greenSpace.AddRule(new Text("No littering"));
            greenSpace.AddRule(new Text("Keep off the grass"));

            // Act
            greenSpace.RemoveRules();

            // Assert
            Assert.Empty(greenSpace.Rules);
        }

        [Fact]
        public void AddAmenity_ValidAmenity_AddsAmenity()
        {
            // Arrange
            var greenSpace = CreateGreenSpace();
            var amenityToAdd = new Text("Picnic tables");

            // Act
            var result = greenSpace.AddAmenity(amenityToAdd);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(amenityToAdd, greenSpace.Amenities);
        }

        [Fact]
        public void AddAmenity_NullAmenity_ThrowsDomainException()
        {
            // Arrange
            var greenSpace = CreateGreenSpace();
            Text amenityToAdd = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => greenSpace.AddAmenity(amenityToAdd));
        }

        [Fact]
        public void RemoveAmenities_HasAmenities_ClearsAmenitiesList()
        {
            // Arrange
            var greenSpace = CreateGreenSpace();
            greenSpace.AddAmenity(new Text("Benches"));
            greenSpace.AddAmenity(new Text("Walking paths"));

            // Act
            greenSpace.RemoveAmenities();

            // Assert
            Assert.Empty(greenSpace.Amenities);
        }

        private static GreenSpace CreateGreenSpace()
        {
            var lot = Lot.Create(LotNumber.From("123"));

            return GreenSpace.Create(DevelopmentStatus.EmptyLot, lot);
        }
    }

}