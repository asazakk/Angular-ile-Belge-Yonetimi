using Microsoft.AspNetCore.Http;
using e_Ticaret.Application.DTOs.Document;
using e_Ticaret.Application.Interfaces;
using e_Ticaret.Domain.Entities;
using e_Ticaret.Domain.Enums;
using e_Ticaret.Domain.Interfaces;

namespace e_Ticaret.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        
        public DocumentService(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        
        public async Task<IEnumerable<DocumentListDto>> GetAllDocumentsAsync()
        {
            var documents = await _unitOfWork.Documents.GetAllAsync();
            return documents.Select(MapToDocumentListDto);
        }
        
        public async Task<DocumentDto?> GetDocumentByIdAsync(int id)
        {
            var document = await _unitOfWork.Documents.GetByIdAsync(id);
            return document != null ? MapToDocumentDto(document) : null;
        }
        
        public async Task<IEnumerable<DocumentListDto>> GetDocumentsByStatusAsync(DocumentStatus status)
        {
            var documents = await _unitOfWork.Documents.GetByStatusAsync(status);
            return documents.Select(MapToDocumentListDto);
        }
        
        public async Task<IEnumerable<DocumentListDto>> GetDocumentsByTypeAsync(DocumentType documentType)
        {
            var documents = await _unitOfWork.Documents.GetByTypeAsync(documentType);
            return documents.Select(MapToDocumentListDto);
        }
        
        public async Task<IEnumerable<DocumentListDto>> GetUserDocumentsAsync(int userId)
        {
            var documents = await _unitOfWork.Documents.GetByUserIdAsync(userId);
            return documents.Select(MapToDocumentListDto);
        }
        
        public async Task<IEnumerable<DocumentListDto>> GetRecentDocumentsAsync(int count = 10)
        {
            var documents = await _unitOfWork.Documents.GetRecentDocumentsAsync(count);
            return documents.Select(MapToDocumentListDto);
        }
        
        public async Task<IEnumerable<DocumentListDto>> SearchDocumentsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllDocumentsAsync();
            }
            
            var documents = await _unitOfWork.Documents.SearchDocumentsAsync(searchTerm);
            return documents.Select(MapToDocumentListDto);
        }
        
        public async Task<DocumentDto> CreateDocumentAsync(CreateDocumentDto createDocumentDto, int currentUserId)
        {
            var document = new Document
            {
                Title = createDocumentDto.Title,
                Description = createDocumentDto.Description,
                DocumentType = createDocumentDto.DocumentType,
                Status = DocumentStatus.Draft,
                CreatedByUserId = currentUserId,
                CreatedBy = currentUserId.ToString()
            };
            
            // Dosya varsa kaydet
            if (createDocumentDto.File != null && createDocumentDto.File.Length > 0)
            {
                var fileResult = await _fileService.SaveFileAsync(createDocumentDto.File, "documents");
                document.FilePath = fileResult.FilePath;
                document.FileExtension = fileResult.FileExtension;
                document.FileSize = fileResult.FileSize;
            }
            
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.Documents.AddAsync(document);
                await _unitOfWork.SaveChangesAsync();
                
                // DocumentAction kaydı oluştur
                var documentAction = new DocumentAction
                {
                    DocumentId = document.Id,
                    UserId = currentUserId,
                    ActionType = ActionType.Created,
                    ActionDate = DateTime.UtcNow
                };
                
                await _unitOfWork.Repository<DocumentAction>().AddAsync(documentAction);
                await _unitOfWork.SaveChangesAsync();
                
                await _unitOfWork.CommitTransactionAsync();
                
                // Tam veriyi getir
                var savedDocument = await _unitOfWork.Documents.GetByIdAsync(document.Id);
                return MapToDocumentDto(savedDocument!);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        
        public async Task<DocumentDto?> UpdateDocumentAsync(int id, UpdateDocumentDto updateDocumentDto, int currentUserId)
        {
            var document = await _unitOfWork.Documents.GetByIdAsync(id);
            if (document == null)
            {
                return null;
            }
            
            // Güncelleme yetki kontrolü (sadece oluşturan veya admin güncelleyebilir)
            if (document.CreatedByUserId != currentUserId)
            {
                // Burada admin kontrolü de yapılabilir
                throw new UnauthorizedAccessException("Bu belgeyi güncelleme yetkiniz yok.");
            }
            
            var oldStatus = document.Status;
            
            // Bilgileri güncelle
            document.Title = updateDocumentDto.Title;
            document.Description = updateDocumentDto.Description;
            document.DocumentType = updateDocumentDto.DocumentType;
            document.Status = updateDocumentDto.Status;
            document.UpdatedByUserId = currentUserId;
            document.UpdatedBy = currentUserId.ToString();
            
            // Yeni dosya varsa kaydet
            if (updateDocumentDto.File != null && updateDocumentDto.File.Length > 0)
            {
                // Eski dosyayı sil
                if (!string.IsNullOrEmpty(document.FilePath))
                {
                    await _fileService.DeleteFileAsync(document.FilePath);
                }
                
                var fileResult = await _fileService.SaveFileAsync(updateDocumentDto.File, "documents");
                document.FilePath = fileResult.FilePath;
                document.FileExtension = fileResult.FileExtension;
                document.FileSize = fileResult.FileSize;
            }
            
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.Documents.UpdateAsync(document);
                await _unitOfWork.SaveChangesAsync();
                
                // DocumentAction kaydı oluştur
                var actionType = oldStatus != document.Status ? GetActionTypeFromStatus(document.Status) : ActionType.Updated;
                
                var documentAction = new DocumentAction
                {
                    DocumentId = document.Id,
                    UserId = currentUserId,
                    ActionType = actionType,
                    ActionDate = DateTime.UtcNow,
                    Notes = oldStatus != document.Status ? $"Status changed from {oldStatus} to {document.Status}" : null
                };
                
                await _unitOfWork.Repository<DocumentAction>().AddAsync(documentAction);
                await _unitOfWork.SaveChangesAsync();
                
                await _unitOfWork.CommitTransactionAsync();
                
                // Güncellenmiş veriyi getir
                var updatedDocument = await _unitOfWork.Documents.GetByIdAsync(document.Id);
                return MapToDocumentDto(updatedDocument!);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        
        public async Task<bool> DeleteDocumentAsync(int id, int currentUserId)
        {
            var document = await _unitOfWork.Documents.GetByIdAsync(id);
            if (document == null)
            {
                return false;
            }
            
            // Silme yetki kontrolü
            if (document.CreatedByUserId != currentUserId)
            {
                throw new UnauthorizedAccessException("Bu belgeyi silme yetkiniz yok.");
            }
            
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Soft delete
                document.IsDeleted = true;
                document.UpdatedByUserId = currentUserId;
                document.UpdatedBy = currentUserId.ToString();
                
                await _unitOfWork.Documents.UpdateAsync(document);
                await _unitOfWork.SaveChangesAsync();
                
                // DocumentAction kaydı oluştur
                var documentAction = new DocumentAction
                {
                    DocumentId = document.Id,
                    UserId = currentUserId,
                    ActionType = ActionType.Archived, // Deleted yerine Archived kullanıyoruz
                    ActionDate = DateTime.UtcNow
                };
                
                await _unitOfWork.Repository<DocumentAction>().AddAsync(documentAction);
                await _unitOfWork.SaveChangesAsync();
                
                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        
        public async Task<bool> ChangeDocumentStatusAsync(int id, DocumentStatus newStatus, int currentUserId)
        {
            var document = await _unitOfWork.Documents.GetByIdAsync(id);
            if (document == null)
            {
                return false;
            }
            
            var oldStatus = document.Status;
            document.Status = newStatus;
            document.UpdatedByUserId = currentUserId;
            document.UpdatedBy = currentUserId.ToString();
            
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.Documents.UpdateAsync(document);
                await _unitOfWork.SaveChangesAsync();
                
                // DocumentAction kaydı oluştur
                var documentAction = new DocumentAction
                {
                    DocumentId = document.Id,
                    UserId = currentUserId,
                    ActionType = GetActionTypeFromStatus(newStatus),
                    ActionDate = DateTime.UtcNow,
                    Notes = $"Status changed from {oldStatus} to {newStatus}"
                };
                
                await _unitOfWork.Repository<DocumentAction>().AddAsync(documentAction);
                await _unitOfWork.SaveChangesAsync();
                
                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        
        public async Task<byte[]?> DownloadDocumentAsync(int id)
        {
            var document = await _unitOfWork.Documents.GetByIdAsync(id);
            if (document == null || string.IsNullOrEmpty(document.FilePath))
            {
                return null;
            }
            
            return await _fileService.GetFileAsync(document.FilePath);
        }
        
        // Helper methods
        private static DocumentDto MapToDocumentDto(Document document)
        {
            return new DocumentDto
            {
                Id = document.Id,
                Title = document.Title,
                Description = document.Description,
                DocumentType = document.DocumentType,
                DocumentTypeName = GetDocumentTypeName(document.DocumentType),
                Status = document.Status,
                StatusName = GetStatusName(document.Status),
                FilePath = document.FilePath,
                FileExtension = document.FileExtension,
                FileSize = document.FileSize,
                FileSizeFormatted = FormatFileSize(document.FileSize),
                CreatedAt = document.CreatedAt,
                UpdatedAt = document.UpdatedAt,
                CreatedByUserName = $"{document.CreatedByUser.FirstName} {document.CreatedByUser.LastName}",
                UpdatedByUserName = document.UpdatedByUser != null ? $"{document.UpdatedByUser.FirstName} {document.UpdatedByUser.LastName}" : null
            };
        }
        
        private static DocumentListDto MapToDocumentListDto(Document document)
        {
            return new DocumentListDto
            {
                Id = document.Id,
                Title = document.Title,
                DocumentType = document.DocumentType,
                DocumentTypeName = GetDocumentTypeName(document.DocumentType),
                Status = document.Status,
                StatusName = GetStatusName(document.Status),
                CreatedAt = document.CreatedAt,
                CreatedByUserName = $"{document.CreatedByUser.FirstName} {document.CreatedByUser.LastName}",
                FileSizeFormatted = FormatFileSize(document.FileSize)
            };
        }
        
        private static string GetDocumentTypeName(DocumentType documentType)
        {
            return documentType switch
            {
                DocumentType.Karar => "Karar",
                DocumentType.Duyuru => "Duyuru",
                DocumentType.Genelge => "Genelge",
                DocumentType.Rapor => "Rapor",
                DocumentType.Dilekce => "Dilekçe",
                DocumentType.Evrak => "Evrak",
                DocumentType.Diğer => "Diğer",
                _ => "Bilinmiyor"
            };
        }
        
        private static string GetStatusName(DocumentStatus status)
        {
            return status switch
            {
                DocumentStatus.Draft => "Taslak",
                DocumentStatus.Pending => "Beklemede",
                DocumentStatus.Approved => "Onaylandı",
                DocumentStatus.Rejected => "Reddedildi",
                DocumentStatus.Published => "Yayınlandı",
                DocumentStatus.Archived => "Arşivlendi",
                _ => "Bilinmiyor"
            };
        }
        
        private static ActionType GetActionTypeFromStatus(DocumentStatus status)
        {
            return status switch
            {
                DocumentStatus.Approved => ActionType.Approved,
                DocumentStatus.Rejected => ActionType.Rejected,
                DocumentStatus.Published => ActionType.Published,
                DocumentStatus.Archived => ActionType.Archived,
                _ => ActionType.Updated
            };
        }
        
        private static string? FormatFileSize(long? fileSizeInBytes)
        {
            if (!fileSizeInBytes.HasValue) return null;
            
            var size = fileSizeInBytes.Value;
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            double len = size;
            
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            
            return $"{len:0.##} {sizes[order]}";
        }
    }
}
