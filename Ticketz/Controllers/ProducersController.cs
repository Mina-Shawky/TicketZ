using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Ticketz.Repository.Services;
using Ticketz.ViewModel;

namespace Ticketz.Controllers;

public class ProducersController : Controller
{
    private readonly IProducerRepository _producerRepository;
    public ProducersController(IProducerRepository producerRepository)
    {
        _producerRepository = producerRepository;
    }
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Index()
    {
        var producer = await _producerRepository.GetAllAsync();
        return View(producer);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(AddNewProducerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _producerRepository.AddProducerAsync(model);
        return RedirectToAction(nameof(Index));

    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id)
    {
        var producer = await _producerRepository.GetByIdAsync(id);
        var producerVM = new UpdateProducerViewModel()
        {
            Name=producer.Name,
            Bio=producer.Bio,
            PictureCurrent=producer.Picture,
        };
        return View(producerVM);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(UpdateProducerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _producerRepository.UpdateProducerAsync(model);
        return RedirectToAction(nameof(Index));

    }
    public async Task<IActionResult> Details(int id)
    {
        var producer = await _producerRepository.GetByIdAsync(id);
        return View(producer);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _producerRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
