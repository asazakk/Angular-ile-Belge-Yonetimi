using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using e_Ticaret.Application.DTOs.Platforms;
using e_Ticaret.Application.Interfaces;

namespace e_Ticaret.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PlatformIntegrationsController : ControllerBase
    {
        private readonly IPlatformIntegrationService _platformIntegrationService;
        private readonly ILogger<PlatformIntegrationsController> _logger;
        
        public PlatformIntegrationsController(
            IPlatformIntegrationService platformIntegrationService, 
            ILogger<PlatformIntegrationsController> logger)
        {
            _platformIntegrationService = platformIntegrationService;
            _logger = logger;
        }
        
        /// <summary>
        /// Mağazanın tüm platform entegrasyonlarını getir
        /// </summary>
        [HttpGet("store/{storeId}")]
        public async Task<IActionResult> GetStorePlatformIntegrations(int storeId)
        {
            try
            {
                var integrations = await _platformIntegrationService.GetByStoreIdAsync(storeId);
                return Ok(new
                {
                    success = true,
                    data = integrations,
                    count = integrations.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting store platform integrations");
                return StatusCode(500, new { message = "Platform entegrasyonları getirirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// ID'ye göre platform entegrasyonu getir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlatformIntegrationById(int id)
        {
            try
            {
                var integration = await _platformIntegrationService.GetByIdAsync(id);
                if (integration == null)
                {
                    return NotFound(new { message = "Platform entegrasyonu bulunamadı." });
                }
                return Ok(new { success = true, data = integration });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting platform integration {IntegrationId}", id);
                return StatusCode(500, new { message = "Platform entegrasyonu getirilirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Yeni platform entegrasyonu oluştur
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreatePlatformIntegration([FromBody] CreatePlatformIntegrationDto createDto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var integration = await _platformIntegrationService.CreatePlatformIntegrationAsync(createDto, userId);
                return CreatedAtAction(nameof(GetPlatformIntegrationById), new { id = integration.Id }, 
                    new { success = true, data = integration, message = "Platform entegrasyonu başarıyla oluşturuldu." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating platform integration");
                return StatusCode(500, new { message = "Platform entegrasyonu oluşturulurken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Platform entegrasyonu güncelle
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlatformIntegration(int id, [FromBody] UpdatePlatformIntegrationDto updateDto)
        {
            try
            {
                if (id != updateDto.Id)
                {
                    return BadRequest(new { message = "ID uyuşmazlığı." });
                }
                
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var integration = await _platformIntegrationService.UpdatePlatformIntegrationAsync(updateDto, userId);
                
                if (integration == null)
                {
                    return NotFound(new { message = "Platform entegrasyonu bulunamadı." });
                }
                
                return Ok(new { success = true, data = integration, message = "Platform entegrasyonu başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating platform integration {IntegrationId}", id);
                return StatusCode(500, new { message = "Platform entegrasyonu güncellenirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Platform entegrasyonu sil
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlatformIntegration(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var result = await _platformIntegrationService.DeletePlatformIntegrationAsync(id, userId);
                
                if (!result)
                {
                    return NotFound(new { message = "Platform entegrasyonu bulunamadı veya silinemedi." });
                }
                
                return Ok(new { success = true, message = "Platform entegrasyonu başarıyla silindi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting platform integration {IntegrationId}", id);
                return StatusCode(500, new { message = "Platform entegrasyonu silinirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Platform bağlantısını test et
        /// </summary>
        [HttpPost("{id}/test-connection")]
        public async Task<IActionResult> TestConnection(int id)
        {
            try
            {
                var result = await _platformIntegrationService.TestConnectionAsync(id);
                
                if (!result)
                {
                    return BadRequest(new { message = "Bağlantı testi başarısız." });
                }
                
                return Ok(new { success = true, message = "Bağlantı testi başarılı." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while testing connection for integration {IntegrationId}", id);
                return StatusCode(500, new { message = "Bağlantı testi sırasında hata oluştu." });
            }
        }
        
        /// <summary>
        /// Ürünleri senkronize et
        /// </summary>
        [HttpPost("{id}/sync-products")]
        public async Task<IActionResult> SyncProducts(int id)
        {
            try
            {
                var result = await _platformIntegrationService.SyncProductsAsync(id);
                return Ok(new { success = result.Success, data = result, message = result.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while syncing products for integration {IntegrationId}", id);
                return StatusCode(500, new { message = "Ürün senkronizasyonu sırasında hata oluştu." });
            }
        }
        
        /// <summary>
        /// Siparişleri senkronize et
        /// </summary>
        [HttpPost("{id}/sync-orders")]
        public async Task<IActionResult> SyncOrders(int id)
        {
            try
            {
                var result = await _platformIntegrationService.SyncOrdersAsync(id);
                return Ok(new { success = result.Success, data = result, message = result.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while syncing orders for integration {IntegrationId}", id);
                return StatusCode(500, new { message = "Sipariş senkronizasyonu sırasında hata oluştu." });
            }
        }
        
        /// <summary>
        /// Tam senkronizasyon (ürünler ve siparişler)
        /// </summary>
        [HttpPost("{id}/full-sync")]
        public async Task<IActionResult> FullSync(int id)
        {
            try
            {
                var result = await _platformIntegrationService.FullSyncAsync(id);
                return Ok(new { success = result.Success, data = result, message = result.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while performing full sync for integration {IntegrationId}", id);
                return StatusCode(500, new { message = "Tam senkronizasyon sırasında hata oluştu." });
            }
        }
    }
}
