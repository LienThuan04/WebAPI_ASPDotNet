using LearnASPDotNet.Features.Files.Models;
using MongoDB.Driver;

namespace LearnASPDotNet.Features.Files.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly IMongoCollection<FileMetadata> _filesCollection;

        public FileRepository(IMongoDatabase database)
        {
            _filesCollection = database.GetCollection<FileMetadata>("files");

            // Tạo index cho query nhanh
            var indexKeys = Builders<FileMetadata>.IndexKeys
                .Ascending(f => f.UserId)
                .Ascending(f => f.FileType);
            _filesCollection.Indexes.CreateOne(new CreateIndexModel<FileMetadata>(indexKeys));
        }
        //CRUD FileMetadata
        public async Task<FileMetadata> CreateAsync(FileMetadata fileMetadata)
        {
            await _filesCollection.InsertOneAsync(fileMetadata);
            return fileMetadata;
        }

        public async Task<FileMetadata?> GetByIdAsync(string fileId)
        {
            var filter = Builders<FileMetadata>.Filter.And(
                Builders<FileMetadata>.Filter.Eq(f => f.Id, fileId),
                Builders<FileMetadata>.Filter.Eq(f => f.IsDeleted, false)
            );
            return await _filesCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<FileMetadata>> GetByUserIdAsync(string userId)
        {
            var filter = Builders<FileMetadata>.Filter.And(
                Builders<FileMetadata>.Filter.Eq(f => f.UserId, userId),
                Builders<FileMetadata>.Filter.Eq(f => f.IsDeleted, false)
            );
            return await _filesCollection.Find(filter)
                .SortByDescending(f => f.UploadedAt)
                .ToListAsync();
        }

        public async Task<FileMetadata?> GetByUserIdAndTypeAsync(string userId, string fileType)
        {
            var filter = Builders<FileMetadata>.Filter.And(
                Builders<FileMetadata>.Filter.Eq(f => f.UserId, userId),
                Builders<FileMetadata>.Filter.Eq(f => f.FileType, fileType),
                Builders<FileMetadata>.Filter.Eq(f => f.IsDeleted, false)
            );
            // Lấy file mới nhất nếu có nhiều
            return await _filesCollection.Find(filter)
                .SortByDescending(f => f.UploadedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteHardManyFileTypeByUserId(string userId, string fileType)
        {
            var filter = Builders<FileMetadata>.Filter.And(
                Builders<FileMetadata>.Filter.Eq(f => f.UserId, userId),
                Builders<FileMetadata>.Filter.Eq(f => f.FileType, fileType)
            );
            var delete = await _filesCollection.DeleteManyAsync(filter); // xóa nhiều file cùng loại, delete many file type with userId
            return delete.DeletedCount > 0;
        }

        public async Task<bool> DeleteAsync(string fileId)
        {
            var filter = Builders<FileMetadata>.Filter.Eq(f => f.Id, fileId);
            var update = Builders<FileMetadata>.Update
                .Set(f => f.IsDeleted, true);
            var result = await _filesCollection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}
