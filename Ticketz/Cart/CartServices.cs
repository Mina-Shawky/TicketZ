using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Ticketz.Data;
using Ticketz.Models;

namespace Ticketz.Cart;

public class CartServices : ICartServices
{
    private readonly TicketZDbContext _context;
    public string ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    public CartServices(TicketZDbContext context)
    {
        _context = context;
    }

    public void AddItemToCart(Movie movie)
    {
        var shoppingcartitem = _context.ShoppingCartItems.FirstOrDefault(e => e.Movie.Id == movie.Id && e.ShoppingCartId == ShoppingCartId);
        if (shoppingcartitem == null)
        {
            shoppingcartitem = new ShoppingCartItem()
            {
                ShoppingCartId = Guid.NewGuid().ToString(),
                Movie = movie,
                Amount = 1
            };

            _context.ShoppingCartItems.Add(shoppingcartitem);
        }
        else
        {
            shoppingcartitem.Amount++;
        }
        _context.SaveChanges();
    }
    public void RemoveItemFromCart(Movie movie)
    {
        var shoppingcartitem = _context.ShoppingCartItems.FirstOrDefault(e => e.Movie.Id == movie.Id && e.ShoppingCartId == ShoppingCartId);
        if (shoppingcartitem != null)
        {
            if (shoppingcartitem.Amount > 1)
            {
                shoppingcartitem.Amount--;
            }
            else
            {
                _context.ShoppingCartItems.Remove(shoppingcartitem);
            }
        }
        _context.SaveChanges();
    }
    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList());
    }

    public double TotalPriceOrder()
    {
        var total = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();
        return total;
    }

    public async Task ClearShoppingCartAsync()
    {
        var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
        _context.ShoppingCartItems.RemoveRange(items);
        await _context.SaveChangesAsync();
    }



}
