using eTicketsApp.Data.Base;
using eTicketsApp.Models;

namespace eTicketsApp.Data.Services.Interfaces
{
    public interface IMovieService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
    }
}
