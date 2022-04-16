using BlazorEcommerce.Server.Data;
using BlazorEcommerce.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Product>>> GetFeaturedProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _context.Products
                    .Where(p => p.Featured)
                    .Include(p => p.Variants)
                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int id)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products
                .Include(p => p.Variants)
                .ThenInclude(v => v.ProductType)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                response.Success = false;
                response.Message = "Product does not exists!";
            }
            else
                response.Data = product;

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Variants)
                .ToListAsync();

            return new ServiceResponse<List<Product>>() {  Data = products };
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _context.Products
                    .Include(p => p.Variants)
                    .Where(p => p.Category != null && p.Category.Url.ToLower().Equals(categoryUrl.ToLower()))
                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestionsAsync(string text)
        {
            var products = await FindProductsBySearchText(text);
            List<string> result = new List<string>();
            foreach (var product in products)
            {
                if (product.Title.Contains(text, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }

                if (product.Description != null)
                {
                    var punctuation = product.Description.Where(char.IsPunctuation)
                        .Distinct().ToArray();
                    var words = product.Description.Split()
                        .Select(s => s.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(text, StringComparison.OrdinalIgnoreCase)
                            && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }

            return new ServiceResponse<List<string>> { Data = result };
        }

        public async Task<ServiceResponse<ProductSearchResult>> SearchProductsAsync(string text, int page)
        {
            var pageResults = 2f;
            var pageCount = Math.Ceiling((await FindProductsBySearchText(text)).Count() / pageResults);
            var products = await _context.Products
                                .Where(p => p.Title.ToLower().Contains(text.ToLower())
                               ||
                               p.Description.ToLower().Contains(text.ToLower()))
                               .Include(p => p.Variants)
                               .Skip((page-1) * (int)pageResults)
                               .Take((int)pageResults)
                               .ToListAsync();

            var response = new ServiceResponse<ProductSearchResult>()
            {
                Data = new ProductSearchResult()
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int) pageCount
                }
            };

            return response;
        }

        private async Task<List<Product>> FindProductsBySearchText(string text)
        {
            return await _context.Products
                                .Where(p => p.Title.ToLower().Contains(text.ToLower())
                               ||
                               p.Description.ToLower().Contains(text.ToLower()))
                               .Include(p => p.Variants)
                               .ToListAsync();
        }
    }
}
