using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LearnASPDotNet.Users.Services;

namespace LearnASPDotNet.Users.Controllers
{
    [ApiController]
    [Route("user")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            this._userService = userService;

        }
        [HttpGet()]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var listUser = await _userService.GetAllUserAsync();
                if (listUser == null || !listUser.Any())
                {
                    return NotFound("No users found");
                }
                return Ok(listUser);

            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
