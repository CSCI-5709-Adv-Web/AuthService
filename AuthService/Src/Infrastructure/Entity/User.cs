using MongoDB.Bson.Serialization.Attributes;

namespace AuthService.Infrastructure.Entity
{
    public class User
    {
        [BsonId]
        public required string Id { get; set; } 
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string Name { get; set; }
        public string? ProfilePicture {get;set;}
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
    }
}