using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Dtos;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public List<Product> Products { get ; set; } = new List<Product>();
        public string Message { get; set; } = "Loading Products...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText { get; set; } = string.Empty;

        public event Action ProductsChanged;

        public async Task<ServiceResponse<Product>> GetProduct(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"/api/product/{id}");
            return result;
        }

        public async Task GetProducts(string categoryUrl = null)
        {
            var result = categoryUrl == null ?
                await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("/api/product/featured") :
                await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"/api/product/category/{categoryUrl}");

            if (result != null && result.Data != null) Products = result.Data;

            CurrentPage = 1;
            PageCount = 0;

            if (Products.Count == 0)
                Message = "No Products found.";

            ProductsChanged?.Invoke();
        }

        public async Task<List<string>> GetSearchSuggestions(string text)
        {
            var result = await _httpClient
                .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/search/suggestions/{text}");
            return result.Data;
        }

        public async Task SearchProducts(string text, int page)
        {
            LastSearchText = text;

            var result = await _httpClient
                .GetFromJsonAsync<ServiceResponse<ProductSearchResult>>($"api/product/search/{text}/{page}");

            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;                
            }

            if (Products.Count == 0) Message = "No Products found.";

            ProductsChanged?.Invoke();
        }
    }
}
