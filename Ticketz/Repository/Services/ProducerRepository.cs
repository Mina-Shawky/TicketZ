using NToastNotify;
using Ticketz.Data;
using Ticketz.Models;
using Ticketz.Repository.Base;
using Ticketz.ViewModel;

namespace Ticketz.Repository.Services;

public class ProducerRepository : BaseRepository<Producer> , IProducerRepository
{
    private readonly TicketZDbContext _context;
    private readonly IToastNotification _toast;
    public ProducerRepository(TicketZDbContext context, IToastNotification toast) : base(context)
    {
        _context = context;
        _toast = toast;
    }

    public async Task AddProducerAsync(AddNewProducerViewModel model)
    {
        var stream = new MemoryStream();
        await model.Picture.CopyToAsync(stream);

        var producer = new Producer()
        {
            Name = model.Name,
            Bio = model.Bio,
            Picture = stream.ToArray()
        };

        await _context.Producers.AddAsync(producer);
        await _context.SaveChangesAsync();
        _toast.AddSuccessToastMessage("Producer Created Successfully");
    }

    public async Task UpdateProducerAsync(UpdateProducerViewModel model)
    {
        var producer = await _context.Producers.FindAsync(model.Id);
        if (producer != null)
        {
            if (model.Picture != null)
            {
                var stream = new MemoryStream();
                await model.Picture.CopyToAsync(stream);
                producer.Picture = stream.ToArray();
            }
            producer.Name = model.Name;
            producer.Bio = model.Bio;

            _context.Producers.Update(producer);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Producer Updated Successfully");

        }
    }
}
