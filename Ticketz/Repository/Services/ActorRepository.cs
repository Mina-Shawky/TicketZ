using Ticketz.Data;
using Ticketz.Models;
using Ticketz.Repository.Base;
using Ticketz.ViewModel;

namespace Ticketz.Repository.Services;

public class ActorRepository : BaseRepository<Actor> , IActorRepository
{
    private readonly TicketZDbContext _context;

    public ActorRepository(TicketZDbContext context):base(context)
    {
        _context = context;
    }

    public async Task AddActorAsync(AddNewActorViewModel model)
    {
        var stream = new MemoryStream();
        await model.Picture.CopyToAsync(stream);

        var actor = new Actor()
        {
            Name = model.Name,
            Bio = model.Bio,
            Picture = stream.ToArray()
        };

        await _context.Actors.AddAsync(actor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateActorAsync(UpdateActorViewModel model)
    {
        var actor = await GetByIdAsync(model.Id);

        if (model.Picture != null) 
        {
            var stream = new MemoryStream();
            await model.Picture.CopyToAsync(stream);
            actor.Picture=stream.ToArray();
        }
        actor.Name = model.Name;
        actor.Bio = model.Bio;

        _context.Actors.Update(actor);
        await _context.SaveChangesAsync();
    }
}
