using AuthService.Infrastructure.Entity;
using AuthService.Models;
using AuthService.Models.Request;
using AuthService.Models.Response;

namespace AuthService.Services
{
    public interface IUserService
    {
        Task<AuthApiResponse> RegisterUser(RegisterRequest request);
        Task<AuthApiResponse> LoginUser(LoginRequest request);
        Task<AuthApiResponse> RefreshToken(string refreshToken);
        Task<AuthApiResponse> LogoutUser(string email);
        Task<User> FindUserByEmail(string email);
        Task<UserApiResponse> GetUserDetailsByEmailAsync(string email); // Get user details by email
        Task<UserApiResponse> UpdateUserDetailsByEmailAsync(string email, UpdateUserRequest model); // Update user details by email
        Task<UserApiResponse> DeleteUserByEmailAsync(string email); // Delete user by email
    }
}