using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using Ticketz.Repository.Services;
using Ticketz.ViewModel;

namespace Ticketz.Controllers;
public class MoviesController : Controller
{
    private readonly IMovieRepository _movieRepository;
    private readonly IToastNotification _toast;
    public MoviesController(IMovieRepository movieRepository, IToastNotification toast)
    {
        _movieRepository = movieRepository;
        _toast = toast;
    }
    public async Task<IActionResult> Index()
    {
        var movies = await _movieRepository.GetAllAsync();
        return View(movies);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var list = await _movieRepository.MovieDropDawnListAsync();
        ViewBag.Nationalities = new SelectList(list.NationalityList, "Id", "Name");
        ViewBag.Actors = new SelectList(list.ActorList, "Id", "Name");
        ViewBag.Cinemas = new SelectList(list.CinemaList, "Id", "Name");
        ViewBag.Producers = new SelectList(list.ProducerList, "Id", "Name");
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateMovieViewModel model)
    {
        if (!ModelState.IsValid && model.NationalityId == 0 && model.CinemaId == 0 &&
            model.Genre == 0 && model.ProducerId == 0) 
        {
            var list = await _movieRepository.MovieDropDawnListAsync();
            ViewBag.Nationalities = new SelectList(list.NationalityList, "Id", "Name");
            ViewBag.Actors = new SelectList(list.ActorList, "Id", "Name");
            ViewBag.Cinemas = new SelectList(list.CinemaList, "Id", "Name");
            ViewBag.Producers = new SelectList(list.ProducerList, "Id", "Name");
            return View(model);
        }
        await _movieRepository.AddNewMovieAsync(model);
        _toast.AddSuccessToastMessage("Movie Created Successfully");
        return RedirectToAction(nameof(Index));
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id)
    {

        var list = await _movieRepository.MovieDropDawnListAsync();
        ViewBag.Nationalities = new SelectList(list.NationalityList, "Id", "Name");
        ViewBag.Actors = new SelectList(list.ActorList, "Id", "Name");
        ViewBag.Cinemas = new SelectList(list.CinemaList, "Id", "Name");
        ViewBag.Producers = new SelectList(list.ProducerList, "Id", "Name");

        var movieVM = await _movieRepository.GetByIdAsync(id);
        var movie = new UpdateMovieViewModel()
        {
            Title=movieVM.Title,
            Description = movieVM.Description,
            Price = movieVM.Price,
            PictureCurrent = movieVM.Picture,
            StartDate=movieVM.StartDate,
            EndDate=movieVM.EndDate,
            Genre=movieVM.Genre,
            Actor_Movie=movieVM.Actor_Movie.Select(e=>e.ActorId).ToList(),
            CinemaId = movieVM.CinemaId,
            ProducerId=movieVM.ProducerId,
            NationalityId=movieVM.NationalityId,
        };

        return View(movie);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(UpdateMovieViewModel model)
    {
        if (!ModelState.IsValid && model.NationalityId == 0 && model.CinemaId == 0 &&
            model.Genre == 0 && model.ProducerId == 0)
        {
            var list = await _movieRepository.MovieDropDawnListAsync();
            ViewBag.Nationalities = new SelectList(list.NationalityList, "Id", "Name");
            ViewBag.Actors = new SelectList(list.ActorList, "Id", "Name");
            ViewBag.Cinemas = new SelectList(list.CinemaList, "Id", "Name");
            ViewBag.Producers = new SelectList(list.ProducerList, "Id", "Name");
            return View(model);
        }
        await _movieRepository.UpdateNewMovieAsync(model);
        _toast.AddSuccessToastMessage("Movie Updated Successfully");
        return RedirectToAction(nameof(Index));
    }
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Details(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        return View(movie);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _movieRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Search(string title)
    {
        var moviesList = await _movieRepository.GetAllAsync();

        if (!string.IsNullOrEmpty(title))
        {
            var filteredResult = await _movieRepository.FindAsync(n => n.Title.ToLower().Contains(title.ToLower()) || n.Description.ToLower().Contains(title.ToLower()));
            return View("Index", filteredResult);
        }

        return View("Index", moviesList);
    }
}
