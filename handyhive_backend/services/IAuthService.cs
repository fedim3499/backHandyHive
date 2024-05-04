using handyhive_backend.Dto;
using handyhive_backend.models;

namespace handyhive_backend.services
{
    public interface IAuthService
    {
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        string CreateToken(User user);
        void SetRefreshToken(User user, RefreshToken newRefreshToken);
        RefreshToken GenerateRefreshToken(User user);
        Task<User> RegisterUser(UserDto request);
        Task<User> login(AuthDto request);
        Task<(string Token, string Message)> RefreshToken(string refreshToken);
        string GenerateRandomCode();
    }
}
