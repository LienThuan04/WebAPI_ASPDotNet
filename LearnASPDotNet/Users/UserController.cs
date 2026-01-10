using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LearnASPDotNet.Users.Services;
using LearnASPDotNet.Users.Dtos;

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

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto updateUser)
        {
            try
            {
                var userExists = await _userService.GetUserByIdAsync(id);
                if (userExists == null)
                {
                    return NotFound("User not found");
                }
                var UpdatedUser = await _userService.UpdateUserAsync(id, updateUser);
                if (UpdatedUser == null)
                {
                    return BadRequest("Failed to update user");
                }
                // Implement update logic here
                return Ok(new
                {
                    message = "User updated successfully",
                    user = UpdatedUser
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var userExists = await _userService.GetUserByIdAsync(id);
                if (userExists == null)
                {
                    return NotFound("User not found");
                }
                var data = await _userService.DeleteUserAsync(id);
                if (data == null)
                {
                    throw new Exception("Failed to delete user");
                }
                return Ok(new
                {
                    message = "User deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
