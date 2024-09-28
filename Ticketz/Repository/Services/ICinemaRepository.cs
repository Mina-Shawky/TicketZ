using Ticketz.Models;
using Ticketz.Repository.Base;
using Ticketz.ViewModel;

namespace Ticketz.Repository.Services;

public interface ICinemaRepository : IBaseRepository<Cinema>
{
    Task AddNewCinemaAsync(AddCinemaViewModel model);
    Task UpdateCinemaAsync(UpdateCinemaViewModel model);
}
