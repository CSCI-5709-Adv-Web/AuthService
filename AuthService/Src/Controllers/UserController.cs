using AuthService.Models.Request;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    [Authorize]
    [HttpGet("details/{email}")]
    public async Task<IActionResult> GetUserDetails(string email)
    {
        var user = await userService.GetUserDetailsByEmailAsync(email);
        return Ok(user);
    }
    
    [Authorize]
    [HttpPut("update/{email}")]
    public async Task<IActionResult> UpdateUserDetails(string email, [FromBody] UpdateUserRequest model)
    {
        var result = await userService.UpdateUserDetailsByEmailAsync(email, model);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(result);
    }
    
    [Authorize]
    [HttpDelete("delete/{email}")]
    public async Task<IActionResult> DeleteUser(string email)
    {
        var result = await userService.DeleteUserByEmailAsync(email);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok("User deleted successfully.");
    }
}