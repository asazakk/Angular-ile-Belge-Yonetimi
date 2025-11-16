using Microsoft.AspNetCore.Mvc;
using e_Ticaret.Application.DTOs.User;
using e_Ticaret.Application.Interfaces;

namespace e_Ticaret.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }
        
        /// <summary>
        /// Kullanıcı girişi
        /// </summary>
        /// <param name="loginDto">Giriş bilgileri</param>
        /// <returns>JWT token ve kullanıcı bilgileri</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                var result = await _authService.LoginAsync(loginDto);
                
                if (result == null)
                {
                    _logger.LogWarning("Login attempt failed for username: {Username}", loginDto.Username);
                    return Unauthorized(new { message = "Kullanıcı adı veya şifre hatalı." });
                }
                
                _logger.LogInformation("User {Username} logged in successfully", loginDto.Username);
                
                return Ok(new
                {
                    success = true,
                    message = "Giriş başarılı",
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login for username: {Username}", loginDto.Username);
                return StatusCode(500, new { message = "Sunucu hatası oluştu." });
            }
        }
        
        /// <summary>
        /// Mevcut kullanıcı bilgilerini getir
        /// </summary>
        /// <returns>Kullanıcı bilgileri</returns>
        [HttpGet("me")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var username = User.Identity?.Name;
                if (string.IsNullOrEmpty(username))
                {
                    return Unauthorized(new { message = "Geçersiz token." });
                }
                
                var user = await _authService.GetCurrentUserAsync(username);
                if (user == null)
                {
                    return NotFound(new { message = "Kullanıcı bulunamadı." });
                }
                
                return Ok(new
                {
                    success = true,
                    data = user
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting current user");
                return StatusCode(500, new { message = "Sunucu hatası oluştu." });
            }
        }
        
        /// <summary>
        /// Token geçerliliğini kontrol et
        /// </summary>
        /// <returns>Token durumu</returns>
        [HttpPost("validate-token")]
        public async Task<IActionResult> ValidateToken([FromBody] ValidateTokenRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Token))
                {
                    return BadRequest(new { message = "Token gerekli." });
                }
                
                var isValid = await _authService.ValidateTokenAsync(request.Token);
                
                return Ok(new
                {
                    success = true,
                    isValid = isValid,
                    message = isValid ? "Token geçerli" : "Token geçersiz"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while validating token");
                return StatusCode(500, new { message = "Sunucu hatası oluştu." });
            }
        }
    }
    
    public class ValidateTokenRequest
    {
        public string Token { get; set; } = string.Empty;
    }
}
