namespace HoaM.Domain.UnitTests
{
    public class NoteTests
    {
        [Fact]
        public void Create_NoteWithValidText_ShouldSucceed()
        {
            // Arrange
            var content = new Text("This is a test note...");

            // Act
            var note = Note.Create(content);

            // Assert
            Assert.NotNull(note);
            Assert.NotEmpty(note.Id.ToString());
            Assert.Equal(content, note.Content);
        }

        [Fact]
        public void Create_NoteWithNullContent_ShouldThrowException()
        {
            // Arrange
#pragma warning disable CS8600 
            Text content = null;
#pragma warning restore CS8600 

            // Act & Assert
#pragma warning disable CS8604 
            Assert.Throws<DomainException>(() => Note.Create(content));
#pragma warning restore CS8604 
        }

        [Fact]
        public void EditContent_NewContentIsValid_ShouldChangeNoteContent()
        {
            // Arrange
            var note = Note.Create(new Text("original note"));
            var newText = new Text("Updated text");

            // Act
            note.EditContent(newText);

            // Assert
            Assert.Equal(newText, note.Content);
        }

        [Fact]
        public void EditContent_NewContentIsNull_ShouldThrowException()
        {
            // Arrange
            var note = Note.Create(new Text("This is a note"));
#pragma warning disable CS8600
            Text newContent = null;
#pragma warning restore CS8600

            // Act & Assert
#pragma warning disable CS8604
            Assert.Throws<DomainException>(() => note.EditContent(newContent));
#pragma warning restore CS8604
        }
    }
}
