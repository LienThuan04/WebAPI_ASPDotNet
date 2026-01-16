using LearnASPDotNet.Features.Sessions.Dtos;
using LearnASPDotNet.Features.Sessions.Models;

namespace LearnASPDotNet.Features.Sessions.Services
{
    public interface ISessionService
    {
        public Task<Session?> UpSertSessionAsync(CreateSessionDto createSessionDto);
        public Task<Session?> GetSessionWithRefreshTokenAndUserId(SessionRequestDto sessionRequestDto);
        public Task<Session?> UpdateSession(SessionRequestDto sessionRequestDto);
        public Task<bool> DeleteSessionByRefreshToken(SessionRequestDto sessionRequestDto);
    }
}
