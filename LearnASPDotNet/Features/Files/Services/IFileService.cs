using LearnASPDotNet.Features.Files.Dtos;
using LearnASPDotNet.Features.Files.Models;
namespace LearnASPDotNet.Features.Files.Services
{
    public interface IFileService
    {
        Task<FileUploadResponseDto> UploadFileAsync(IFormFile file, string userId, string fileType);
        Task<FileMetadata?> GetFileByIdAsync(string fileId);
        Task<List<FileMetadata>> GetUserFilesAsync(string userId);
        Task<FileMetadata?> GetUserFileByTypeAsync(string userId, string fileType);
        Task DeleteAllFileInFolderAsync(string folderPath);
        Task<bool> DeleteFileAsync(string fileId, string userId);
    }
}