using Ticketz.Models;
using Ticketz.Repository.Base;
using Ticketz.ViewModel;

namespace Ticketz.Repository.Services;

public interface IProducerRepository : IBaseRepository<Producer>
{
    Task AddProducerAsync(AddNewProducerViewModel model);
    Task UpdateProducerAsync(UpdateProducerViewModel model);

}
