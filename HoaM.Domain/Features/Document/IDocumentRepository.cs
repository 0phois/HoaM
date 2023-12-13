namespace HoaM.Domain
{
    /// <summary>
    /// Represents a repository for managing <see cref="Document"/> entities.
    /// Inherits from <see cref="IBaseRepository{TEntity, TId}"/>.
    /// </summary>
    public interface IDocumentRepository : IBaseRepository<Document, DocumentId>
    {
    }

}
