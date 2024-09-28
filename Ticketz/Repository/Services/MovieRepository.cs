using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ticketz.Data;
using Ticketz.Models;
using Ticketz.Repository.Base;
using Ticketz.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ticketz.Repository.Services;

public class MovieRepository : BaseRepository<Movie>, IMovieRepository
{
    private readonly TicketZDbContext _context;
    public MovieRepository(TicketZDbContext context) :base(context)
    {
        _context = context;
    }

    public async Task AddNewMovieAsync(CreateMovieViewModel model)
    {
        var stream = new MemoryStream();
        await model.Picture.CopyToAsync(stream);

        Movie movie = new Movie()
        {
            Title=model.Title,
            Description=model.Description,
            StartDate=model.StartDate,
            EndDate=model.EndDate,
            Price=model.Price,
            CinemaId=model.CinemaId,
            NationalityId=model.NationalityId,
            ProducerId=model.ProducerId,
            Picture=stream.ToArray(),
            Genre=model.Genre,

        };
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
        foreach (var actorId in model.Actor_Movie)
        {
            var newActorMovie = new Actor_Movie()
            {
                MovieId = movie.Id,
                ActorId = actorId
            };
            await _context.Actors_Movies.AddAsync(newActorMovie);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<MovieDropDawnListViewModel> MovieDropDawnListAsync()
    {
        var droplist = new MovieDropDawnListViewModel()
        {
            CinemaList = await _context.Cinemas.OrderBy(e => e.Name).ToListAsync(),
            ActorList = await _context.Actors.OrderBy(e => e.Name).ToListAsync(),
            NationalityList = await _context.Nationalities.OrderBy(e => e.Name).ToListAsync(),
            ProducerList = await _context.Producers.OrderBy(e => e.Name).ToListAsync(),
        };
        return droplist;
    }

    public async Task UpdateNewMovieAsync(UpdateMovieViewModel model)
    {
        var onemovie = await GetByIdAsync(model.Id);
        if (onemovie != null) 
        {
            if(model.Picture != null)
            {
                var stream = new MemoryStream();
                await model.Picture.CopyToAsync(stream);
                onemovie.Picture = stream.ToArray();
            }

            onemovie.Title = model.Title;
            onemovie.Description = model.Description;
            onemovie.StartDate = model.StartDate;
            onemovie.EndDate = model.EndDate;
            onemovie.Price = model.Price;
            onemovie.CinemaId = model.CinemaId;
            onemovie.NationalityId = model.NationalityId;
            onemovie.ProducerId = model.ProducerId;
            onemovie.Genre = model.Genre;

            _context.Movies.Update(onemovie);
            await _context.SaveChangesAsync();

            //Remove existing actors
            var existingActorsDb = await _context.Actors_Movies.Where(n => n.MovieId == model.Id).ToListAsync();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            foreach (var actorId in model.Actor_Movie)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = onemovie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

       
    }
}
