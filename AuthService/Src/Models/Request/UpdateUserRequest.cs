namespace AuthService.Models.Request;

public class UpdateUserRequest
{
    public string? Name { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? PasswordHash { get; set; }
}