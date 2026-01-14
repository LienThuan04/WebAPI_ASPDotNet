using LearnASPDotNet.Features.Sessions.Dtos;
using LearnASPDotNet.Features.Sessions.Models;

namespace LearnASPDotNet.Features.Sessions
{
    public interface ISessionRepository
    {
        public Task<Session?> UpSertSessionAsync(Session session);
        public Task<Session?> GetSessionWithRefreshTokenAndUserIdAsync(SessionRequestDto sessionRequestDto);
        public Task<Session?> UpdateSessionAsync(SessionRequestDto sessionRequestDto, DateTime newExpiresAt);
        public Task<Session?> DeleteSessionAsync(SessionRequestDto sessionRequestDto);
    }
}
