using LearnASPDotNet.Auths.Dtos;
using LearnASPDotNet.Users.Models;
using LearnASPDotNet.Users.Services;
using LearnASPDotNet.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LearnASPDotNet.Sessions.Services;
using LearnASPDotNet.Sessions.Dtos;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;
        private readonly SessionService _sessionService;
        private readonly string Refresh_Token = "refreshToken";
        public AuthController(UserService userService, JwtService jwtService, SessionService sessionService)
        {
            _userService = userService;
            _jwtService = jwtService;
            _sessionService = sessionService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
        {
            if (await this._userService.CheckExistEmailOrUsername(createUserDto.Username) || await this._userService.CheckExistEmailOrUsername(createUserDto.Email))
            {
                return Conflict("Username or Email already exists.");
            }
            var user = new User
            {
                Username = createUserDto.Username,
                Email = createUserDto.Email,
                phone = createUserDto.Phone ?? "",
                address = createUserDto.Address ?? "",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password)
            };
            
            await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(Register), new { id = user.Id }, null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var user = await _userService.GetUserByUsernameAsync(loginUserDto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }
            var payload = new JwtPayloadDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Phone = user.phone,
                Address = user.address
            };
            var token = _jwtService.GenerateToken(payload);
            var refreshToken = _jwtService.GenerateRefreshToken(user.Id);
            if (string.IsNullOrEmpty(token) || refreshToken == null)
            {
                return Unauthorized("Generate token failed");
            }
            _jwtService.SetCookedToken(HttpContext, Refresh_Token, refreshToken);
            var sessionDto = new SessionDto
            {
                UserId = user.Id,
                RefreshToken = refreshToken
            };
            await _sessionService.UpSertSessionAsync(sessionDto);
            return Ok(new
            {
                acesssToken = token
            });
        }
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies[Refresh_Token];

                if (string.IsNullOrEmpty(refreshToken))
                {
                    return Unauthorized(new { message = "Missing refresh token" });
                }
                var principal = _jwtService.ValidateRefreshToken(refreshToken);
                string userId = principal?.FindFirst("userId")?.Value ?? "";
                // 1️⃣ Tìm session trong DB
                var session = await _sessionService.GetSessionWithRefreshTokenAndUserId(refreshToken, userId); //lấy session từ DB bằng refreshToken và userId xem có hợp lệ k
                if (session == null) // neu k có thì trả về lỗi
                    return Unauthorized(new { message = "Your login session has expired." });

                if (session.ExpiresAt < DateTime.UtcNow)
                    return Unauthorized(new { message = "Refresh token expired" });

                // 2️⃣ Lấy user từ DB
                var user = await _userService.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return Unauthorized(new { message = "User not found" });
                }
                var payload = new JwtPayloadDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Phone = user.phone,
                    Address = user.address
                };

                // 3️⃣ Tạo access token mới
                var accessToken = _jwtService.GenerateToken(payload);

                // 4️⃣ (OPTIONAL) rotate refresh token
                var newRefreshToken = _jwtService.GenerateRefreshToken(
                    user.Id
                );
                var sessionDto = new SessionDto
                {
                    UserId = user.Id,
                    RefreshToken = newRefreshToken
                };
                await _sessionService.UpSertSessionAsync(sessionDto);
                Response.Cookies.Delete(Refresh_Token);
                _jwtService.SetCookedToken(HttpContext, Refresh_Token, newRefreshToken);

                return Ok(new
                {
                    accessToken
                });
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

            var userId = User.FindFirst("userId")?.Value;
            var username = User.FindFirst("userName")?.Value;
            var email = User.FindFirst("userEmail")?.Value;
            var phone = User.FindFirst("phone")?.Value;
            var address = User.FindFirst("address")?.Value;
            if (userId == null || username == null || email ==null)
            {
                return Unauthorized("You are not logged in yet");
            }
            var profile = new
            {
                Id = userId,
                Username = username,
                Email = email,
                Phone = phone,
                Address = address
            };
            return Ok(profile);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies[Refresh_Token];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest("Missing refresh token or no login session yet.");
            }
            var principal = _jwtService.ValidateRefreshToken(refreshToken);
            string userId = principal?.FindFirst("userId")?.Value ?? "";
            await _sessionService.DeleteSessionByRefreshToken(refreshToken, userId);
            Response.Cookies.Delete(Refresh_Token);
            return Ok(new { message = "Logged out successfully" });
        }
    }
}
