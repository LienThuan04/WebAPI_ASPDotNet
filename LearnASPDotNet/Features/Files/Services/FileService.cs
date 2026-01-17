using Supabase;
using LearnASPDotNet.Features.Files.Dtos;
using LearnASPDotNet.Features.Files.Models;
using LearnASPDotNet.Features.Files.Repositories;

namespace LearnASPDotNet.Features.Files.Services
{
    public class FileService : IFileService
    {
        private readonly Client _supabase;
        private readonly IFileRepository _fileRepository;
        private readonly string _bucketName;

        public FileService(Client supabase, IFileRepository fileRepository)
        {
            _supabase = supabase;
            _fileRepository = fileRepository;
            _bucketName = Environment.GetEnvironmentVariable("SUPABASE_BUCKET_NAME")!;
        }

        public async Task<FileUploadResponseDto> UploadFileAsync(IFormFile file, string userId, string fileType)
        {
            // 1. Generate unique filename
            var fileExtension = Path.GetExtension(file.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";// garantee unique filename
            if (fileType == FileTypes.Avatar) // overwrite avatar if is avatar
            {
                uniqueFileName = $"avatar{fileExtension}"; // overwrite avatar
                var result = await _fileRepository.DeleteHardManyFileTypeByUserId(userId, FileTypes.Avatar);
                //if (!result)
                //{
                //    throw new Exception("Failed to delete existing avatar files");
                //}
                await DeleteAllFileInFolderAsync($"{userId}/{FileTypes.Avatar}");
            }
            var filePath = $"{userId}/{fileType}/{uniqueFileName}"; // userId/fileType/uniqueFileName

            // 2. Upload to Supabase
            using var memoryStream = new MemoryStream();// create memory stream
            await file.CopyToAsync(memoryStream); // copy file to memory stream
            var fileBytes = memoryStream.ToArray(); // convert to byte array

            await _supabase.Storage // upload file to supabase
                .From(_bucketName) // get bucket
                .Upload(fileBytes, filePath, new Supabase.Storage.FileOptions
                {
                    ContentType = file.ContentType,
                    Upsert = fileType == FileTypes.Avatar // overwrite if is avatar
                });

            // 3. Get public URL
            var publicUrl = _supabase.Storage
                .From(_bucketName)
                .GetPublicUrl(filePath);

            // 4. Save to MongoDB
            var fileMetadata = new FileMetadata
            {
                UserId = userId,
                FileName = file.FileName,
                FilePath = filePath,
                FileUrl = publicUrl,
                FileType = fileType,
                ContentType = file.ContentType,
                FileSize = file.Length,
                UploadedAt = DateTime.UtcNow
            }; 

            var savedFile = await _fileRepository.CreateAsync(fileMetadata);

            return new FileUploadResponseDto
            {
                FileId = savedFile.Id,
                FileName = savedFile.FileName,
                FileUrl = savedFile.FileUrl,
                FileType = savedFile.FileType,
                FileSize = savedFile.FileSize,
                ContentType = savedFile.ContentType,
                UploadedAt = savedFile.UploadedAt
            };
        }

        public async Task<FileMetadata?> GetFileByIdAsync(string fileId)
        {
            return await _fileRepository.GetByIdAsync(fileId);
        }

        public async Task<List<FileMetadata>> GetUserFilesAsync(string userId)
        {
            return await _fileRepository.GetByUserIdAsync(userId);
        }

        public async Task<FileMetadata?> GetUserFileByTypeAsync(string userId, string fileType)
        {
            return await _fileRepository.GetByUserIdAndTypeAsync(userId, fileType);
        }

        public async Task DeleteAllFileInFolderAsync(string folderPath)
        {
            try
            {
                var listResponse = await _supabase.Storage
                    .From(_bucketName)
                    .List(folderPath);

                if (listResponse != null && listResponse.Count > 0)
                {
                    var filePaths = listResponse.Select(file => $"{folderPath}/{file.Name}").ToList(); // select full file paths in the folder
                    await _supabase.Storage
                        .From(_bucketName)
                        .Remove(filePaths);
                    Console.WriteLine($"Deleted {filePaths.Count} files in folder {folderPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting files in folder {folderPath}: {ex.Message}");
            }
        }

        public async Task<bool> DeleteFileAsync(string fileId, string userId)
        {
            var file = await _fileRepository.GetByIdAsync(fileId);
            if (file == null || file.UserId != userId)
            {
                return false;
            }

            // Delete from Supabase
            try
            {
                await _supabase.Storage
                    .From(_bucketName)
                    .Remove(new List<string> { file.FilePath });
            }
            catch
            {
                // Log but continue
            }

            // Soft delete from MongoDB
            return await _fileRepository.DeleteAsync(fileId);
        }
    }
}