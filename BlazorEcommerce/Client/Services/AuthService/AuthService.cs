using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Dtos;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;

        public AuthService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request)
        {
            var result = await _client.PostAsJsonAsync("api/auth/changepassword", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _client.PostAsJsonAsync("api/auth/login", request);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            }
            else
            {
                return new ServiceResponse<string>()
                {
                    Success = false,
                    Message = "Check your email and password."
                };
            }
            
        }

        public async Task<ServiceResponse<int>> Register(UserRegister request)
        {
            var result = await _client.PostAsJsonAsync("api/auth/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}
