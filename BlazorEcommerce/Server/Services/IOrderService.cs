using BlazorEcommerce.Shared.Dtos;

namespace BlazorEcommerce.Server.Services
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PlaceOrder();
        Task<ServiceResponse<List<OrderOverViewResponse>>> GetOrders();
        Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetails(int orderId);
    }
}
