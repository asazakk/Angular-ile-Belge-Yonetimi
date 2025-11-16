using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using e_Ticaret.Application.DTOs.Stores;
using e_Ticaret.Application.Interfaces;

namespace e_Ticaret.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly ILogger<StoresController> _logger;
        
        public StoresController(IStoreService storeService, ILogger<StoresController> logger)
        {
            _storeService = storeService;
            _logger = logger;
        }
        
        /// <summary>
        /// Kullanıcının tüm mağazalarını getir
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUserStores()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var stores = await _storeService.GetAllStoresAsync(userId);
                return Ok(new
                {
                    success = true,
                    data = stores,
                    count = stores.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user stores");
                return StatusCode(500, new { message = "Mağazalar getirirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// ID'ye göre mağaza getir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreById(int id)
        {
            try
            {
                var store = await _storeService.GetStoreByIdAsync(id);
                if (store == null)
                {
                    return NotFound(new { message = "Mağaza bulunamadı." });
                }
                return Ok(new { success = true, data = store });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting store {StoreId}", id);
                return StatusCode(500, new { message = "Mağaza getirilirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Aktif mağazaları getir
        /// </summary>
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveStores()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var stores = await _storeService.GetActiveStoresAsync(userId);
                return Ok(new
                {
                    success = true,
                    data = stores,
                    count = stores.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting active stores");
                return StatusCode(500, new { message = "Aktif mağazalar getirirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Yeni mağaza oluştur
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] CreateStoreDto createStoreDto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var store = await _storeService.CreateStoreAsync(createStoreDto, userId);
                return CreatedAtAction(nameof(GetStoreById), new { id = store.Id }, 
                    new { success = true, data = store, message = "Mağaza başarıyla oluşturuldu." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating store");
                return StatusCode(500, new { message = "Mağaza oluşturulurken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Mağaza güncelle
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(int id, [FromBody] UpdateStoreDto updateStoreDto)
        {
            try
            {
                if (id != updateStoreDto.Id)
                {
                    return BadRequest(new { message = "ID uyuşmazlığı." });
                }
                
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var store = await _storeService.UpdateStoreAsync(updateStoreDto, userId);
                
                if (store == null)
                {
                    return NotFound(new { message = "Mağaza bulunamadı." });
                }
                
                return Ok(new { success = true, data = store, message = "Mağaza başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating store {StoreId}", id);
                return StatusCode(500, new { message = "Mağaza güncellenirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Mağaza sil
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var result = await _storeService.DeleteStoreAsync(id, userId);
                
                if (!result)
                {
                    return NotFound(new { message = "Mağaza bulunamadı veya silinemedi." });
                }
                
                return Ok(new { success = true, message = "Mağaza başarıyla silindi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting store {StoreId}", id);
                return StatusCode(500, new { message = "Mağaza silinirken hata oluştu." });
            }
        }
    }
}
