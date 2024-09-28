using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ticketz.Cart;
using Ticketz.Repository.Services;
using Ticketz.ViewModel;

namespace Ticketz.Controllers;
public class OrdersController : Controller
{
    private readonly IOrderRepository _orderRepository;
    private readonly ShoppingCart _cart;
    private readonly IMovieRepository _movieRepository;
    public OrdersController(IOrderRepository orderRepository,
        ShoppingCart cart, IMovieRepository movieRepository)
    {
        _orderRepository = orderRepository;
        _cart = cart;
        _movieRepository = movieRepository;
    }

    public async Task<IActionResult> Index()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        string roleUser = User.FindFirstValue(ClaimTypes.Role);

        var order = await _orderRepository.GetOrdersByUserIdRoleAsync(userId, roleUser);
        return View(order);
    }
    public IActionResult ShoppingCart()
    {
        var items = _cart.GetShoppingCartItems();
        _cart.ShoppingCartItems = items;

        var response = new ShoppingCartViewModel()
        {
            ShoppingCart = _cart,
            ShoppingCartTotal = _cart.TotalPriceOrder()
        };

        return View(response);
    }
    [Authorize(Roles ="User")]
    public async Task<IActionResult> AddItemToCart(int id)
    {
        var item = await _movieRepository.GetByIdAsync(id);

        if (item != null)
        {
            _cart.AddItemToCart(item);
        }
        return RedirectToAction(nameof(ShoppingCart));
    }

    public async Task<IActionResult> RemoveItemFromCart(int id)
    {
        var item = await _movieRepository.GetByIdAsync(id);

        if (item != null)
        {
            _cart.RemoveItemFromCart(item);
        }
        return RedirectToAction(nameof(ShoppingCart));
    }

    public async Task<IActionResult> CompleteOrder()
    {
        var items = _cart.GetShoppingCartItems();
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

        await _orderRepository.AddOrderAsync(items, userId, userEmailAddress);
        await _cart.ClearShoppingCartAsync();

        return View();
    }
}
