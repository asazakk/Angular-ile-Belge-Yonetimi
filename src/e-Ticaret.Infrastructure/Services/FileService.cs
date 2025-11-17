using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using e_Ticaret.Application.Interfaces;

namespace e_Ticaret.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        private readonly string _uploadPath;
        
        private readonly string[] _allowedExtensions = { ".pdf", ".doc", ".docx", ".txt", ".jpg", ".jpeg", ".png" };
        private readonly long _maxFileSizeInBytes = 10 * 1024 * 1024; // 10MB
        
        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
            _uploadPath = _configuration["FileUpload:UploadPath"] ?? Path.Combine("wwwroot", "uploads");
            
            // Upload klasörü yoksa oluştur
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }
        
        public async Task<FileUploadResult> SaveFileAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
            {
                return new FileUploadResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Dosya seçilmedi."
                };
            }
            
            // Dosya boyutu kontrolü
            if (file.Length > _maxFileSizeInBytes)
            {
                return new FileUploadResult
                {
                    IsSuccess = false,
                    ErrorMessage = $"Dosya boyutu {FormatFileSize(_maxFileSizeInBytes)} değerini aşamaz."
                };
            }
            
            var fileExtension = GetFileExtension(file.FileName);
            
            // Dosya türü kontrolü
            if (!IsValidFileType(fileExtension, _allowedExtensions))
            {
                return new FileUploadResult
                {
                    IsSuccess = false,
                    ErrorMessage = $"Geçersiz dosya türü. İzin verilen türler: {string.Join(", ", _allowedExtensions)}"
                };
            }
            
            try
            {
                var folderPath = Path.Combine(_uploadPath, folder);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                
                // Benzersiz dosya adı oluştur
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(folderPath, fileName);
                
                // Dosyayı kaydet
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                
                return new FileUploadResult
                {
                    IsSuccess = true,
                    FilePath = Path.Combine(folder, fileName).Replace('\\', '/'),
                    FileName = fileName,
                    FileExtension = fileExtension,
                    FileSize = file.Length
                };
            }
            catch (Exception ex)
            {
                return new FileUploadResult
                {
                    IsSuccess = false,
                    ErrorMessage = $"Dosya kaydedilemedi: {ex.Message}"
                };
            }
        }
        
        public async Task<byte[]?> GetFileAsync(string filePath)
        {
            try
            {
                var fullPath = Path.Combine(_uploadPath, filePath.Replace('/', '\\'));
                
                if (!File.Exists(fullPath))
                {
                    return null;
                }
                
                return await File.ReadAllBytesAsync(fullPath);
            }
            catch
            {
                return null;
            }
        }
        
        public Task<bool> DeleteFileAsync(string filePath)
        {
            try
            {
                var fullPath = Path.Combine(_uploadPath, filePath.Replace('/', '\\'));
                
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return Task.FromResult(true);
                }
                
                return Task.FromResult(false);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
        
        public Task<bool> FileExistsAsync(string filePath)
        {
            try
            {
                var fullPath = Path.Combine(_uploadPath, filePath.Replace('/', '\\'));
                return Task.FromResult(File.Exists(fullPath));
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
        
        public string GetFileExtension(string fileName)
        {
            return Path.GetExtension(fileName).ToLowerInvariant();
        }
        
        public bool IsValidFileType(string fileExtension, string[] allowedExtensions)
        {
            return allowedExtensions.Contains(fileExtension.ToLowerInvariant());
        }
        
        public long GetMaxFileSizeInBytes()
        {
            return _maxFileSizeInBytes;
        }
        
        private static string FormatFileSize(long fileSizeInBytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            double len = fileSizeInBytes;
            
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            
            return $"{len:0.##} {sizes[order]}";
        }
    }
}
