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
            Text content = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => Note.Create(content));
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
            Text newContent = null;

            // Act & Assert
            Assert.Throws<DomainException>(() => note.EditContent(newContent));
        }
    }
}
