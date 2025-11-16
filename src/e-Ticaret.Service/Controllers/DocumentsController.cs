using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using e_Ticaret.Application.DTOs.Document;
using e_Ticaret.Application.Interfaces;
using e_Ticaret.Domain.Enums;

namespace e_Ticaret.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly ILogger<DocumentsController> _logger;
        
        public DocumentsController(IDocumentService documentService, ILogger<DocumentsController> logger)
        {
            _documentService = documentService;
            _logger = logger;
        }
        
        /// <summary>
        /// Tüm belgeleri getir
        /// </summary>
        /// <returns>Belge listesi</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            try
            {
                var documents = await _documentService.GetAllDocumentsAsync();
                return Ok(new
                {
                    success = true,
                    data = documents,
                    count = documents.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all documents");
                return StatusCode(500, new { message = "Belgeleri getirirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// ID'ye göre belge getir
        /// </summary>
        /// <param name="id">Belge ID'si</param>
        /// <returns>Belge detayları</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            try
            {
                var document = await _documentService.GetDocumentByIdAsync(id);
                
                if (document == null)
                {
                    return NotFound(new { message = "Belge bulunamadı." });
                }
                
                return Ok(new
                {
                    success = true,
                    data = document
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting document {DocumentId}", id);
                return StatusCode(500, new { message = "Belge getirilirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Duruma göre belgeleri getir
        /// </summary>
        /// <param name="status">Belge durumu</param>
        /// <returns>Belge listesi</returns>
        [HttpGet("by-status/{status}")]
        public async Task<IActionResult> GetDocumentsByStatus(DocumentStatus status)
        {
            try
            {
                var documents = await _documentService.GetDocumentsByStatusAsync(status);
                return Ok(new
                {
                    success = true,
                    data = documents,
                    count = documents.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting documents by status {Status}", status);
                return StatusCode(500, new { message = "Belgeler getirilirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Türe göre belgeleri getir
        /// </summary>
        /// <param name="type">Belge türü</param>
        /// <returns>Belge listesi</returns>
        [HttpGet("by-type/{type}")]
        public async Task<IActionResult> GetDocumentsByType(DocumentType type)
        {
            try
            {
                var documents = await _documentService.GetDocumentsByTypeAsync(type);
                return Ok(new
                {
                    success = true,
                    data = documents,
                    count = documents.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting documents by type {Type}", type);
                return StatusCode(500, new { message = "Belgeler getirilirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Kullanıcının belgelerini getir
        /// </summary>
        /// <returns>Kullanıcının belge listesi</returns>
        [HttpGet("my-documents")]
        public async Task<IActionResult> GetMyDocuments()
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var documents = await _documentService.GetUserDocumentsAsync(currentUserId);
                
                return Ok(new
                {
                    success = true,
                    data = documents,
                    count = documents.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user documents");
                return StatusCode(500, new { message = "Belgeleriniz getirilirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Son belgeleri getir
        /// </summary>
        /// <param name="count">Getirilen belge sayısı (varsayılan: 10)</param>
        /// <returns>Son belgeler</returns>
        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentDocuments([FromQuery] int count = 10)
        {
            try
            {
                // Count parametresini doğrula
                if (count <= 0 || count > 100)
                {
                    count = 10; // Varsayılan değer
                }
                
                _logger.LogInformation("Getting recent documents with count: {Count}", count);
                
                var documents = await _documentService.GetRecentDocumentsAsync(count);
                
                _logger.LogInformation("Found {DocumentCount} recent documents", documents.Count());
                
                return Ok(new
                {
                    success = true,
                    data = documents,
                    count = documents.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting recent documents");
                return StatusCode(500, new { 
                    success = false,
                    message = "Son belgeler getirilirken hata oluştu.",
                    error = ex.Message 
                });
            }
        }
        
        /// <summary>
        /// Belge arama
        /// </summary>
        /// <param name="searchTerm">Arama terimi</param>
        /// <returns>Arama sonuçları</returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchDocuments([FromQuery] string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return BadRequest(new { message = "Arama terimi gerekli." });
                }
                
                var documents = await _documentService.SearchDocumentsAsync(searchTerm);
                
                return Ok(new
                {
                    success = true,
                    data = documents,
                    count = documents.Count(),
                    searchTerm = searchTerm
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching documents with term: {SearchTerm}", searchTerm);
                return StatusCode(500, new { message = "Arama yapılırken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Yeni belge oluştur
        /// </summary>
        /// <param name="createDocumentDto">Belge bilgileri</param>
        /// <returns>Oluşturulan belge</returns>
        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromForm] CreateDocumentDto createDocumentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                var currentUserId = GetCurrentUserId();
                var document = await _documentService.CreateDocumentAsync(createDocumentDto, currentUserId);
                
                _logger.LogInformation("Document created successfully with ID: {DocumentId} by user: {UserId}", 
                    document.Id, currentUserId);
                
                return CreatedAtAction(nameof(GetDocumentById), new { id = document.Id }, new
                {
                    success = true,
                    message = "Belge başarıyla oluşturuldu.",
                    data = document
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating document");
                return StatusCode(500, new { message = "Belge oluşturulurken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Belge güncelle
        /// </summary>
        /// <param name="id">Belge ID'si</param>
        /// <param name="updateDocumentDto">Güncellenmiş belge bilgileri</param>
        /// <returns>Güncellenmiş belge</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromForm] UpdateDocumentDto updateDocumentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                var currentUserId = GetCurrentUserId();
                var document = await _documentService.UpdateDocumentAsync(id, updateDocumentDto, currentUserId);
                
                if (document == null)
                {
                    return NotFound(new { message = "Belge bulunamadı." });
                }
                
                _logger.LogInformation("Document updated successfully with ID: {DocumentId} by user: {UserId}", 
                    id, currentUserId);
                
                return Ok(new
                {
                    success = true,
                    message = "Belge başarıyla güncellendi.",
                    data = document
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized attempt to update document {DocumentId}", id);
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating document {DocumentId}", id);
                return StatusCode(500, new { message = "Belge güncellenirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Belge sil
        /// </summary>
        /// <param name="id">Belge ID'si</param>
        /// <returns>Silme durumu</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var result = await _documentService.DeleteDocumentAsync(id, currentUserId);
                
                if (!result)
                {
                    return NotFound(new { message = "Belge bulunamadı." });
                }
                
                _logger.LogInformation("Document deleted successfully with ID: {DocumentId} by user: {UserId}", 
                    id, currentUserId);
                
                return Ok(new
                {
                    success = true,
                    message = "Belge başarıyla silindi."
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized attempt to delete document {DocumentId}", id);
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting document {DocumentId}", id);
                return StatusCode(500, new { message = "Belge silinirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Belge durumunu değiştir
        /// </summary>
        /// <param name="id">Belge ID'si</param>
        /// <param name="request">Yeni durum bilgisi</param>
        /// <returns>İşlem durumu</returns>
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeDocumentStatus(int id, [FromBody] ChangeStatusRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                var currentUserId = GetCurrentUserId();
                var result = await _documentService.ChangeDocumentStatusAsync(id, request.NewStatus, currentUserId);
                
                if (!result)
                {
                    return NotFound(new { message = "Belge bulunamadı." });
                }
                
                _logger.LogInformation("Document status changed successfully for ID: {DocumentId} to {NewStatus} by user: {UserId}", 
                    id, request.NewStatus, currentUserId);
                
                return Ok(new
                {
                    success = true,
                    message = "Belge durumu başarıyla değiştirildi."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while changing document status for {DocumentId}", id);
                return StatusCode(500, new { message = "Belge durumu değiştirilirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Belge indir
        /// </summary>
        /// <param name="id">Belge ID'si</param>
        /// <returns>Dosya içeriği</returns>
        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadDocument(int id)
        {
            try
            {
                var fileContent = await _documentService.DownloadDocumentAsync(id);
                
                if (fileContent == null)
                {
                    return NotFound(new { message = "Dosya bulunamadı." });
                }
                
                var document = await _documentService.GetDocumentByIdAsync(id);
                var fileName = $"{document?.Title ?? "document"}{document?.FileExtension ?? ""}";
                
                _logger.LogInformation("Document downloaded with ID: {DocumentId}", id);
                
                return File(fileContent, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while downloading document {DocumentId}", id);
                return StatusCode(500, new { message = "Dosya indirilirken hata oluştu." });
            }
        }
        
        // Helper method
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }
    }
    
    public class ChangeStatusRequest
    {
        public DocumentStatus NewStatus { get; set; }
    }
}
