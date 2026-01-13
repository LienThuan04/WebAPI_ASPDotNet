using LearnASPDotNet.Features.Auths.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LearnASPDotNet.Sessions.Services;
using LearnASPDotNet.Sessions.Dtos;

namespace LearnASPDotNet.Features.Auths
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly JwtService _jwtService;
        private readonly SessionService _sessionService;
        private readonly string _refreshToken = "refreshToken";
        public AuthController(IAuthService authService, JwtService jwtService, SessionService sessionService)
        {
            _authService = authService;
            _jwtService = jwtService;
            _sessionService = sessionService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            _jwtService.SetCookedToken(HttpContext, _refreshToken, result.RefreshToken);
            var sessionDto = new SessionDto
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
                var sessionDto = new SessionDto
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
            //var userId = User.FindFirst("userId")?.Value;
            //var username = User.FindFirst("userName")?.Value;
            //var email = User.FindFirst("userEmail")?.Value;
            //var phone = User.FindFirst("phone")?.Value;
            //var address = User.FindFirst("address")?.Value;
            //if (userId == null || username == null || email == null)
            //{
            //    return Unauthorized("You are not logged in yet");
            //}
            //var profile = new JwtPayloadDto
            //{
            //    UserId = userId,
            //    Username = username,
            //    Email = email,
            //    Phone = phone,
            //    Address = address
            //};
            //return Ok(profile);
            var profile = HttpContext.GetCurrentUser();
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
            await _sessionService.DeleteSessionByRefreshToken(refreshToken, result.UserId);
            return Ok(new { result.Message});
        }
    }
}
