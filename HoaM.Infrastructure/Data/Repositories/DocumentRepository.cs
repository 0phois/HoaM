using HoaM.Domain;
using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data
{
    internal sealed class DocumentRepository(DbContext dbContext) : GenericRepository<Document, DocumentId>(dbContext), IDocumentRepository
    {
    }
}
