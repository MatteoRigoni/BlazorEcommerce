using BlazorEcommerce.Server.Data;
using BlazorEcommerce.Server.Services;
using BlazorEcommerce.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
        {
            var result = await _productService.GetProductsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int id)
        {
            var result = await _productService.GetProductAsync(id);
            return Ok(result);
        }

        [HttpGet("category/{categoryUrl}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategory(string categoryUrl)
        {
            var result = await _productService.GetProductsByCategoryAsync(categoryUrl);
            return Ok(result);
        }

        [HttpGet("search/{text}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductSearchResult>>> SearchProducts(string text, int page)
        {
            var result = await _productService.SearchProductsAsync(text, page);
            return Ok(result);
        }

        [HttpGet("search/suggestions/{text}")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetSearchSuggestions(string text)
        {
            var result = await _productService.GetProductSearchSuggestionsAsync(text);
            return Ok(result);
        }

        [HttpGet("featured")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetFeaturedProducts()
        {
            var result = await _productService.GetFeaturedProductsAsync();
            return Ok(result);
        }
    }
}
