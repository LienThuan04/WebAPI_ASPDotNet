namespace LearnASPDotNet.Features.Files.Dtos
{
    public class FileUploadResponseDto
    {
        public string FileId { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = null!;
        public DateTime UploadedAt { get; set; }
    }
}