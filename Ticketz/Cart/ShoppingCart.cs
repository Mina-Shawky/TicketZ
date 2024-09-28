using Ticketz.Models;
using Ticketz.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Ticketz.Cart;

public class ShoppingCart
{
    public TicketZDbContext _context {  get; set; }
    public string ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    public ShoppingCart(TicketZDbContext context)
    {
        _context = context;
    }
    public static ShoppingCart GetShoppingCart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        var context = services.GetService<TicketZDbContext>();

        string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
        session.SetString("CartId", cartId);

        return new ShoppingCart(context) { ShoppingCartId = cartId };
    }
    public void AddItemToCart(Movie movie)
    {
        var shoppingcartitem = _context.ShoppingCartItems.FirstOrDefault(e => e.Movie.Id == movie.Id && e.ShoppingCartId == ShoppingCartId);
        if (shoppingcartitem == null)
        {
            shoppingcartitem = new ShoppingCartItem()
            {
                ShoppingCartId = ShoppingCartId,
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
        var total= _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();
        return total;
    }

    public async Task ClearShoppingCartAsync()
    {
        var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
        _context.ShoppingCartItems.RemoveRange(items);
        await _context.SaveChangesAsync();
    }

}
