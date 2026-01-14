using LearnASPDotNet.Features.Sessions.Dtos;
using LearnASPDotNet.Features.Sessions.Models;

namespace LearnASPDotNet.Features.Sessions
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository _sessionRepository)
        {
            this._sessionRepository = _sessionRepository;
        }

        //todo: CRUD session  
        public async Task<Session?> UpSertSessionAsync(CreateSessionDto createSessionDto)
        {
            var refreshExpiryConfig = Environment.GetEnvironmentVariable("JWT_REFRESH_EXPIRE_DAYS");
            //var refreshExpiryConfig = 1.ToString(); // test expire one minute
            if (string.IsNullOrEmpty(refreshExpiryConfig))
            {
                throw new Exception("JWT_REFRESH_EXPIRE_DAYS is not configured");
            }
            var session = new Session
            {
                UserId = createSessionDto.UserId,
                RefreshToken = createSessionDto.RefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(
                    int.Parse(refreshExpiryConfig)
                ) // Set expiration date for 7 days  
            };
            var updatedSession = await _sessionRepository.UpSertSessionAsync(session);
            if (updatedSession == null)
            {
                throw new Exception("Create session failed");
            }
            return updatedSession;
        }

        public async Task<Session?> GetSessionWithRefreshTokenAndUserId(SessionRequestDto sessionRequestDto)
        {
            var result = await _sessionRepository.GetSessionWithRefreshTokenAndUserIdAsync(sessionRequestDto);
            return result;
        }

        public async Task<Session?> UpdateSession(SessionRequestDto sessionRequestDto)
        {
            DateTime ExpiresAt = DateTime.UtcNow.AddDays(
                    int.Parse(Environment.GetEnvironmentVariable("JWT_REFRESH_EXPIRE_DAYS")!)
                );
            var result = await _sessionRepository.UpdateSessionAsync(sessionRequestDto, ExpiresAt);
            return result;
        }

        public async Task<bool> DeleteSessionByRefreshToken(SessionRequestDto sessionRequestDto)
        {
            var result = await _sessionRepository.DeleteSessionAsync(sessionRequestDto);
            return result != null; //if result is null return false
        }
    }
}

