namespace HoaM.Domain.UnitTests
{
    public class DocumentTests
    {
        [Fact]
        public void CreateDocument_WithValidData_ShouldCreateDocument()
        {
            // Arrange
            var title = new FileName("TestDocument.txt");
            var data = new byte[] { 1, 2, 3, 4 };

            // Act
            var document = Document.Create(title, data);

            // Assert
            Assert.NotNull(document);
            Assert.Equal(title, document.Title);
            Assert.NotNull(document.Content);
            Assert.True(document.Content.Length > 0);
        }

        [Fact]
        public void CreateDocument_WithNullOrEmptyTitle_ShouldThrowDomainException()
        {
            // Arrange
            FileName title = null;
            var data = new byte[] { 1, 2, 3, 4 };

            // Act & Assert
            Assert.Throws<DomainException>(() => Document.Create(title, data));
        }

        [Fact]
        public void CreateDocument_WithNullOrEmptyData_ShouldThrowDomainException()
        {
            // Arrange
            var title = new FileName("TestDocument.txt");
            byte[] data = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => Document.Create(title, data));
        }

        [Fact]
        public void EditTitle_WithValidTitle_ShouldChangeTitle()
        {
            // Arrange
            var document = CreateTestDocument();
            var newTitle = new FileName("NewDocumentTitle.txt");

            // Act
            document.EditTitle(newTitle);

            // Assert
            Assert.Equal(newTitle, document.Title);
        }

        [Fact]
        public void EditTitle_WithNullOrEmptyTitle_ShouldThrowDomainException()
        {
            // Arrange
            var document = CreateTestDocument();
            FileName newTitle = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => document.EditTitle(newTitle));
        }

        [Fact]
        public void EditTitle_WithSameTitle_ShouldNotChangeTitle()
        {
            // Arrange
            var document = CreateTestDocument();
            var originalTitle = document.Title;

            // Act
            document.EditTitle(originalTitle);

            // Assert
            Assert.Equal(originalTitle, document.Title);
        }

        [Fact]
        public async Task ExtractUncompressedData_ShouldReturnOriginalData()
        {
            // Arrange
            var originalData = new byte[5000];

            Enumerable.Repeat<byte>(0xFF, 1000).ToArray().CopyTo(originalData, 0);
            Enumerable.Repeat<byte>(0xAA, 1000).ToArray().CopyTo(originalData, 1000);
            Enumerable.Repeat<byte>(0x1A, 1000).ToArray().CopyTo(originalData, 2000);
            Enumerable.Repeat<byte>(0xAF, 1000).ToArray().CopyTo(originalData, 3000);
            Enumerable.Repeat<byte>(0x01, 1000).ToArray().CopyTo(originalData, 4000);

            var document = Document.Create(new FileName("TestDocument.txt"), originalData);

            // Act
            var uncompressedData = await document.ExtractUncompressedDataAsync();

            // Assert
            Assert.Equal(originalData, uncompressedData);
        }

        private static Document CreateTestDocument()
        {
            var title = new FileName("TestDocument.txt");
            var data = new byte[] { 1, 2, 3, 4 };
            return Document.Create(title, data);
        }
    }
}
