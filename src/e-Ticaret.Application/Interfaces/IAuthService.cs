using e_Ticaret.Application.DTOs.User;

namespace e_Ticaret.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginDto loginDto);
        Task<UserDto?> GetCurrentUserAsync(string username);
        Task<bool> ValidateTokenAsync(string token);
    }
}
