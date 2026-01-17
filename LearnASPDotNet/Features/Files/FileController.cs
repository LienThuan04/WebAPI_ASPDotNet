using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LearnASPDotNet.Features.Files.Services;
using LearnASPDotNet.Features.Files.Dtos;

namespace LearnASPDotNet.Features.Files
{
    [ApiController]
    [Route("api/files")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Upload file với fileType (avatar, document, image, video)
        /// </summary>
        [HttpPost("upload")]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile(
            [FromForm] FileUploadRequestDto fileUploadRequestDto,
            [FromForm] string fileType = FileTypes.Image) // default to image
        {
            try
            {
                var file = fileUploadRequestDto.File;
                if (file == null || file.Length == 0)
                {
                    return BadRequest(new { message = "No file uploaded" });
                }

                if (file.Length > 10 * 1024 * 1024) // 10MB limit
                {
                    return BadRequest(new { message = "File size cannot exceed 10MB" });
                }

                var userId = User.FindFirst("userId")?.Value!;
                var result = await _fileService.UploadFileAsync(file, userId, fileType.ToLower());

                return Ok(new
                {
                    message = "File uploaded successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy tất cả files của user (truyền userId qua query param)
        /// </summary>
        [HttpGet("user/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserFiles(string userId)
        {
            try
            {
                var files = await _fileService.GetUserFilesAsync(userId);
                return Ok(new
                {
                    message = "Files retrieved successfully",
                    count = files.Count,
                    data = files
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy file của user theo type (ví dụ: avatar)
        /// GET /api/files/user/507f1f77bcf86cd799439011/avatar
        /// </summary>
        [HttpGet("user/{userId}/{fileType}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserFileByType(string userId, string fileType)
        {
            try
            {
                var file = await _fileService.GetUserFileByTypeAsync(userId, fileType);

                if (file == null)
                {
                    return NotFound(new { message = $"No {fileType} found for user" });
                }

                return Ok(new
                {
                    message = $"{fileType} retrieved successfully",
                    data = file
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy avatar của user (shortcut)
        /// GET /api/files/avatar/507f1f77bcf86cd799439011
        /// </summary>
        [HttpGet("avatar/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserAvatar(string userId)
        {
            try
            {
                var avatar = await _fileService.GetUserFileByTypeAsync(userId, FileTypes.Avatar);

                if (avatar == null)
                {
                    return NotFound(new { message = "No avatar found" });
                }

                return Ok(new { data = avatar });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy file theo ID
        /// </summary>
        [HttpGet("{fileId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFile(string fileId)
        {
            try
            {
                var file = await _fileService.GetFileByIdAsync(fileId);

                if (file == null)
                {
                    return NotFound(new { message = "File not found" });
                }

                return Ok(new { data = file });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Xóa file (chỉ chủ sở hữu)
        /// </summary>
        [HttpDelete("{fileId}")]
        [Authorize]
        public async Task<IActionResult> DeleteFile(string fileId)
        {
            try
            {
                var userId = User.FindFirst("userId")?.Value!;
                var result = await _fileService.DeleteFileAsync(fileId, userId);

                if (result)
                {
                    return Ok(new { message = "File deleted successfully" });
                }
                else
                {
                    return NotFound(new { message = "File not found or unauthorized" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}