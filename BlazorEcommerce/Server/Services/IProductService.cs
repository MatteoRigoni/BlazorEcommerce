using BlazorEcommerce.Shared.Dtos;

namespace BlazorEcommerce.Server.Services
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<Product>> GetProductAsync(int id);
        Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl);
        Task<ServiceResponse<ProductSearchResult>> SearchProductsAsync(string text, int page);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestionsAsync(string text);
        Task<ServiceResponse<List<Product>>> GetFeaturedProductsAsync();
    }
}
