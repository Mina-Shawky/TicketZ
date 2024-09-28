using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using NuGet.Protocol.Core.Types;
using Ticketz.Repository.Services;
using Ticketz.ViewModel;

namespace Ticketz.Controllers;

public class ActorsController : Controller
{
    private readonly IActorRepository _actorRepository;
    private readonly IToastNotification _toastNotification;
    public ActorsController(IActorRepository actorRepository, IToastNotification toastNotification)
    {
        _actorRepository = actorRepository;
        _toastNotification = toastNotification;
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var actors = await _actorRepository.GetAllAsync();
        return View(actors);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(AddNewActorViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _actorRepository.AddActorAsync(model);
        _toastNotification.AddSuccessToastMessage("Actor Created Successfully");
        return RedirectToAction(nameof(Index));
        
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id)
    {
        var actor = await _actorRepository.GetByIdAsync(id);
        var actorviewmodel = new UpdateActorViewModel()
        {
            Name=actor.Name,
            Bio = actor.Bio,
            PictureCurrent=actor.Picture,
            
        };
        return View(actorviewmodel);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(UpdateActorViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _actorRepository.UpdateActorAsync(model);
        _toastNotification.AddSuccessToastMessage("Actor Updated Successfully");
        return RedirectToAction(nameof(Index));

    }
    public async Task<IActionResult> Details(int id)
    {
        var actor = await _actorRepository.GetByIdAsync(id);
        return View(actor);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _actorRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

}
