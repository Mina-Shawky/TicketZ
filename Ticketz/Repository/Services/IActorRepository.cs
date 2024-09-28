using Ticketz.Models;
using Ticketz.Repository.Base;
using Ticketz.ViewModel;

namespace Ticketz.Repository.Services;

public interface IActorRepository : IBaseRepository<Actor> 
{
    Task AddActorAsync(AddNewActorViewModel model);
    Task UpdateActorAsync(UpdateActorViewModel model);
}
