using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly NavigationManager _navigationManager;

        public OrderService(HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider,
            NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _navigationManager = navigationManager;
        }

        public async Task PlaceOrder()
        {
            if (await IsUserAuthenticated())
            {
                await _httpClient.PostAsync("api/order", null);
            }
            else
            {
                _navigationManager.NavigateTo("login");
            }
        }

        public async Task<List<OrderOverViewResponse>> GetOrders()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OrderOverViewResponse>>>("api/order");
            return result.Data;
        }

        public async Task<OrderDetailsResponse> GetOrderDetails(int orderId)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>($"api/order/{orderId}");
            return result.Data;
        }

        private async Task<bool> IsUserAuthenticated()
        {
            return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }
    }
}
