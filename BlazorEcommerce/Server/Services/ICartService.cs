using BlazorEcommerce.Shared.Dtos;

namespace BlazorEcommerce.Server.Services
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems);
    }
}
