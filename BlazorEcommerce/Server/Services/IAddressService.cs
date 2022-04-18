
namespace BlazorEcommerce.Server.Services
{
    public interface IAddressService
    {
        Task<ServiceResponse<Address>> AddOrUpdateAddress(Address address);
        Task<ServiceResponse<Address>> GetAddress();
    }
}