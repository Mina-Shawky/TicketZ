using Ticketz.Models;
using Ticketz.Repository.Base;

namespace Ticketz.Repository.Services;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task AddOrderAsync(List<ShoppingCartItem> items, string userId, string email);
    Task<List<Order>> GetOrdersByUserIdRoleAsync(string userId, string role);
}
