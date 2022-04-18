﻿using BlazorEcommerce.Shared.Dtos;

namespace BlazorEcommerce.Client.Services.OrderService
{
    public interface IOrderService
    {
        Task<string> PlaceOrder();
        Task<List<OrderOverViewResponse>> GetOrders();
        Task<OrderDetailsResponse> GetOrderDetails(int orderId);
    }
}
