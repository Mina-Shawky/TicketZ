using Ticketz.Data;
using Ticketz.Models;
using Ticketz.Repository.Base;
using Ticketz.ViewModel;

namespace Ticketz.Repository.Services;

public class CinemaRepository : BaseRepository<Cinema>,ICinemaRepository
{
    private readonly TicketZDbContext _context;

    public CinemaRepository(TicketZDbContext context) :base(context)
    {
        _context = context;
    }

    public async Task AddNewCinemaAsync(AddCinemaViewModel model)
    {
        var stream = new MemoryStream();
        await model.Logo.CopyToAsync(stream);

        var cinema = new Cinema()
        {
            Name = model.Name,
            Description = model.Description,
            Logo = stream.ToArray()
        };

        await _context.Cinemas.AddAsync(cinema);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCinemaAsync(UpdateCinemaViewModel model)
    {
        var cinema = await _context.Cinemas.FindAsync(model.Id);
        if (cinema != null) 
        {
            if(model.Logo != null)
            {
                var stream = new MemoryStream();
                await model.Logo.CopyToAsync(stream);
                cinema.Logo = stream.ToArray();
            }
            cinema.Name = model.Name;
            cinema.Description = model.Description;

            _context.Cinemas.Update(cinema);
            await _context.SaveChangesAsync();
        }

    }
}
