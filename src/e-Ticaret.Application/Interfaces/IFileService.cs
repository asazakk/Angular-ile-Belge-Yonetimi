using Microsoft.AspNetCore.Http;

namespace e_Ticaret.Application.Interfaces
{
    public interface IFileService
    {
        Task<FileUploadResult> SaveFileAsync(IFormFile file, string folder);
        Task<byte[]?> GetFileAsync(string filePath);
        Task<bool> DeleteFileAsync(string filePath);
        Task<bool> FileExistsAsync(string filePath);
        string GetFileExtension(string fileName);
        bool IsValidFileType(string fileExtension, string[] allowedExtensions);
        long GetMaxFileSizeInBytes();
    }
    
    public class FileUploadResult
    {
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
