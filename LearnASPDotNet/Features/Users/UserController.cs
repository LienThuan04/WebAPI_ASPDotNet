using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LearnASPDotNet.Features.Users.Dtos;
using LearnASPDotNet.Features.Users.Services;

namespace LearnASPDotNet.Features.Users
{
    [ApiController]
    [Route("user")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpPost()]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUser)
        {
            try
            {
                if (await _userService.CheckExistEmailOrUsername(createUser.Email) || await _userService.CheckExistEmailOrUsername(createUser.Username))
                {
                    HttpContext.Items["MessageResponse"] = "Email or UserName is already in use by another user";
                    return BadRequest("Email or UserName is already in use by another user");
                }
                await _userService.CreateUserAsync(createUser);
                return Ok(new
                {
                    Message = "User created successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllListUser()
        {
            try
            {
                var result = await _userService.FindAllUsersAsync();
                if (result == null || !result.Any())
                {
                    HttpContext.Items["MessageResponse"] = "No users found";
                    return NotFound("No users found");
                }
                return Ok(new
                {
                    Message = "Users retrieved successfully",
                    Users = result
                });

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
                var user = await _userService.FindOneUserByIdAsync(id);
                if (user == null)
                {
                    HttpContext.Items["MessageResponse"] = "User not found";
                    return NotFound("User not found");
                }
                //HttpContext.Items["MessageResponse"] = "User retrieved successfully"; // Đặt thông điệp thành công vào HttpContext.Items để ApiResponseWrapperFilter có thể sử dụng
                return Ok(new
                {
                    Message = "User retrieved successfully",
                    User = user
                });
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
                if (await _userService.CheckExistEmailOrUsername(updateUser.Email))
                {
                    HttpContext.Items["MessageResponse"] = "Email is already in use by another user";
                    return BadRequest("Email is already in use by another user");
                }
                var result = await _userService.UpdateUserAsync(id, updateUser);
                if (result == null)
                {
                    HttpContext.Items["MessageResponse"] = "User not found";
                    return NotFound("User not found");
                }
                var UpdatedUser = await _userService.UpdateUserAsync(id, updateUser);
                if (UpdatedUser == null)
                {
                    HttpContext.Items["MessageResponse"] = "Failed to update user";
                    return BadRequest("Failed to update user");
                }
                // Implement update logic here
                HttpContext.Items["MessageResponse"] = "User updated successfully";
                return Ok(new
                {
                    message = "User updated successfully",
                    user = result
                });
            }
            catch (Exception ex)
            {
                HttpContext.Items["MessageResponse"] = "Error updating user: " + ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var userExists = await _userService.FindOneUserByIdAsync(id);
                if (userExists == null)
                {
                    HttpContext.Items["MessageResponse"] = "User not found";
                    return NotFound("User not found");
                }
                var data = await _userService.DeleteUserAsync(id);
                if (data == null)
                {
                    HttpContext.Items["MessageResponse"] = "Failed to delete user";
                    return BadRequest("Failed to delete user");
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
