
using Ticketz.Models;
using Ticketz.Repository.Base;
using Ticketz.ViewModel;

namespace Ticketz.Repository.Services;

public interface IMovieRepository : IBaseRepository<Movie>
{
    Task<MovieDropDawnListViewModel> MovieDropDawnListAsync();
    Task AddNewMovieAsync(CreateMovieViewModel model);
    Task UpdateNewMovieAsync(UpdateMovieViewModel model);


}
