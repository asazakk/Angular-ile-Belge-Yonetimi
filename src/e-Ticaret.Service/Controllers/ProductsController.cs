using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using e_Ticaret.Application.DTOs.Products;
using e_Ticaret.Application.Interfaces;

namespace e_Ticaret.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        
        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        
        /// <summary>
        /// Mağazadaki tüm ürünleri getir
        /// </summary>
        [HttpGet("store/{storeId}")]
        public async Task<IActionResult> GetStoreProducts(int storeId)
        {
            try
            {
                var products = await _productService.GetAllProductsAsync(storeId);
                return Ok(new
                {
                    success = true,
                    data = products,
                    count = products.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting store products");
                return StatusCode(500, new { message = "Ürünleri getirirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// ID'ye göre ürün getir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new { message = "Ürün bulunamadı." });
                }
                return Ok(new { success = true, data = product });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting product {ProductId}", id);
                return StatusCode(500, new { message = "Ürün getirilirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Platform bilgileriyle birlikte ürün getir
        /// </summary>
        [HttpGet("{id}/with-platforms")]
        public async Task<IActionResult> GetProductWithPlatforms(int id)
        {
            try
            {
                var product = await _productService.GetProductWithPlatformsAsync(id);
                if (product == null)
                {
                    return NotFound(new { message = "Ürün bulunamadı." });
                }
                return Ok(new { success = true, data = product });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting product with platforms {ProductId}", id);
                return StatusCode(500, new { message = "Ürün getirilirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Düşük stoklu ürünleri getir
        /// </summary>
        [HttpGet("store/{storeId}/low-stock")]
        public async Task<IActionResult> GetLowStockProducts(int storeId)
        {
            try
            {
                var products = await _productService.GetLowStockProductsAsync(storeId);
                return Ok(new
                {
                    success = true,
                    data = products,
                    count = products.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting low stock products");
                return StatusCode(500, new { message = "Ürünleri getirirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Ürün ara
        /// </summary>
        [HttpGet("store/{storeId}/search")]
        public async Task<IActionResult> SearchProducts(int storeId, [FromQuery] string term)
        {
            try
            {
                var products = await _productService.SearchProductsAsync(term, storeId);
                return Ok(new
                {
                    success = true,
                    data = products,
                    count = products.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching products");
                return StatusCode(500, new { message = "Arama yapılırken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Yeni ürün oluştur
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var product = await _productService.CreateProductAsync(createProductDto, userId);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, 
                    new { success = true, data = product, message = "Ürün başarıyla oluşturuldu." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating product");
                return StatusCode(500, new { message = "Ürün oluşturulurken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Ürün güncelle
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto updateProductDto)
        {
            try
            {
                if (id != updateProductDto.Id)
                {
                    return BadRequest(new { message = "ID uyuşmazlığı." });
                }
                
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var product = await _productService.UpdateProductAsync(updateProductDto, userId);
                
                if (product == null)
                {
                    return NotFound(new { message = "Ürün bulunamadı." });
                }
                
                return Ok(new { success = true, data = product, message = "Ürün başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product {ProductId}", id);
                return StatusCode(500, new { message = "Ürün güncellenirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Ürün sil
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var result = await _productService.DeleteProductAsync(id, userId);
                
                if (!result)
                {
                    return NotFound(new { message = "Ürün bulunamadı veya silinemedi." });
                }
                
                return Ok(new { success = true, message = "Ürün başarıyla silindi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product {ProductId}", id);
                return StatusCode(500, new { message = "Ürün silinirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Ürün stok güncelle
        /// </summary>
        [HttpPut("{id}/stock")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockRequest request)
        {
            try
            {
                var result = await _productService.UpdateStockAsync(id, request.Quantity, request.ChangeType, request.Reason);
                
                if (!result)
                {
                    return NotFound(new { message = "Ürün bulunamadı veya stok güncellenemedi." });
                }
                
                return Ok(new { success = true, message = "Stok başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating stock for product {ProductId}", id);
                return StatusCode(500, new { message = "Stok güncellenirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Ürün fiyat güncelle
        /// </summary>
        [HttpPut("{id}/price")]
        public async Task<IActionResult> UpdatePrice(int id, [FromBody] UpdatePriceRequest request)
        {
            try
            {
                var result = await _productService.UpdatePriceAsync(id, request.NewPrice, request.ChangeType, request.Reason);
                
                if (!result)
                {
                    return NotFound(new { message = "Ürün bulunamadı veya fiyat güncellenemedi." });
                }
                
                return Ok(new { success = true, message = "Fiyat başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating price for product {ProductId}", id);
                return StatusCode(500, new { message = "Fiyat güncellenirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Ürünü platforma senkronize et
        /// </summary>
        [HttpPost("{id}/sync/{platformIntegrationId}")]
        public async Task<IActionResult> SyncProductToPlatform(int id, int platformIntegrationId)
        {
            try
            {
                var result = await _productService.SyncProductToPlatformAsync(id, platformIntegrationId);
                
                if (!result)
                {
                    return BadRequest(new { message = "Ürün senkronize edilemedi." });
                }
                
                return Ok(new { success = true, message = "Ürün başarıyla senkronize edildi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while syncing product {ProductId} to platform {PlatformIntegrationId}", id, platformIntegrationId);
                return StatusCode(500, new { message = "Senkronizasyon sırasında hata oluştu." });
            }
        }
    }
    
    public class UpdateStockRequest
    {
        public int Quantity { get; set; }
        public string ChangeType { get; set; } = string.Empty;
        public string? Reason { get; set; }
    }
    
    public class UpdatePriceRequest
    {
        public decimal NewPrice { get; set; }
        public string ChangeType { get; set; } = string.Empty;
        public string? Reason { get; set; }
    }
}
