using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Ticketz.Repository.Services;
using Ticketz.ViewModel;

namespace Ticketz.Controllers;

public class CinemasController : Controller
{
    private readonly ICinemaRepository _cinemaRepository;
    private readonly IToastNotification _toastNotification;
    public CinemasController(ICinemaRepository cinemaRepository, IToastNotification toastNotification)
    {
        _cinemaRepository = cinemaRepository;
        _toastNotification = toastNotification;
    }
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Index()
    {
        var listOfCinemas = await _cinemaRepository.GetAllAsync();
        return View(listOfCinemas);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(AddCinemaViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _cinemaRepository.AddNewCinemaAsync(model);
        _toastNotification.AddSuccessToastMessage("Cinema Created Successfully");
        return RedirectToAction(nameof(Index));
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id)
    {
        var cinema = await _cinemaRepository.GetByIdAsync(id);
        var cimaVM = new UpdateCinemaViewModel()
        {
            Id = cinema.Id,
            Name = cinema.Name,
            Description = cinema.Description,
            LogoCurrent = cinema.Logo
        };
        return View(cimaVM);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(UpdateCinemaViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _cinemaRepository.UpdateCinemaAsync(model);
        _toastNotification.AddSuccessToastMessage("Cinema Updated Successfully");
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Details(int id)
    {
        var cinema = await _cinemaRepository.GetByIdAsync(id);
        return View(cinema);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _cinemaRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
