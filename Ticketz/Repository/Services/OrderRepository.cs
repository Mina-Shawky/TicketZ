using Microsoft.EntityFrameworkCore;
using Ticketz.Data;
using Ticketz.Models;
using Ticketz.Repository.Base;

namespace Ticketz.Repository.Services;

public class OrderRepository : BaseRepository<Order>, IOrderRepository 
{
    private readonly TicketZDbContext _context;
    public OrderRepository(TicketZDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddOrderAsync(List<ShoppingCartItem> items, string userId, string email)
    {
        var order = new Order()
        {
            UserId = userId,
            Email =email
        };
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        foreach(var item in items)
        {
            var orderitem = new OrderItem()
            {
                Amount = item.Amount,
                Price = item.Movie.Price,
                OrderId=item.Id,
                MovieId=item.Movie.Id
            };
            await _context.OrderItems.AddAsync(orderitem);
        }
        await _context.SaveChangesAsync();

    }

    public async Task<List<Order>> GetOrdersByUserIdRoleAsync(string userId, string role)
    {
        var orders = await _context.Orders.ToListAsync();

        if (role != "Admin")
            orders =orders.Where(e => e.UserId == userId).ToList();

        return orders;
    }
}

