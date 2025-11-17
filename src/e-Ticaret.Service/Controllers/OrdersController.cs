using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using e_Ticaret.Application.DTOs.Orders;
using e_Ticaret.Application.Interfaces;
using e_Ticaret.Domain.Enums;

namespace e_Ticaret.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;
        
        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }
        
        /// <summary>
        /// Mağazadaki tüm siparişleri getir
        /// </summary>
        [HttpGet("store/{storeId}")]
        public async Task<IActionResult> GetStoreOrders(int storeId)
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync(storeId);
                return Ok(new
                {
                    success = true,
                    data = orders,
                    count = orders.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting store orders");
                return StatusCode(500, new { message = "Siparişleri getirirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// ID'ye göre sipariş getir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return NotFound(new { message = "Sipariş bulunamadı." });
                }
                return Ok(new { success = true, data = order });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting order {OrderId}", id);
                return StatusCode(500, new { message = "Sipariş getirilirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Sipariş detaylarını getir (ürünlerle birlikte)
        /// </summary>
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetOrderWithItems(int id)
        {
            try
            {
                var order = await _orderService.GetOrderWithItemsAsync(id);
                if (order == null)
                {
                    return NotFound(new { message = "Sipariş bulunamadı." });
                }
                return Ok(new { success = true, data = order });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting order details {OrderId}", id);
                return StatusCode(500, new { message = "Sipariş detayları getirilirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Duruma göre siparişleri getir
        /// </summary>
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetOrdersByStatus(OrderStatus status)
        {
            try
            {
                var orders = await _orderService.GetOrdersByStatusAsync(status);
                return Ok(new
                {
                    success = true,
                    data = orders,
                    count = orders.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting orders by status");
                return StatusCode(500, new { message = "Siparişleri getirirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Son siparişleri getir
        /// </summary>
        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentOrders([FromQuery] int count = 10)
        {
            try
            {
                var orders = await _orderService.GetRecentOrdersAsync(count);
                return Ok(new
                {
                    success = true,
                    data = orders,
                    count = orders.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting recent orders");
                return StatusCode(500, new { message = "Siparişleri getirirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Tarih aralığına göre siparişleri getir
        /// </summary>
        [HttpGet("date-range")]
        public async Task<IActionResult> GetOrdersByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var orders = await _orderService.GetOrdersByDateRangeAsync(startDate, endDate);
                return Ok(new
                {
                    success = true,
                    data = orders,
                    count = orders.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting orders by date range");
                return StatusCode(500, new { message = "Siparişleri getirirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Sipariş durumu güncelle
        /// </summary>
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDto updateOrderStatusDto)
        {
            try
            {
                if (id != updateOrderStatusDto.OrderId)
                {
                    return BadRequest(new { message = "ID uyuşmazlığı." });
                }
                
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var result = await _orderService.UpdateOrderStatusAsync(updateOrderStatusDto, userId);
                
                if (!result)
                {
                    return NotFound(new { message = "Sipariş bulunamadı veya güncellenemedi." });
                }
                
                return Ok(new { success = true, message = "Sipariş durumu başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating order status {OrderId}", id);
                return StatusCode(500, new { message = "Sipariş durumu güncellenirken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Toplam satış tutarını getir
        /// </summary>
        [HttpGet("store/{storeId}/sales")]
        public async Task<IActionResult> GetTotalSales(int storeId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var totalSales = await _orderService.GetTotalSalesAsync(storeId, startDate, endDate);
                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        totalSales,
                        startDate,
                        endDate
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting total sales");
                return StatusCode(500, new { message = "Satış tutarı hesaplanırken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Sipariş istatistiklerini getir
        /// </summary>
        [HttpGet("store/{storeId}/statistics")]
        public async Task<IActionResult> GetOrderStatistics(int storeId)
        {
            try
            {
                var statistics = await _orderService.GetOrderStatisticsAsync(storeId);
                return Ok(new
                {
                    success = true,
                    data = statistics
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting order statistics");
                return StatusCode(500, new { message = "İstatistikler hesaplanırken hata oluştu." });
            }
        }
        
        /// <summary>
        /// Platformdan siparişleri senkronize et
        /// </summary>
        [HttpPost("sync/platform/{platformIntegrationId}")]
        public async Task<IActionResult> SyncOrdersFromPlatform(int platformIntegrationId)
        {
            try
            {
                var result = await _orderService.SyncOrdersFromPlatformAsync(platformIntegrationId);
                
                if (!result)
                {
                    return BadRequest(new { message = "Siparişler senkronize edilemedi." });
                }
                
                return Ok(new { success = true, message = "Siparişler başarıyla senkronize edildi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while syncing orders from platform {PlatformIntegrationId}", platformIntegrationId);
                return StatusCode(500, new { message = "Senkronizasyon sırasında hata oluştu." });
            }
        }
    }
}
