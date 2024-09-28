using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ticketz.Models;
using Ticketz.ViewModel;
using Ticketz.Data;

namespace Ticketz.Controllers;
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly TicketZDbContext _context;

    public AccountController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        TicketZDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Users()
    {
        var users = await _context.Users.ToListAsync();
        return View(users);
    }
    public async Task<IActionResult> Register()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                TempData["Error"] = "This Email Address is already in use";
                return View(model);
            }

            var newUser = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.FirstName
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser,"User");

            return RedirectToAction("Index","Movies");
        }
        return View(model);
    }
    public async Task<IActionResult> Login()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null) 
            {
                bool found = await _userManager.CheckPasswordAsync(user, model.Password);
                if (found == true)
                {
                    await _signInManager.SignInAsync(user, model.RememberMe);
                    return RedirectToAction("Index", "Movies");
                }
                else
                {
                    TempData["Error"]= "Email and Password Invalid!";
                }
            }
            else
            {
                TempData["Error"] = "Email and Password Invalid!";
            }
        }
        return View(model);
    }
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

}
