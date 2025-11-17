using e_Ticaret.Application.DTOs.Document;
using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Application.Interfaces
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentListDto>> GetAllDocumentsAsync();
        Task<DocumentDto?> GetDocumentByIdAsync(int id);
        Task<IEnumerable<DocumentListDto>> GetDocumentsByStatusAsync(DocumentStatus status);
        Task<IEnumerable<DocumentListDto>> GetDocumentsByTypeAsync(DocumentType documentType);
        Task<IEnumerable<DocumentListDto>> GetUserDocumentsAsync(int userId);
        Task<IEnumerable<DocumentListDto>> GetRecentDocumentsAsync(int count = 10);
        Task<IEnumerable<DocumentListDto>> SearchDocumentsAsync(string searchTerm);
        Task<DocumentDto> CreateDocumentAsync(CreateDocumentDto createDocumentDto, int currentUserId);
        Task<DocumentDto?> UpdateDocumentAsync(int id, UpdateDocumentDto updateDocumentDto, int currentUserId);
        Task<bool> DeleteDocumentAsync(int id, int currentUserId);
        Task<bool> ChangeDocumentStatusAsync(int id, DocumentStatus newStatus, int currentUserId);
        Task<byte[]?> DownloadDocumentAsync(int id);
    }
}
