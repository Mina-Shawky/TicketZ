using Microsoft.AspNetCore.Mvc;
using Ticketz.Cart;

namespace Ticketz.ViewComponents;

public class CartViewComponentsummery : ViewComponent
{
    private readonly ShoppingCart _shoppingCart;
    public CartViewComponentsummery(ShoppingCart shoppingCart)
    {
        _shoppingCart = shoppingCart;
    }

    public IViewComponentResult Invoke()
    {
        var items = _shoppingCart.GetShoppingCartItems();
        return View(items.Count);
    }
}
