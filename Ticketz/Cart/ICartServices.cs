using Ticketz.Models;

namespace Ticketz.Cart;

public interface ICartServices
{
    string ShoppingCartId { get; set; }
    List<ShoppingCartItem> ShoppingCartItems { get; set; }
    void AddItemToCart(Movie movie);
    void RemoveItemFromCart(Movie movie);
    List<ShoppingCartItem> GetShoppingCartItems();
    double TotalPriceOrder();
    Task ClearShoppingCartAsync();
}
