using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;
using e_Ticaret.Application.DTOs.User;
using e_Ticaret.Application.Interfaces;
using e_Ticaret.Domain.Interfaces;
using e_Ticaret.Domain.Entities;

namespace e_Ticaret.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        
        public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
        {
            // DEBUG: Login attempt
            Console.WriteLine($"DEBUG: Login attempt for username: {loginDto.Username}");
            
            // Kullanıcıyı username ile bul
            var user = await _unitOfWork.Users.GetByUsernameAsync(loginDto.Username);
            
            // DEBUG: User found check
            Console.WriteLine($"DEBUG: User found: {user != null}");
            if (user != null)
            {
                Console.WriteLine($"DEBUG: User active: {user.IsActive}");
                var hashLength = user.PasswordHash?.Length ?? 0;
                var displayLength = Math.Min(20, hashLength);
                Console.WriteLine($"DEBUG: User password hash: {user.PasswordHash?.Substring(0, displayLength)}...");
            }
            
            if (user == null || !user.IsActive)
            {
                Console.WriteLine("DEBUG: User not found or inactive, returning null");
                return null; // Kullanıcı bulunamadı veya aktif değil
            }
            
            // Şifre kontrolü - Geçici basit kontrol
            bool isPasswordValid = false;
            
            Console.WriteLine($"DEBUG: Checking password for user: {loginDto.Username}");
            Console.WriteLine($"DEBUG: Input password: {loginDto.Password}");
            
            // Test kullanıcıları için basit kontrol
            if (loginDto.Username == "admin" && loginDto.Password == "admin123")
            {
                isPasswordValid = true;
                Console.WriteLine("DEBUG: Admin login success");
            }
            else if (loginDto.Username == "user1" && loginDto.Password == "user123")
            {
                isPasswordValid = true;
                Console.WriteLine("DEBUG: User1 login success");
            }
            else
            {
                // BCrypt kontrolü de deneyelim
                try 
                {
                    isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
                    Console.WriteLine($"DEBUG: BCrypt verification result: {isPasswordValid}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"DEBUG: BCrypt verification failed: {ex.Message}");
                }
            }
            
            Console.WriteLine($"DEBUG: Final password validation result: {isPasswordValid}");
            
            if (!isPasswordValid)
            {
                Console.WriteLine("DEBUG: Password validation failed, returning null");
                return null; // Yanlış şifre
            }
            
            // Son giriş tarihini güncelle
            user.LastLoginDate = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            
            // Token oluştur
            var token = GenerateJwtToken(user);
            var expiresAt = DateTime.UtcNow.AddHours(GetTokenExpirationHours());
            
            return new LoginResponseDto
            {
                Token = token,
                ExpiresAt = expiresAt,
                User = MapToUserDto(user)
            };
        }
        
        public async Task<UserDto?> GetCurrentUserAsync(string username)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(username);
            return user != null ? MapToUserDto(user) : null;
        }
        
        public Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(GetJwtSecret());
                
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = GetJwtIssuer(),
                    ValidateAudience = true,
                    ValidAudience = GetJwtAudience(),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
        
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(GetJwtSecret());
            
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.GivenName, user.FirstName),
                new(ClaimTypes.Surname, user.LastName),
                new("department", user.Department ?? ""),
                new("position", user.Position ?? "")
            };
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(GetTokenExpirationHours()),
                Issuer = GetJwtIssuer(),
                Audience = GetJwtAudience(),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
        private UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Department = user.Department,
                Position = user.Position,
                IsActive = user.IsActive,
                LastLoginDate = user.LastLoginDate,
                CreatedAt = user.CreatedAt
            };
        }
        
        // Configuration helpers
        private string GetJwtSecret()
        {
            return _configuration["Jwt:Secret"] ?? "MySecretKeyForDanistayApp2025!@#$%^&*()";
        }
        
        private string GetJwtIssuer()
        {
            return _configuration["Jwt:Issuer"] ?? "DanistayApp";
        }
        
        private string GetJwtAudience()
        {
            return _configuration["Jwt:Audience"] ?? "DanistayAppUsers";
        }
        
        private int GetTokenExpirationHours()
        {
            return int.TryParse(_configuration["Jwt:ExpirationHours"], out int hours) ? hours : 24;
        }
    }
}
