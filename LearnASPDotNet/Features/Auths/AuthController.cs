using LearnASPDotNet.Features.Auths.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LearnASPDotNet.Features.Users;
using LearnASPDotNet.Features.Sessions;
using LearnASPDotNet.Features.Sessions.Dtos;

namespace LearnASPDotNet.Features.Auths
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;
        private readonly ISessionService _sessionService;
        private readonly string _refreshToken = "refreshToken";
        public AuthController(IAuthService authService, JwtService jwtService, ISessionService sessionService, IUserService userService)
        {
            _userService = userService;
            _authService = authService;
            _jwtService = jwtService;
            _sessionService = sessionService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if(await _userService.CheckExistEmailOrUsername(request.Email) || await _userService.CheckExistEmailOrUsername(request.Username))
            {
                return BadRequest("Email or UserName is already in use by another user");
            }
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);
            _jwtService.SetCookedToken(HttpContext, _refreshToken, result.RefreshToken);
            var sessionDto = new CreateSessionDto
            {
                UserId = result.User.UserId,
                RefreshToken = result.RefreshToken
            };
            //  lưu refresh token vào DB table Sessions
            await _sessionService.UpSertSessionAsync(sessionDto);
            return Ok(new
            {
                acesssToken = result.AccessToken,
                User = result.User
            });
        }


        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies[_refreshToken];
                if (string.IsNullOrEmpty(refreshToken))
                {
                    return Unauthorized("Missing refresh token");
                }
                var result = await _authService.RefreshTokenAsync(refreshToken);
                // Cập nhật cookie refresh token
                Response.Cookies.Delete(_refreshToken);
                _jwtService.SetCookedToken(HttpContext, _refreshToken, result.RefreshToken);
                var sessionDto = new CreateSessionDto
                {
                    UserId = result.User.UserId,
                    RefreshToken = result.RefreshToken
                };
                // Cập nhật session trong DB
                await _sessionService.UpSertSessionAsync(sessionDto);
                return Ok(new {result.AccessToken, result.User});
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }   

        [HttpGet("profile")]
        [Authorize]
        public IActionResult GetProfile()
        {
            var profile = HttpContext.GetCurrentUser();
            if (profile == null)
            {
                return Unauthorized("User not found or User no login");
            }
            return Ok(profile);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies[_refreshToken];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest("Missing refresh token or no login session yet.");
            }
            var result = await _authService.LogoutAsync(refreshToken);
            Response.Cookies.Delete(_refreshToken);
            SessionRequestDto sessionRequestDto = new SessionRequestDto
            {
                RefreshToken = refreshToken,
                UserId = result.UserId
            };
            var resultDelete = await _sessionService.DeleteSessionByRefreshToken(sessionRequestDto);
            if (!resultDelete)
            {
                return BadRequest("Logout failed. Session not found.");
            }
            return Ok(new { result.Message});
        }
    }
}
