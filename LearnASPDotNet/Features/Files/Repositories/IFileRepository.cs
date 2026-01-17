using LearnASPDotNet.Features.Files.Models;

namespace LearnASPDotNet.Features.Files.Repositories
{
    public interface IFileRepository
    {
        Task<FileMetadata> CreateAsync(FileMetadata fileMetadata);
        Task<FileMetadata?> GetByIdAsync(string fileId);
        Task<List<FileMetadata>> GetByUserIdAsync(string userId);
        Task<FileMetadata?> GetByUserIdAndTypeAsync(string userId, string fileType);
        Task<bool> DeleteHardManyFileTypeByUserId(string userId, string fileName);
        Task<bool> DeleteAsync(string fileId);
    }
}
