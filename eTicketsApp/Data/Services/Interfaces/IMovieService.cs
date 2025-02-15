using eTicketsApp.Data.Base;
using eTicketsApp.Data.ViewModel;
using eTicketsApp.Models;

namespace eTicketsApp.Data.Services.Interfaces
{
    public interface IMovieService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownValues();
        Task AddNewMovieAsync(NewMovieVM newMovie);
        Task UpdateMovieAsync(NewMovieVM newMovie);
    }
}
